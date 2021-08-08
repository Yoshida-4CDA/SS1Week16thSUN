using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// 敵の進行方向
    /// </summary>
    public enum DIRECTION
    {
        STOP,
        RIGHT,
        LEFT
    }

    DIRECTION enemyDirection = DIRECTION.LEFT;

    Rigidbody2D rb;
    float speed;    // 移動スピード

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (transform.localScale.x != 1)
        {
            enemyDirection = DIRECTION.RIGHT;
        }
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        switch (enemyDirection)
        {
            case DIRECTION.STOP:                    // 停止
                speed = 0;
                break;
            case DIRECTION.RIGHT:                   // 右に移動
                speed = ParamsSO.Entity.enemySpeed;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case DIRECTION.LEFT:                    // 左に移動
                speed = ParamsSO.Entity.enemySpeed * -1;
                transform.localScale = new Vector3(1, 1, 1);
                break;
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Enemy"))
        {
            // 木もしくは敵同士ぶつかったら向きを反転させる
            ChangeDirection();
        }
    }

    /// <summary>
    /// 移動方向を反転させる
    /// </summary>
    void ChangeDirection()
    {
        if (enemyDirection == DIRECTION.RIGHT)      // 右⇒左
        {
            enemyDirection = DIRECTION.LEFT;
        }
        else if (enemyDirection == DIRECTION.LEFT)  // 左⇒右
        {
            enemyDirection = DIRECTION.RIGHT;
        }
    }

    /// <summary>
    /// 敵を消す
    /// </summary>
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
