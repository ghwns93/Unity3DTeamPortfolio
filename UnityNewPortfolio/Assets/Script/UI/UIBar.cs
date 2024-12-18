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
        if (hp_Bar != null)
        {
            hp_Bar.value = PlayerState.Instance.Hp / PlayerState.Instance.HpMax;
        }

        if (sp_Bar != null)
        {
            sp_Bar.value = PlayerState.Instance.Stamina / PlayerState.Instance.StaminaMax;
        }
    }
}
