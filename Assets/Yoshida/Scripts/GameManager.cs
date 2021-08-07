using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("�Q�[���X�^�[�g�̃e�L�X�g")]
    [SerializeField] GameObject startText = default;

    [Header("�Q�[���I�[�o�[�̃e�L�X�g")]
    [SerializeField] GameObject gameOverText = default;

    [Header("�X�e�[�W�N���A�̃e�L�X�g")]
    [SerializeField] GameObject stageClearText = default;

    public bool isStart;

    void Start()
    {
        StartCoroutine(GameStart());
    }

    void Update()
    {
    }

    IEnumerator GameStart()
    {
        isStart = false;

        yield return new WaitForSeconds(1.5f);
        startText.SetActive(false);

        isStart = true;
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
