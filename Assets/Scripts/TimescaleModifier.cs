using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimescaleModifier : MonoBehaviour {

    Slider m_slider;

    private void Awake()
    {
        m_slider = GetComponent<Slider>();
        m_slider.onValueChanged.AddListener(data => { OnSliderChange(data); });
    }

    void OnSliderChange(float _val)
    {
        Time.timeScale = _val/10f;
    }
}
