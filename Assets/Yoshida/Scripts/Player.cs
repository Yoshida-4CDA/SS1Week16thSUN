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

    /// <summary>
    /// �v���C���[�̐i�s����
    /// </summary>
    public enum DIRECTION
    {
        STOP,
        RIGHT,
        LEFT
    }

    DIRECTION playerDirection = DIRECTION.STOP;

    float axisX;    // X���̒l(-1.0 ~ 1.0)
    static float defaultScale = 0.5f;
    Rigidbody2D rb;

    Animator animator;

    bool isFinish = false;  // �S�[���������ǂ����𔻕ʂ���ϐ�
    bool isDead = false;    // �v���C���[�����񂾂��ǂ����𔻕ʂ���ϐ�

    // 
    bool isDamage;          // �_���[�W���󂯂Ă��邩
    // 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(defaultScale, defaultScale);

        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isFinish || !gameManager.isStart || isDead || isDamage)
        {
            return;
        }

        // ���E�L�[�̓��͂��擾����X���̒l��ϐ��ɑ��
        Vector2 inputAxis = context.ReadValue<Vector2>();
        axisX = inputAxis.x;

        animator.SetFloat("speed", Mathf.Abs(axisX));

        if (axisX == 0)
        {
            // ��~���Ă���
            playerDirection = DIRECTION.STOP;
        }
        else if (axisX > 0)
        {
            // �E�������Ă���
            playerDirection = DIRECTION.RIGHT;
        }
        else if (axisX < 0)
        {
            // ���������Ă���
            playerDirection = DIRECTION.LEFT;
        }

        switch (playerDirection)
        {
            case DIRECTION.STOP:
                break;
            case DIRECTION.RIGHT:
                transform.localScale = new Vector2(defaultScale, defaultScale);
                break;
            case DIRECTION.LEFT:
                transform.localScale = new Vector2(defaultScale * -1, defaultScale);
                break;
        }

        float xSpeed = ParamsSO.Entity.playerSpeed * axisX;
        rb.velocity = new Vector2(xSpeed, rb.velocity.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isFinish || !gameManager.isStart || isDead || isDamage)
        {
            return;
        }

        if (context.performed && IsGround())
        {
            // �n�ʂ̏��Space�L�[�������ꂽ��W�����v������
            Jump();
        }
    }

    void FixedUpdate()
    {
    }

    /// <summary>
    /// �n�ʂɂ��Ă��邩�𔻒�
    /// </summary>
    /// <returns></returns>
    bool IsGround()
    {
        Vector3 startVec = transform.position - (transform.up * ParamsSO.Entity.playerDistanceToGround);
        Vector3 endVec = startVec - transform.up * 0.1f;
        Debug.DrawLine(startVec, endVec, Color.red);

        return Physics2D.Linecast(startVec, endVec, groundLayer);
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

            if (this.transform.position.y - ParamsSO.Entity.playerDistanceToEnemy[(int)enemy.enemyType] > enemy.transform.position.y)
            {
                // �ォ��G�𓥂񂾂�v���C���[���W�����v������
                rb.velocity = new Vector2(rb.velocity.x, 0);
                Jump();
                enemy.DestroyEnemy();
            }
            else
            {
                if (isDamage)
                {
                    return;
                }
                // �Ԃ�������_���[�W���󂯂�
                StartCoroutine(OnDamage(collision.gameObject));

                /*
                // ������Ԃ�������Q�[���I�[�o�[
                // PlayerDead();
                */
            }
        }
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    void Jump()
    {
        rb.AddForce(Vector2.up * ParamsSO.Entity.playerJump);
        animator.SetTrigger("jump");
    }

    /// <summary>
    /// �X�e�[�W�N���A�̏���
    /// </summary>
    void Finish()
    {
        isFinish = true;
        rb.velocity = new Vector2(0, 0);
        animator.SetFloat("speed", Mathf.Abs(0));
        Debug.Log("�X�e�[�W�N���A");
        gameManager.StageClear();
    }

    /// <summary>
    /// �Q�[���I�[�o�[�̏���
    /// </summary>
    public void PlayerDead()
    {
        isDead = true;
        animator.SetTrigger("die");
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.up * ParamsSO.Entity.playerJump);

        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Destroy(capsuleCollider2D);
        gameManager.GameOver();
    }

    IEnumerator OnDamage(GameObject enemy)
    {
        isDamage = true;
        Debug.Log(isDamage);

        // Hit�A�j���[�V�����ɐ؂�ւ���
        animator.SetTrigger("hit");

        // ��U�ړ����~�߂� 
        rb.velocity = Vector2.zero;
        animator.SetFloat("speed", Mathf.Abs(0));

        // �G�Ɣ��Ε����Ƀm�b�N�o�b�N
        Vector3 v = (transform.position - enemy.transform.position).normalized;
        rb.AddForce(new Vector2(v.x * 2, 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;
        isDamage = false;
        Debug.Log(isDamage);
    }
}
