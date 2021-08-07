using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("�Q�[���X�^�[�g�̃e�L�X�g")]
    [SerializeField] GameObject startText = default;

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
        // Invoke("RestartScene", 1.5f);   // 1.5�b��Ƀ��X�^�[�g
    }

    /*
    void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();    // ���݂̃V�[�����擾
        SceneManager.LoadScene(thisScene.name);
    }
    */
}
