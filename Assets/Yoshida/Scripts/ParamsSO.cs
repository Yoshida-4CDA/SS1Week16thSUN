using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("�v���C���[�̈ړ��X�s�[�h")]
    public float playerSpeed;

    [Header("�v���C���[�̃W�����v��")]
    public float playerJump;


    // ParamsSO���ۑ����Ă���ꏊ�̃p�X
    public const string PATH = "ParamsSO";

    // ParamsSO�̎���
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //���A�N�Z�X���Ƀ��[�h����
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //���[�h�o���Ȃ������ꍇ�̓G���[���O��\��
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}