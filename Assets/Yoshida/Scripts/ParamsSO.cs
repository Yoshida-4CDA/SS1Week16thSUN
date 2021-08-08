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

    [Header("水分ゲージの最大値")]
    [Range(1, 100)]
    public int waterGaugeMaxValue;

    [Header("水分ゲージの減少量")]
    public float waterThirstyValue;

    [Header("アイテムの回復量")]
    public int recoverValue;

    [Header("敵の移動スピード")]
    public float enemySpeed;

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
