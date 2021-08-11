using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    

    public void BGM01()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.AmachanTheme);
    }
    public void BGM02()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.DesertNoon);
    }
    public void BGM03()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.DesertNight);
    }
    public void BGM04()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.CityNoon);
    }
    public void BGM05()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.DesertNight);
    }
    public void BGM06()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Temple);
    }
    public void SE01()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Damage);
    }
    public void SE02()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Clear);
    }

    public void SE03()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Jump);
    }
    public void SE04()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Drink);
    }
    public void SE05()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Attack);
    }
    public void SE06()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Click);
    }
    public void SE07()
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Gameover);
    }
}
