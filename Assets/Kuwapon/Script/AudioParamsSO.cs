using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AudioParamsSO : ScriptableObject
{
    [Header("BGM音量:再生前に変更しないと反映されない")]
    [Range(0, 1)]
    public float BGMVolume;

    [Header("SEの音量の上限")]
    [Range(0, 1)]
    public float SEVolume;

    [Header("BGMの音量")]
    [Range(0, 1)]
    public float AmachanThemeVolume;
    [Range(0, 1)]
    public float DesertNoonVolume;
    [Range(0, 1)]
    public float DesertNightVolume;
    [Range(0, 1)]
    public float CityNoonVolume;
    [Range(0, 1)]
    public float CityNightVolume;
    [Range(0, 1)]
    public float TempleVolume;

    [Header("SEの音量")]
    [Range(0, 1)]
    public float DamageVolume;
    [Range(0, 1)]
    public float ClearVolume;
    [Range(0, 1)]
    public float JumpVolume;
    [Range(0, 1)]
    public float DrinkVolume;
    [Range(0, 1)]
    public float AttackVolume;
    [Range(0, 1)]
    public float ClickVolume;
    [Range(0, 1)]
    public float GameoverVolume;


    public float GetVolume(SoundManager.BGM bgm)
    {
        switch (bgm)
        {
            case SoundManager.BGM.AmachanTheme:
                return AmachanThemeVolume * BGMVolume;
            case SoundManager.BGM.DesertNoon:
                return DesertNoonVolume * BGMVolume;
            case SoundManager.BGM.DesertNight:
                return DesertNightVolume * BGMVolume;
            case SoundManager.BGM.CityNoon:
                return CityNoonVolume * BGMVolume;
            case SoundManager.BGM.CityNight:
                return CityNightVolume * BGMVolume;
            case SoundManager.BGM.Temple:
                return TempleVolume * BGMVolume;


        }
        return 0;
    }


    public float GetVolume(SoundManager.SE se)
    {
        switch (se)
        {
            case SoundManager.SE.Damage:
                return DamageVolume * SEVolume;
            case SoundManager.SE.Clear:
                return ClearVolume * SEVolume;
            case SoundManager.SE.Jump:
                return JumpVolume * SEVolume;
            case SoundManager.SE.Drink:
                return DrinkVolume * SEVolume;
            case SoundManager.SE.Attack:
                return AttackVolume * SEVolume;
            case SoundManager.SE.Click:
                return ClickVolume * SEVolume;
            case SoundManager.SE.Gameover:
                return GameoverVolume * SEVolume;
            
        }
        return 0;
    }

    public const string PATH = "AudioParamsSO";
    private static AudioParamsSO _entity;
    public static AudioParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<AudioParamsSO>(PATH);

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
