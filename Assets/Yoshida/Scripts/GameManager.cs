using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("ゲームスタートのテキスト")]
    [SerializeField] GameObject startText = default;

    [Header("ステージクリアのテキスト")]
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
        // Invoke("RestartScene", 1.5f);   // 1.5秒後にリスタート
    }

    /*
    void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();    // 現在のシーンを取得
        SceneManager.LoadScene(thisScene.name);
    }
    */
}
