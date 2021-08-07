using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum DIRECTION
    {
        STOP,
        RIGHT,
        LEFT
    }

    DIRECTION direction = DIRECTION.LEFT;

    Rigidbody2D rb;
    float speed;

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
            case DIRECTION.STOP:
                speed = 0;
                break;
            case DIRECTION.RIGHT:
                speed = ParamsSO.Entity.enemySpeed;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case DIRECTION.LEFT:
                speed = ParamsSO.Entity.enemySpeed * -1;
                transform.localScale = new Vector3(1, 1, 1);
                break;
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            ChangeDirection();
        }
    }

    /// <summary>
    /// à⁄ìÆï˚å¸ÇîΩì]Ç≥ÇπÇÈ
    /// </summary>
    void ChangeDirection()
    {
        if (direction == DIRECTION.RIGHT)      // âEÅÀç∂
        {
            direction = DIRECTION.LEFT;
        }
        else if (direction == DIRECTION.LEFT)  // ç∂ÅÀâE
        {
            direction = DIRECTION.RIGHT;
        }
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
