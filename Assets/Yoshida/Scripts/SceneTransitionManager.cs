using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void OnClickNext(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickRetry()
    {
        Scene thisScene = SceneManager.GetActiveScene();    // 現在のシーンを取得
        SceneManager.LoadScene(thisScene.name);
    }
}
