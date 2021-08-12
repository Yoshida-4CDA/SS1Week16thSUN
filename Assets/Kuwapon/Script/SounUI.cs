using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class SounUI : MonoBehaviour
{
    [SerializeField] GameObject panel = default;

    [SerializeField] Slider bgmSlider = default;
    [SerializeField] Slider seSlider = default;

    private void Start()
    {
        bgmSlider.value = AudioParamsSO.Entity.BGMVolume;
        seSlider.value = AudioParamsSO.Entity.SEVolume;

        bgmSlider.onValueChanged.AddListener(volume => SoundManager.instance.SetBGMVolume(volume));
        seSlider.onValueChanged.AddListener(volume => SoundManager.instance.SetSEVolume(volume));
    }


    public void Show()
    {
       panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

}
