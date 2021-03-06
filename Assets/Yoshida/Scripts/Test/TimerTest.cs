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

    // ========================
    GameObject[] targets;
    EnemyManager.ENEMY_TYPE targetType;

    void Start()
    {
        z = Mathf.RoundToInt(rotationObj.transform.localEulerAngles.z);

        dayBgColor = Camera.main.backgroundColor;
        nightBgColor = Color.black;

        rotationTest = RotationTest();
        StartCoroutine(rotationTest);

        // ========================
        targets = GameObject.FindGameObjectsWithTag("Enemy");
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
        foreach (GameObject targetObj in targets)
        {
            targetType = targetObj.GetComponent<EnemyManager>().enemyType;
            if (targetType == EnemyManager.ENEMY_TYPE.Mummy)
            {
                if (z >= 0 && z < 120 || z >= 300)
                {
                    targetObj.SetActive(false);
                }
                else
                {
                    targetObj.SetActive(true);
                }
            }
        }

        if (z >= 0 && z < 120 || z >= 300)
        {
            dayText.SetActive(true);
            nightText.SetActive(false);
        }
        else
        {
            dayText.SetActive(false);
            nightText.SetActive(true);
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
