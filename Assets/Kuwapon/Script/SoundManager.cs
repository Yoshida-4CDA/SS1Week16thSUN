using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource = default;
    [SerializeField] AudioSource seAudioSource = default;
    [SerializeField] AudioClip[] bgmClips = default;
    [SerializeField] AudioClip[] seClips = default;
    BGM currentBgm;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum BGM
    {
        AmachanTheme,//
        DesertNoon,
        DesertNight,
        CityNoon,
        CityNight,
        Temple,
    }

    public enum SE
    {
        Damage,
        Clear,
        Jump,
        Drink,
        Attack,
        Click,  //
        Gameover,
            
    }


    public void SetBGMVolume(float volume)
    {
        AudioParamsSO.Entity.BGMVolume = volume;
        bgmAudioSource.volume = AudioParamsSO.Entity.GetVolume(currentBgm);
        
    }
    public void SetSEVolume(float volume)
    {
        seAudioSource.volume = volume;
        AudioParamsSO.Entity.SEVolume = volume;
    }

    /*public float GetBGMVolume()
    {
        return bgmAudioSource.volume;
    }*/



    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
        


    public void PlayBGM(BGM bgm)
    {
        int index = (int)bgm;
    currentBgm = bgm;
    bgmAudioSource.volume = AudioParamsSO.Entity.GetVolume(currentBgm);
    bgmAudioSource.clip = bgmClips[index];
    bgmAudioSource.Play();
    Debug.Log(bgmAudioSource.volume);

    }

    public void PlaySE(SE se)
    {
        int index = (int)se;
        seAudioSource.volume = AudioParamsSO.Entity.GetVolume(se);
        seAudioSource.PlayOneShot(seClips[index]);
    }
        

    public bool IsPlaySE()
    {
        return seAudioSource.isPlaying;
    }


}

