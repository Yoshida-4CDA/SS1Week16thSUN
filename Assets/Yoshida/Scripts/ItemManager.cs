using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        // �q�G�����L�[�ォ��GameManager��������GameManager�R���|�[�l���g���擾����gameManager�ɑ��
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
    }

    public void GetItem()
    {
        if (this.gameObject.CompareTag("Item"))
        {
            Debug.Log("�A�C�e���Q�b�g");
            gameManager.AddWaterValue(ParamsSO.Entity.recoverValue);
            Destroy(gameObject);
        }
    }
}
