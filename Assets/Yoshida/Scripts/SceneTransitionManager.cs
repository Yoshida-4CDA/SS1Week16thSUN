using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void OnClickButton(string sceneName)
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Click);
        SceneManager.LoadScene(sceneName);

        // シーンに合わせてBGMを設定する
        if (sceneName == "Stage1")
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.BGM.DesertNoon);

            Player.currentHp = ParamsSO.Entity.maxHpGaugeValue;
        }
        else if (sceneName == "Stage2")
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.BGM.CityNight);
        }
        else if (sceneName == "Stage3")
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.BGM.Temple);
        }
        else
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.BGM.AmachanTheme);
        }
    }

    public void OnClickRetry()
    {
        Scene thisScene = SceneManager.GetActiveScene();    // 現在のシーンを取得
        SoundManager.instance.PlaySE(SoundManager.SE.Click);
        Player.currentHp = ParamsSO.Entity.maxHpGaugeValue;
        SceneManager.LoadScene(thisScene.name);

        // シーンに合わせてBGMを設定する
        string sceneName = thisScene.name;
        if (sceneName == "Stage1")
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.BGM.DesertNoon);
        }
        else if (sceneName == "Stage2")
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.BGM.CityNight);
        }
        else if (sceneName == "Stage3")
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.BGM.Temple);
        }
    }
}
