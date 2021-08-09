using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// �G�̎��
    /// </summary>
    public enum ENEMY_TYPE
    {
        Mummy,
        Scorpion,
        Snake
    }

    /// <summary>
    /// �G�̐i�s����
    /// </summary>
    public enum DIRECTION
    {
        STOP,
        RIGHT,
        LEFT
    }

    [Header("�n�ʂ̃��C���[")]
    [SerializeField] LayerMask groundLayer = default;

    [Header("�G�̎��")]
    public ENEMY_TYPE enemyType;

    DIRECTION enemyDirection = DIRECTION.LEFT;

    Rigidbody2D rb;
    float speed;    // �ړ��X�s�[�h

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
            case DIRECTION.STOP:                    // ��~
                speed = 0;
                break;
            case DIRECTION.RIGHT:                   // �E�Ɉړ�
                speed = enemyMove;
                transform.localScale = new Vector3(enemySize * -1, enemySize, 1);
                break;
            case DIRECTION.LEFT:                    // ���Ɉړ�
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
            // �ǂɂԂ�����������𔽓]������
            ChangeDirection();
        }
    }

    bool IsGround()
    {
        Vector3 startVec = transform.position - (transform.up * ParamsSO.Entity.enemyDistanceToGround[(int)enemyType]);
        Vector3 endVec = startVec - transform.up * 0.2f;
        Debug.DrawLine(startVec, endVec, Color.red);

        return Physics2D.Linecast(startVec, endVec, groundLayer);
    }

    /// <summary>
    /// �ړ������𔽓]������
    /// </summary>
    void ChangeDirection()
    {
        if (enemyDirection == DIRECTION.RIGHT)      // �E�ˍ�
        {
            enemyDirection = DIRECTION.LEFT;
        }
        else if (enemyDirection == DIRECTION.LEFT)  // ���ˉE
        {
            enemyDirection = DIRECTION.RIGHT;
        }
    }

    /// <summary>
    /// �G������
    /// </summary>
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
