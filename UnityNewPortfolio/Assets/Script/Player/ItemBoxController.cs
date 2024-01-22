using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxController : MonoBehaviour
{
    public GameObject dropItem;
    public Canvas canvas;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            canvas.enabled = false;
        }
    }
}
