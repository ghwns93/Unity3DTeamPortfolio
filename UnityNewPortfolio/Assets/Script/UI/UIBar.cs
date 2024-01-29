using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Slider hp_Bar;
    public Slider sp_Bar;

    void Update()
    {
        hp_Bar.value = PlayerState.Instance.Hp / PlayerState.Instance.HpMax;
        sp_Bar.value = PlayerState.Instance.Stamina / PlayerState.Instance.StaminaMax;
    }
}
