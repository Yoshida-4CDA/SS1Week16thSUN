using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("移動スピード")]
    [SerializeField] float speed = 3f;

    [Header("ジャンプ力")]
    [SerializeField] float jump = 9f;

    [Header("地面のレイヤー")]
    [SerializeField] LayerMask groundLayer = default;

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
        if (isFinish)
        {
            return;
        }

        Vector2 inputAxis = context.ReadValue<Vector2>();
        axisH = inputAxis.x;
        Debug.Log($"{axisH}");

        if (axisH > 0)
        {
            Debug.Log("右へ移動");
            transform.localScale = Vector2.one;
        }
        else if (axisH < 0)
        {
            Debug.Log("左へ移動");
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isFinish)
        {
            return;
        }

        if (context.performed)
        {
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
            rb.velocity = new Vector2(speed * axisH, rb.velocity.y);
        }

        if (isGround && isJump)
        {
            // 地面の上でSpaceキーが押されたらジャンプさせる
            Debug.Log("ジャンプ");
            Vector2 jumpPower = new Vector2(0, jump);
            rb.AddForce(jumpPower, ForceMode2D.Impulse);
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // ゴール
            Debug.Log("ゴール");
            isFinish = true;
        }
    }

    /// <summary>
    /// ジャンプする
    /// </summary>
    void Jump()
    {
        isJump = true;
        Debug.Log("Spaceを押した");
    }
}
