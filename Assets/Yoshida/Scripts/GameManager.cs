using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("ゲームスタートのテキスト")]
    [SerializeField] GameObject startText = default;

    [Header("ゲームオーバーのテキスト")]
    [SerializeField] GameObject gameOverText = default;

    [Header("ステージクリアのテキスト")]
    [SerializeField] GameObject stageClearText = default;

    [Header("水分ゲージ")]
    [SerializeField] Image waterGauge = default;

    [Header("Player")]
    [SerializeField] Player player = default;

    [Header("DayTimer")]
    [SerializeField] GameObject dayTimer = default;

    [Header("太陽/月")]
    [SerializeField] GameObject sunObj = default;

    public bool isStart;    // ゲームがスタートしたかどうかを判別する変数
    
    float maxWaterValue;        // 水分ゲージの最大値
    float currentWaterValue;    // 現在の水分ゲージの値

    // ===== DayTimer用の変数 =====
    int z;      // 0 ~ 359
    Color dayBgColor;       // 昼の背景色
    Color nightBgColor;     // 夜の背景色

    SpriteRenderer objSprite;
    Color sunColor;         // sunObjの色 => 太陽(白)
    Color moonColor;        // sunObjの色 => 月(黄色)

    // コルーチンを代入する変数
    IEnumerator updateWaterValue;
    IEnumerator timerRotation;

    void Start()
    {
        StartCoroutine(GameStart());

        // 水分ゲージの初期化
        maxWaterValue = ParamsSO.Entity.maxWaterGaugeValue;
        currentWaterValue = maxWaterValue;
        waterGauge.fillAmount = currentWaterValue / maxWaterValue;

        // DayTimerの初期設定
        z = Mathf.RoundToInt(dayTimer.transform.localEulerAngles.z);
        dayBgColor = Camera.main.backgroundColor;
        nightBgColor = Color.black;

        sunColor = Color.white;
        moonColor = Color.yellow;
        objSprite = sunObj.GetComponent<SpriteRenderer>();

        // コルーチンを変数に代入 => コルーチンの処理を途中で停止させるため
        updateWaterValue = UpdateWaterValue();
        timerRotation = TimerRotation();
    }

    void Update()
    {
    }

    /// <summary>
    /// 「START」の表示
    /// </summary>
    /// <returns></returns>
    IEnumerator GameStart()
    {
        isStart = false;

        yield return new WaitForSeconds(1.5f);
        startText.SetActive(false);
        isStart = true;
        Debug.Log("ゲームスタート");

        // コルーチン呼び出し
        StartCoroutine(updateWaterValue);
        StartCoroutine(timerRotation);
    }

    /// <summary>
    /// 水分ゲージを徐々に減らす
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateWaterValue()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("水分ゲージの減少");
        while (currentWaterValue > 0)
        {
            yield return new WaitForSeconds(2f);
            currentWaterValue -= ParamsSO.Entity.waterThirstyValue;
            waterGauge.fillAmount = currentWaterValue / maxWaterValue;
            Debug.Log($"{currentWaterValue}");
            yield return null;
        }
        if (currentWaterValue <= 0)
        {
            // 0になったらゲームオーバー
            player.PlayerDead();
        }
    }

    /// <summary>
    /// 水分ゲージの回復
    /// </summary>
    /// <param name="value">回復量</param>
    public void RecoverWaterValue(int value)
    {
        Debug.Log("水分ゲージを回復");
        currentWaterValue += value;
        if (currentWaterValue > maxWaterValue)
        {
            currentWaterValue = maxWaterValue;
        }
        waterGauge.fillAmount = currentWaterValue / maxWaterValue;
        Debug.Log(currentWaterValue);
    }

    /// <summary>
    /// DayTimerの回転
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerRotation()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("タイマーの回転");

        while (true)
        {
            dayTimer.transform.Rotate(new Vector3(0, 0, ParamsSO.Entity.timerRotationSpeed));
            z = Mathf.RoundToInt(dayTimer.transform.localEulerAngles.z);

            float t = Mathf.PingPong(z, 180);
            // カメラの背景色を徐々に変える
            Camera.main.backgroundColor = Color.Lerp(dayBgColor, nightBgColor, t / 180);
            // 太陽/月の色を徐々に変える
            objSprite.color = Color.Lerp(sunColor, moonColor, t / 180);
            yield return null;
        }
    }

    /// <summary>
    /// ステージクリア
    /// </summary>
    public void StageClear()
    {
        StopCoroutine(updateWaterValue);
        StopCoroutine(timerRotation);
        stageClearText.SetActive(true);
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        StopCoroutine(updateWaterValue);
        StopCoroutine(timerRotation);
        gameOverText.SetActive(true);
    }
}
