using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("地面のレイヤー")]
    [SerializeField] LayerMask groundLayer = default;

    [Header("GameManager")]
    [SerializeField] GameManager gameManager = default;

    [Header("HPゲージ")]
    [SerializeField] Image hpGauge = default;

    /// <summary>
    /// プレイヤーの進行方向
    /// </summary>
    public enum DIRECTION
    {
        STOP,
        RIGHT,
        LEFT
    }

    DIRECTION playerDirection = DIRECTION.STOP;

    float axisX;    // X軸の値(-1.0 ~ 1.0)
    static float defaultScale = 0.5f;
    Rigidbody2D rb;

    Animator animator;

    float maxHp;                        // HPゲージの最大値
    public static float currentHp;      // 現在のHP

    bool isFinish = false;  // ゴールしたかどうかを判別する変数
    bool isDead = false;    // プレイヤーが死んだかどうかを判別する変数
    bool isDamage;          // ダメージを受けているかどうかを判別する変数

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(defaultScale, defaultScale);

        animator = GetComponent<Animator>();

        // HPゲージの初期化
        maxHp = ParamsSO.Entity.maxHpGaugeValue;
        currentHp = GetHP();
        hpGauge.fillAmount = currentHp / maxHp;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isFinish || !gameManager.isStart || isDead || isDamage)
        {
            return;
        }

        // 左右キーの入力を取得してX軸の値を変数に代入
        Vector2 inputAxis = context.ReadValue<Vector2>();
        axisX = inputAxis.x;

        animator.SetFloat("speed", Mathf.Abs(axisX));

        if (axisX == 0)
        {
            // 停止している
            playerDirection = DIRECTION.STOP;
        }
        else if (axisX > 0)
        {
            // 右を向いている
            playerDirection = DIRECTION.RIGHT;
        }
        else if (axisX < 0)
        {
            // 左を向いている
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
            // 地面の上でSpaceキーが押されたらジャンプさせる
            Jump();
        }
    }

    /// <summary>
    /// 地面に接しているかどうかを判別
    /// </summary>
    /// <returns></returns>
    bool IsGround()
    {
        Vector3 startVec = transform.position - (transform.up * ParamsSO.Entity.playerDistanceToGround);
        Vector3 endVec = startVec - transform.up * 0.1f;
        Debug.DrawLine(startVec, endVec, Color.red);

        return Physics2D.Linecast(startVec, endVec, groundLayer);
    }

    void OnTriggerEnter2D(Collider2D collision)
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
            SoundManager.instance.PlaySE(SoundManager.SE.Drink);
            // アイテムゲット
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isFinish)
            {
                return;
            }
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();

            if (this.transform.position.y - ParamsSO.Entity.playerDistanceToEnemy[(int)enemy.enemyType] > enemy.transform.position.y)
            {
                // 上から敵を踏んだらプレイヤーをジャンプさせる
                rb.velocity = new Vector2(rb.velocity.x, 0);
                Jump();
                enemy.DestroyEnemy();
            }
            else
            {
                if (isDamage)
                {
                    SoundManager.instance.PlaySE(SoundManager.SE.Damage);
                    return;
                }
                // ぶつかったらダメージを受ける(敵ごとに受けるダメージ量が違う)
                StartCoroutine(OnDamage(collision.gameObject, ParamsSO.Entity.playerDamege[(int)enemy.enemyType]));

                // 敵にぶつかったら水分ゲージも減らす
                gameManager.currentWaterValue -= ParamsSO.Entity.waterDamage;
                gameManager.waterGauge.fillAmount = gameManager.currentWaterValue / gameManager.maxWaterValue;
                // Debug.Log($"水分：{gameManager.currentWaterValue}");

                if (currentHp <= 0 || gameManager.currentWaterValue <= 0)
                {
                    // 体力か水分が0になったらゲームオーバー
                    PlayerDead();
                }
            }
        }
    }

    /// <summary>
    /// ジャンプする
    /// </summary>
    void Jump()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Jump);
        rb.AddForce(Vector2.up * ParamsSO.Entity.playerJump);
        animator.SetTrigger("jump");
    }

    /// <summary>
    /// ステージクリアの処理
    /// </summary>
    void Finish()
    {
        isFinish = true;
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlaySE(SoundManager.SE.Clear);
        rb.velocity = new Vector2(0, 0);
        animator.SetFloat("speed", Mathf.Abs(0));
        // Debug.Log("ステージクリア");
        gameManager.StageClear();
    }

    /// <summary>
    /// ゲームオーバーの処理
    /// </summary>
    public void PlayerDead()
    {
        isDead = true;
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlaySE(SoundManager.SE.Gameover);
        animator.SetTrigger("die");
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.up * ParamsSO.Entity.playerJump);

        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Destroy(capsuleCollider2D);
        gameManager.GameOver();
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    IEnumerator OnDamage(GameObject enemy, float damage)
    {
        isDamage = true;
        // Debug.Log(isDamage);
        SoundManager.instance.PlaySE(SoundManager.SE.Damage);
        // Hitアニメーションに切り替える
        animator.SetTrigger("hit");

        // 一旦移動を止める 
        rb.velocity = Vector2.zero;
        animator.SetFloat("speed", Mathf.Abs(0));

        // 敵と反対方向にノックバック
        Vector3 v = (transform.position - enemy.transform.position).normalized;
        rb.AddForce(new Vector2(v.x * 2, 0), ForceMode2D.Impulse);

        // HPを減らす
        currentHp -= damage;
        hpGauge.fillAmount = currentHp / maxHp;
        // Debug.Log($"HP：{currentHp}");

        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;

        isDamage = false;
        // Debug.Log(isDamage);
    }

    public static float GetHP()
    {
        return currentHp;
    }
}
