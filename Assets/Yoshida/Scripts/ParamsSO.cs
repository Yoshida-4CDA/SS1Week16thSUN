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

    [Header("�G�𓥂߂邩�ǂ����̔��苗��")]
    [Tooltip("0 = �~�C��, 1 = �T�\��, 2 = �w�r")]
    public float[] playerDistanceToEnemy;

    [Header("�n�ʂ܂ł̔��苗��(�v���C���[)")]
    public float playerDistanceToGround;

    [Header("�v���C���[��MaxHP")]
    [Range(1, 100)]
    public int maxHpGaugeValue;

    [Header("�G����󂯂�_���[�W")]
    [Tooltip("0 = �~�C��, 1 = �T�\��, 2 = �w�r")]
    public float[] playerDamege;

    [Header("�����Q�[�W�̍ő�l")]
    [Range(1, 100)]
    public int maxWaterGaugeValue;

    [Header("�����Q�[�W�̌�����")]
    public float waterThirstyValue;

    [Header("�A�C�e���̉񕜗�")]
    public int recoverValue;

    [Header("�G�̈ړ��X�s�[�h")]
    [Tooltip("0 = �~�C��, 1 = �T�\��, 2 = �w�r")]
    public float[] enemySpeed;

    [Header("�G�̃T�C�Y")]
    [Tooltip("0 = �~�C��, 1 = �T�\��, 2 = �w�r")]
    public float[] enemyScale;

    [Header("�n�ʂ܂ł̔��苗��(�G)")]
    [Tooltip("0 = �~�C��, 1 = �T�\��, 2 = �w�r")]
    public float[] enemyDistanceToGround;

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
