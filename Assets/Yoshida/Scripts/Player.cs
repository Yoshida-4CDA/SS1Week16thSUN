using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("�n�ʂ̃��C���[")]
    [SerializeField] LayerMask groundLayer = default;

    [Header("GameManager")]
    [SerializeField] GameManager gameManager = default;

    float axisH;
    Rigidbody2D rb;

    bool isJump = false;    // �W�����v�����ǂ����𔻕ʂ���ϐ�
    bool isGround = false;  // �n�ʂɗ����Ă��邩�ǂ����𔻕ʂ���ϐ�
    bool isFinish = false;  // �S�[���������ǂ����𔻕ʂ���ϐ�

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
            // Debug.Log("�E�ֈړ�");
            transform.localScale = Vector2.one;
        }
        else if (axisH < 0)
        {
            // Debug.Log("���ֈړ�");
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
            // �n�ʂ̏��Space�L�[�������ꂽ��W�����v������
            Jump();
        }
    }

    void FixedUpdate()
    {
        // �n�ʂɂ��Ă��邩�𔻒�
        Vector2 startPos = transform.position;
        Vector2 endPos = transform.position - (transform.up * 0.1f);

        isGround = Physics2D.Linecast(startPos, endPos, groundLayer);
        Debug.DrawLine(startPos, endPos, Color.red);

        if (isGround || axisH != 0)
        {
            // �n�ʂ̏�܂��͑��x��0�ł͂Ȃ��Ȃ瑬�x���X�V����
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
    /// �W�����v����
    /// </summary>
    void Jump()
    {
        Vector2 jumpPower = new Vector2(0, ParamsSO.Entity.playerJump);
        rb.AddForce(jumpPower, ForceMode2D.Impulse);
    }

    /// <summary>
    ///  �S�[�����̏���
    /// </summary>
    void Finish()
    {
        isFinish = true;
        Debug.Log("�X�e�[�W�N���A");
        gameManager.StageClear();
    }
}
