using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("�Q�[���X�^�[�g�̃e�L�X�g")]
    [SerializeField] GameObject startText = default;

    [Header("�Q�[���I�[�o�[�̃e�L�X�g")]
    [SerializeField] GameObject gameOverText = default;

    [Header("�X�e�[�W�N���A�̃e�L�X�g")]
    [SerializeField] GameObject stageClearText = default;

    [Header("�����Q�[�W")]
    [SerializeField] Image waterGauge = default;

    [Header("Player")]
    [SerializeField] Player player = default;

    public bool isStart;    // �Q�[�����X�^�[�g�������ǂ����𔻕ʂ���ϐ�
    
    float waterMaxValue;        // �����Q�[�W�̍ő�l
    float currentWaterValue;    // ���݂̐����Q�[�W�̒l

    // �R���[�`����������ϐ�
    public IEnumerator updateWaterValue;

    void Start()
    {
        StartCoroutine(GameStart());

        // �����Q�[�W�̏�����
        waterMaxValue = ParamsSO.Entity.waterGaugeMaxValue;
        currentWaterValue = waterMaxValue;
        waterGauge.fillAmount = currentWaterValue / waterMaxValue;

        // �R���[�`����ϐ��ɑ�� => �R���[�`���̏�����r���Œ�~�����邽��
        updateWaterValue = UpdateWaterValue();
    }

    void Update()
    {
    }

    /// <summary>
    /// �uSTART�v�̕\��
    /// </summary>
    /// <returns></returns>
    IEnumerator GameStart()
    {
        isStart = false;

        yield return new WaitForSeconds(1.5f);
        startText.SetActive(false);
        isStart = true;

        // �R���[�`���Ăяo��
        yield return updateWaterValue;
    }

    /// <summary>
    /// �����Q�[�W�����X�Ɍ��炷
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateWaterValue()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("�����J�n");
        while (currentWaterValue > 0)
        {
            yield return new WaitForSeconds(2f);
            currentWaterValue -= ParamsSO.Entity.waterThirstyValue;
            waterGauge.fillAmount = currentWaterValue / waterMaxValue;
            Debug.Log($"{currentWaterValue}");
            yield return null;
        }
        if (currentWaterValue <= 0)
        {
            // 0�ɂȂ�����Q�[���I�[�o�[
            player.PlayerDead();
        }
    }

    /// <summary>
    /// �����Q�[�W�̉�
    /// </summary>
    /// <param name="value">�񕜗�</param>
    public void RecoverWaterValue(int value)
    {
        Debug.Log("��");
        currentWaterValue += value;
        if (currentWaterValue > waterMaxValue)
        {
            currentWaterValue = waterMaxValue;
        }
        waterGauge.fillAmount = currentWaterValue / waterMaxValue;
        Debug.Log(currentWaterValue);
    }

    /// <summary>
    /// �X�e�[�W�N���A
    /// </summary>
    public void StageClear()
    {
        StopCoroutine(updateWaterValue);
        stageClearText.SetActive(true);
    }

    /// <summary>
    /// �Q�[���I�[�o�[
    /// </summary>
    public void GameOver()
    {
        StopCoroutine(updateWaterValue);
        gameOverText.SetActive(true);
    }
}
