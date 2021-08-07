using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
    }

    /// <summary>
    /// アイテムゲット時の処理
    /// </summary>
    public void GetItem()
    {
        if (this.gameObject.CompareTag("Item"))
        {
            // 水分ゲージを回復する
            Debug.Log("アイテムゲット");
            gameManager.RecoverWaterValue(ParamsSO.Entity.recoverValue);
            Destroy(gameObject);
        }
    }
}
