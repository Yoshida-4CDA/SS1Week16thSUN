using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("画面端(左)")]
    [SerializeField] float leftLimit = default;

    [Header("画面端(右)")]
    [SerializeField] float rightLimit = default;

    void Start()
    {
    }

    void Update()
    {
        // Playerを探す
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // カメラ座標の更新
            float x = player.transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;

            // 両端に移動制限をつける
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }

            // カメラ位置の設定
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;
        }
    }
}
