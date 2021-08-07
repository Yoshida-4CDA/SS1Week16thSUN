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

    float axisH;
    Rigidbody2D rb;

    bool isJump = false;    // ジャンプ中かどうかを判別する変数
    bool isGround = false;  // 地面に立っているかどうかを判別する変数
    bool isFinish = false;  // ゴールしたかどうかを判別する変数

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isFinish || !gameManager.isStart)
        {
            return;
        }

        Vector2 inputAxis = context.ReadValue<Vector2>();
        axisH = inputAxis.x;
        Debug.Log($"{axisH}");

        if (axisH > 0)
        {
            // Debug.Log("右へ移動");
            transform.localScale = Vector2.one;
        }
        else if (axisH < 0)
        {
            // Debug.Log("左へ移動");
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isFinish || !gameManager.isStart)
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
        // 地面についているかを判定
        Vector2 startPos = transform.position;
        Vector2 endPos = transform.position - (transform.up * 0.1f);

        isGround = Physics2D.Linecast(startPos, endPos, groundLayer);
        Debug.DrawLine(startPos, endPos, Color.red);

        if (isGround || axisH != 0)
        {
            // 地面の上または速度が0ではないなら速度を更新する
            rb.velocity = new Vector2(ParamsSO.Entity.playerSpeed * axisH, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Finish();
        }
    }

    /// <summary>
    /// ジャンプする
    /// </summary>
    void Jump()
    {
        Vector2 jumpPower = new Vector2(0, ParamsSO.Entity.playerJump);
        rb.AddForce(jumpPower, ForceMode2D.Impulse);
    }

    /// <summary>
    ///  ゴール時の処理
    /// </summary>
    void Finish()
    {
        isFinish = true;
        Debug.Log("ステージクリア");
        gameManager.StageClear();
    }
}
