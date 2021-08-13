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
        Cat,
        Bird
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

    [HideInInspector]
    [Header("地面のレイヤー")]
    [SerializeField] LayerMask groundLayer = default;

    [Header("プレイヤーのレイヤー")]
    [SerializeField] LayerMask playerLayer = default;

    [Header("敵の種類")]
    public ENEMY_TYPE enemyType;

    DIRECTION enemyDirection = DIRECTION.LEFT;

    Rigidbody2D rb;
    float speed;    // 移動スピード

    // ===== 夜間の処理用の変数(ミイラ専用) =====
    GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ===== 夜間の処理用の処理(ミイラ専用) =====
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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

        // ===== 夜間の処理用の処理(ミイラ専用) =====
        if (enemyType == ENEMY_TYPE.Mummy)
        {
            // 元の移動スピードとサイズを保存
            Vector3 currentLocalScale = transform.localScale;

            if (gameManager.isDay)
            {
                // 昼間はミイラを表示しない
                HideMummy();
            }
            else
            {
                // 夜間はミイラを表示する
                ShowMummy(currentLocalScale);
            }
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    /// <summary>
    /// ミイラを(無理やり)非表示にする
    /// </summary>
    void HideMummy()
    {
        // 移動、重力、コライダーの判定を全て無効にする
        rb.simulated = false;

        // サイズを0にする
        transform.localScale = new Vector3(0, 0, 1);
    }

    /// <summary>
    /// ミイラを表示する
    /// </summary>
    /// <param name="currentSpeed">元の移動スピード</param>
    /// <param name="currentLocalScale">元のサイズ</param>
    void ShowMummy(Vector3 currentLocalScale)
    {
        // 移動、重力、コライダーの判定を全て有効にする
        rb.simulated = true;

        // サイズを元に戻す
        transform.localScale = currentLocalScale;
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

    /// <summary>
    /// プレイヤーに触れたかどうかを判別
    /// </summary>
    /// <returns></returns>
    bool IsPlayer()
    {
        Vector3 startVecPos = transform.position - (transform.up * 0.35f);
        Vector3 startVec = startVecPos - (0.3f * transform.localScale.x * transform.right);
        Vector3 endVec = startVec - (0.75f * transform.localScale.x * transform.right);
        Debug.DrawLine(startVec, endVec, Color.red);
        return Physics2D.Linecast(startVec, endVec, playerLayer);
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
        SoundManager.instance.PlaySE(SoundManager.SE.Attack);
        Destroy(this.gameObject);
    }
}
