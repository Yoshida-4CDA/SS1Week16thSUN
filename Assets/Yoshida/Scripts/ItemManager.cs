using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        // ヒエラルキー上からGameManagerを見つけてGameManagerコンポーネントを取得してgameManagerに代入
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
    }

    public void GetItem()
    {
        if (this.gameObject.CompareTag("Item"))
        {
            Debug.Log("アイテムゲット");
            gameManager.AddWaterValue(ParamsSO.Entity.recoverValue);
            Destroy(gameObject);
        }
    }
}
