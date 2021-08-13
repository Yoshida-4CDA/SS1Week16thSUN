using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBGM : MonoBehaviour
{
    public GameObject soundUIPanel;
    // Start is called before the first frame update
    void Start()
    {
        // soundUIPanel.SetActive(false);
        // SoundManager.instance.PlayBGM(SoundManager.BGM.AmachanTheme);
    }

    public void ShowPanel()
    {
        soundUIPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        soundUIPanel.SetActive(false);
    }


}
