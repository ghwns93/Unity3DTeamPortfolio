using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatText : MonoBehaviour
{
    public Text text_lv;    // ����
    public Text text_hp;    // ü��
    public Text text_sp;    // ���¹̳�
    //public Text text_atk;   // ���ݷ�
    //public Text text_def;   // ����
    //public Text text_cri;   // ġ��Ÿ��
    //public Text text_agi;   // ȸ����
    //public Text text_as;    // ���ݼӵ�
    //public Text text_ms;    // �̵��ӵ�
    public Text text_gold;  // ��

    void LateUpdate()
    {
        text_lv.text = PlayerState.Instance.Level.ToString();
        text_hp.text = PlayerState.Instance.Hp.ToString();
        text_sp.text = PlayerState.Instance.Stamina.ToString();
        text_gold.text = PlayerState.Instance.Money.ToString() + " G";
    }
}
