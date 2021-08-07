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
    [SerializeField] Slider waterSlider = default;

    [Header("Player")]
    [SerializeField] Player player = default;

    public bool isStart;    // ゲームがスタートしたかどうかを判別する変数

    float waterMaxValue;    // 水分ゲージの最大値
    float waterMinValue;    // 水分ゲージの最小値
    float waterValue;       // 水分ゲージの値

    // コルーチンを代入する変数
    IEnumerator updateWaterValue;

    void Start()
    {
        StartCoroutine(GameStart());

        // 水分ゲージの初期化
        waterMaxValue = ParamsSO.Entity.waterMaxValue;
        waterMinValue = waterSlider.minValue;
        waterValue = waterMaxValue;
        waterSlider.value = waterValue;

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

        // コルーチン呼び出し
        yield return updateWaterValue;
    }

    /// <summary>
    /// 水分ゲージを徐々に減らす
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateWaterValue()
    {
        yield return new WaitForSeconds(0.5f);
        while (waterValue > waterMinValue)
        {
            // ゲージを減らす
            waterValue--;
            waterSlider.value = waterValue;
            yield return new WaitForSeconds(0.3f);
        }
        if (waterValue <= waterMinValue)
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
        waterValue += value;
        if (waterValue > waterMaxValue)
        {
            waterValue = waterMaxValue;
        }
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
