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
    [SerializeField] Slider waterSlider = default;

    [SerializeField] Player player = default;

    public bool isStart;

    float waterMaxValue;
    float waterMinValue;
    float waterValue;

    public IEnumerator updateWaterValue;

    void Start()
    {
        StartCoroutine(GameStart());

        waterMaxValue = ParamsSO.Entity.waterMaxValue;
        waterMinValue = waterSlider.minValue;

        waterValue = waterMaxValue;
        waterSlider.value = waterValue;

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

        yield return updateWaterValue;
    }

    /// <summary>
    /// �����Q�[�W�����X�Ɍ��炷
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateWaterValue()
    {
        yield return new WaitForSeconds(0.5f);
        while (waterValue > waterMinValue)
        {
            waterValue--;
            waterSlider.value = waterValue;
            yield return new WaitForSeconds(0.3f);
        }
        if (waterValue <= waterMinValue)
        {
            player.PlayerDead();
        }
    }

    public void AddWaterValue(int value)
    {
        waterValue += value;
        if (waterValue > waterMaxValue)
        {
            waterValue = waterMaxValue;
        }
    }

    public void StageClear()
    {
        stageClearText.SetActive(true);
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
    }
}
