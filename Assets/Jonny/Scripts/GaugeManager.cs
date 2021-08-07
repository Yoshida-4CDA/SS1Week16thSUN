using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    public Image _waterGauge;
    public Image _hpGauge;

    [SerializeField] private int _wMaxHp, _wHp;
    [SerializeField] private int _maxHp, _hp;

    void Start()
    {
        _wMaxHp = 100;
        _maxHp = 100;

        _wHp = _wMaxHp;
        _hp = _maxHp;
    }

    void Update()
    {
        _waterGauge.fillAmount = (float)_wHp / _wMaxHp;
        _hpGauge.fillAmount = (float)_hp / _maxHp;
    }
}
