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
    /// �A�C�e���Q�b�g���̏���
    /// </summary>
    public void GetItem()
    {
        if (this.gameObject.CompareTag("Item"))
        {
            // �����Q�[�W���񕜂���
            Debug.Log("�A�C�e���Q�b�g");
            gameManager.RecoverWaterValue(ParamsSO.Entity.recoverValue);
            Destroy(gameObject);
        }
    }
}
