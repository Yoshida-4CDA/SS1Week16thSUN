using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// �G�̐i�s����
    /// </summary>
    public enum DIRECTION
    {
        STOP,
        RIGHT,
        LEFT
    }

    DIRECTION direction = DIRECTION.LEFT;

    Rigidbody2D rb;
    float speed;    // �ړ��X�s�[�h

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (transform.localScale.x != 1)
        {
            direction = DIRECTION.RIGHT;
        }
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION.STOP:                    // ��~
                speed = 0;
                break;
            case DIRECTION.RIGHT:                   // �E�Ɉړ�
                speed = ParamsSO.Entity.enemySpeed;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case DIRECTION.LEFT:                    // ���Ɉړ�
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
            // �؂������͓G���m�Ԃ�����������𔽓]������
            ChangeDirection();
        }
    }

    /// <summary>
    /// �ړ������𔽓]������
    /// </summary>
    void ChangeDirection()
    {
        if (direction == DIRECTION.RIGHT)      // �E�ˍ�
        {
            direction = DIRECTION.LEFT;
        }
        else if (direction == DIRECTION.LEFT)  // ���ˉE
        {
            direction = DIRECTION.RIGHT;
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
