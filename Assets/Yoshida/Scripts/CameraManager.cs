using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("��ʒ[(��)")]
    [SerializeField] float leftLimit = default;

    [Header("��ʒ[(�E)")]
    [SerializeField] float rightLimit = default;

    void Start()
    {
    }

    void Update()
    {
        // Player��T��
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // �J�������W�̍X�V
            float x = player.transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;

            // ���[�Ɉړ�����������
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }

            // �J�����ʒu�̐ݒ�
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;
        }
    }
}
