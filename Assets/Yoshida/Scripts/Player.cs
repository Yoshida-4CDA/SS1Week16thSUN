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

    float axisX;    // X���̒l(-1.0 ~ 1.0)
    Rigidbody2D rb;

    bool isGround = false;  // �n�ʂɗ����Ă��邩�ǂ����𔻕ʂ���ϐ�
    bool isFinish = false;  // �S�[���������ǂ����𔻕ʂ���ϐ�
    bool isDead = false;    // �v���C���[�����񂾂��ǂ����𔻕ʂ���ϐ�

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

        // ���E�L�[�̓��͂��擾����X���̒l��ϐ��ɑ��
        Vector2 inputAxis = context.ReadValue<Vector2>();
        axisX = inputAxis.x;

        if (axisX > 0)
        {
            // �E�ֈړ�
            transform.localScale = Vector2.one;
        }
        else if (axisX < 0)
        {
            // ���ֈړ�(�v���C���[�𔽓]������)
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
            // �n�ʂ̏��Space�L�[�������ꂽ��W�����v������
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        // �n�ʂɂ��Ă��邩�𔻒�
        Vector2 startPos = transform.position;
        Vector2 endPos = transform.position - (transform.up * 0.1f);

        isGround = Physics2D.Linecast(startPos, endPos, groundLayer);
        Debug.DrawLine(startPos, endPos, Color.red);

        if (isGround || axisX != 0)
        {
            // �n�ʂ̏�܂��͑��x��0�ł͂Ȃ��Ȃ瑬�x���X�V����
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
            // �X�e�[�W�N���A
            Finish();
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            // �A�C�e���Q�b�g
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();

            if (this.transform.position.y > enemy.transform.position.y)
            {
                // �ォ��G�𓥂񂾂�v���C���[���W�����v������
                rb.velocity = new Vector2(rb.velocity.x, 0);
                Jump();
                enemy.DestroyEnemy();
            }
            else
            {
                // ������Ԃ�������Q�[���I�[�o�[
                PlayerDead();
            }
        }
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    void Jump()
    {
        rb.AddForce(Vector2.up * ParamsSO.Entity.playerJump);
    }

    /// <summary>
    /// �X�e�[�W�N���A�̏���
    /// </summary>
    void Finish()
    {
        isFinish = true;
        Debug.Log("�X�e�[�W�N���A");
        gameManager.StageClear();
    }

    /// <summary>
    /// �Q�[���I�[�o�[�̏���
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
