using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DayTimer動作テスト用
/// </summary>
public class TimerTest : MonoBehaviour
{
    [Header("回転させるオブジェクト")]
    [SerializeField] GameObject rotationObj = default;

    [Header("回転速度")]
    [SerializeField] float rotationSpeed = default;

    [Header("昼テキスト(GameObject)")]
    [SerializeField] GameObject dayText = default;

    [Header("夜テキスト(GameObject)")]
    [SerializeField] GameObject nightText = default;

    int z;  // 0 ~ 359

    Color dayBgColor;
    Color nightBgColor;
    
    IEnumerator rotationTest;

    void Start()
    {
        z = Mathf.RoundToInt(rotationObj.transform.localEulerAngles.z);

        dayBgColor = Camera.main.backgroundColor;
        nightBgColor = Color.black;

        rotationTest = RotationTest();
        StartCoroutine(rotationTest);
    }

    void Update()
    {
        DayOrNight();
    }

    /// <summary>
    /// 昼と夜を判別する
    /// </summary>
    void DayOrNight()
    {
        // if (z >= 0 && z < 180)
        if (z >= 0 && z < 120 || z >= 300)
        {
            dayText.SetActive(true);
            nightText.SetActive(false);
            // Camera.main.backgroundColor = dayBgColor;
        }
        else
        {
            dayText.SetActive(false);
            nightText.SetActive(true);
            // Camera.main.backgroundColor = nightBgColor;
        }
    }

    /// <summary>
    /// DayTimerの回転
    /// </summary>
    /// <returns></returns>
    IEnumerator RotationTest()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            rotationObj.transform.Rotate(new Vector3(0, 0, rotationSpeed));
            z = Mathf.RoundToInt(rotationObj.transform.localEulerAngles.z);
            Debug.Log(z);

            float t = Mathf.PingPong(z, 180);
            Camera.main.backgroundColor = Color.Lerp(dayBgColor, nightBgColor, t / 180);
            // Debug.Log(t / 180);

            yield return null;
            // yield return new WaitForSeconds(0.5f);
        }
    }
}
