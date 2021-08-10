using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("プレイヤーの移動スピード")]
    public float playerSpeed;

    [Header("プレイヤーのジャンプ力")]
    public float playerJump;

    [Header("敵を踏めるかどうかの判定距離")]
    [Tooltip("0 = ミイラ, 1 = サソリ, 2 = ヘビ, 3 = ネコ")]
    public float[] playerDistanceToEnemy;

    [Header("地面までの判定距離(プレイヤー)")]
    public float playerDistanceToGround;

    [Header("プレイヤーのMaxHP")]
    [Range(1, 100)]
    public int maxHpGaugeValue;

    [Header("敵から受けるダメージ")]
    [Tooltip("0 = ミイラ, 1 = サソリ, 2 = ヘビ, 3 = ネコ")]
    public float[] playerDamege;

    [Header("水分ゲージの最大値")]
    [Range(1, 100)]
    public int maxWaterGaugeValue;

    [Header("水分ゲージの減少量")]
    public float waterThirstyValue;

    [Header("アイテムの回復量")]
    public int recoverValue;

    [Header("敵の移動スピード")]
    [Tooltip("0 = ミイラ, 1 = サソリ, 2 = ヘビ, 3 = ネコ")]
    public float[] enemySpeed;

    [Header("敵のサイズ")]
    [Tooltip("0 = ミイラ, 1 = サソリ, 2 = ヘビ, 3 = ネコ")]
    public float[] enemyScale;

    [Header("地面までの判定距離(敵)")]
    [Tooltip("0 = ミイラ, 1 = サソリ, 2 = ヘビ, 3 = ネコ")]
    public float[] enemyDistanceToGround;

    // ParamsSOが保存してある場所のパス
    public const string PATH = "ParamsSO";

    // ParamsSOの実体
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}
