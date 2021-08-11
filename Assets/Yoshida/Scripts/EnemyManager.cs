using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// 敵の種類
    /// </summary>
    public enum ENEMY_TYPE
    {
        Mummy,
        Scorpion,
        Snake,
        Cat
    }

    /// <summary>
    /// 敵の進行方向
    /// </summary>
    public enum DIRECTION
    {
        STOP,
        RIGHT,
        LEFT
    }

    [Header("地面のレイヤー")]
    [SerializeField] LayerMask groundLayer = default;

    [Header("プレイヤーのレイヤー")]
    [SerializeField] LayerMask playerLayer = default;

    [Header("敵の種類")]
    public ENEMY_TYPE enemyType;

    DIRECTION enemyDirection = DIRECTION.LEFT;

    Rigidbody2D rb;
    float speed;    // 移動スピード

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (transform.localScale.x < 0)
        {
            enemyDirection = DIRECTION.RIGHT;
        }
    }

    void Update()
    {
        IsPlayer();
    }

    void FixedUpdate()
    {
        if (!IsGround())
        {
            return;
        }

        float enemySize = ParamsSO.Entity.enemyScale[(int)enemyType];
        float enemyMove = ParamsSO.Entity.enemySpeed[(int)enemyType];

        switch (enemyDirection)
        {
            case DIRECTION.STOP:                    // 停止
                speed = 0;
                break;
            case DIRECTION.RIGHT:                   // 右に移動
                speed = enemyMove;
                transform.localScale = new Vector3(enemySize * -1, enemySize, 1);
                break;
            case DIRECTION.LEFT:                    // 左に移動
                speed = enemyMove * -1;
                transform.localScale = new Vector3(enemySize, enemySize, 1);
                break;
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 壁にぶつかったら向きを反転させる
            ChangeDirection();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsPlayer())
        {
            // プレイヤーにぶつかったら向きを反転させる
            ChangeDirection();
        }
    }

    public bool IsPlayer()
    {
        Vector3 startVecPos = transform.position - (transform.up * 0.35f);
        Vector3 startVec = startVecPos - (0.3f * transform.localScale.x * transform.right);
        Vector3 endVec = startVec - (0.75f * transform.localScale.x * transform.right);
        Debug.DrawLine(startVec, endVec, Color.red);
        return Physics2D.Linecast(startVec, endVec, playerLayer);
    }

    bool IsGround()
    {
        Vector3 startVec = transform.position - (transform.up * ParamsSO.Entity.enemyDistanceToGround[(int)enemyType]);
        Vector3 endVec = startVec - transform.up * 0.2f;
        Debug.DrawLine(startVec, endVec, Color.red);

        return Physics2D.Linecast(startVec, endVec, groundLayer);
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
