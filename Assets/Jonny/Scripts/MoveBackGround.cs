using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    // 動く速さを決める
    [SerializeField] float speed;

    // 移動先と移動ポイントを決める
    [SerializeField] float endPos; // ここまで
    [SerializeField] float movePos; // ここに移動

    void Update()
    {
        transform.Translate(speed, 0, 0);

        // endPosまできたらmovePosに移動させる
        if (transform.position.x < endPos)
        {
            transform.position = new Vector3(movePos, transform.position.y, 0);
        }
    }
}
