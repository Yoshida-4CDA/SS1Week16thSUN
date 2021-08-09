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

    public bool isStart;    // ゲームがスタートしたかどうかを判別する変数
    
    float maxWaterValue;        // 水分ゲージの最大値
    float currentWaterValue;    // 現在の水分ゲージの値

    // コルーチンを代入する変数
    public IEnumerator updateWaterValue;

    void Start()
    {
        StartCoroutine(GameStart());

        // 水分ゲージの初期化
        maxWaterValue = ParamsSO.Entity.maxWaterGaugeValue;
        currentWaterValue = maxWaterValue;
        waterGauge.fillAmount = currentWaterValue / maxWaterValue;

        // コルーチンを変数に代入 => コルーチンの処理を途中で停止させるため
        updateWaterValue = UpdateWaterValue();
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
        yield return updateWaterValue;
    }

    /// <summary>
    /// 水分ゲージを徐々に減らす
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateWaterValue()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("水分ゲージの減少開始");
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
    /// ステージクリア
    /// </summary>
    public void StageClear()
    {
        StopCoroutine(updateWaterValue);
        stageClearText.SetActive(true);
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        StopCoroutine(updateWaterValue);
        gameOverText.SetActive(true);
    }
}
