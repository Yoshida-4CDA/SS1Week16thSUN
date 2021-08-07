using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("地面のレイヤー")]
    [SerializeField] LayerMask groundLayer = default;

    [Header("GameManager")]
    [SerializeField] GameManager gameManager = default;

    float axisX;    // X軸の値(-1.0 ~ 1.0)
    Rigidbody2D rb;

    bool isGround = false;  // 地面に立っているかどうかを判別する変数
    bool isFinish = false;  // ゴールしたかどうかを判別する変数
    bool isDead = false;    // プレイヤーが死んだかどうかを判別する変数

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isFinish || !gameManager.isStart || isDead)
        {
            return;
        }

        // 左右キーの入力を取得してX軸の値を変数に代入
        Vector2 inputAxis = context.ReadValue<Vector2>();
        axisX = inputAxis.x;

        if (axisX > 0)
        {
            // 右へ移動
            transform.localScale = Vector2.one;
        }
        else if (axisX < 0)
        {
            // 左へ移動(プレイヤーを反転させる)
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isFinish || !gameManager.isStart || isDead)
        {
            return;
        }

        if (context.performed && isGround)
        {
            // 地面の上でSpaceキーが押されたらジャンプさせる
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        // 地面についているかを判定
        Vector2 startPos = transform.position;
        Vector2 endPos = transform.position - (transform.up * 0.1f);

        isGround = Physics2D.Linecast(startPos, endPos, groundLayer);
        Debug.DrawLine(startPos, endPos, Color.red);

        if (isGround || axisX != 0)
        {
            // 地面の上または速度が0ではないなら速度を更新する
            rb.velocity = new Vector2(ParamsSO.Entity.playerSpeed * axisX, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            // ステージクリア
            Finish();
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            // アイテムゲット
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();

            if (this.transform.position.y > enemy.transform.position.y)
            {
                // 上から敵を踏んだらプレイヤーをジャンプさせる
                rb.velocity = new Vector2(rb.velocity.x, 0);
                Jump();
                enemy.DestroyEnemy();
            }
            else
            {
                // 横からぶつかったらゲームオーバー
                PlayerDead();
            }
        }
    }

    /// <summary>
    /// ジャンプする
    /// </summary>
    void Jump()
    {
        rb.AddForce(Vector2.up * ParamsSO.Entity.playerJump);
    }

    /// <summary>
    /// ステージクリアの処理
    /// </summary>
    void Finish()
    {
        isFinish = true;
        Debug.Log("ステージクリア");
        gameManager.StageClear();
    }

    /// <summary>
    /// ゲームオーバーの処理
    /// </summary>
    public void PlayerDead()
    {
        isDead = true;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.up * ParamsSO.Entity.playerJump);

        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Destroy(capsuleCollider2D);
        gameManager.GameOver();
    }
}
