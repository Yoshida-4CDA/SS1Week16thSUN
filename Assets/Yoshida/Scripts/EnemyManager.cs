using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(ParamsSO.Entity.enemySpeed * -1, rb.velocity.y);
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
