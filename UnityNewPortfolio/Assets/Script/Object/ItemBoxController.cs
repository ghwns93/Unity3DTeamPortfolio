using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ItemBoxController : MonoBehaviour
{
    public GameObject dropItem;
    public Canvas canvas;

    ObjectState objectState;
    GameObject target;
    Text text;

    private void Start()
    {
        Instantiate(dropItem, transform);

        objectState = dropItem.GetComponent<ObjectState>();
        text = canvas.transform.Find("Name").GetComponent<Text>();

        target = GameObject.FindGameObjectWithTag("MainCamera");

        text.text = objectState.name;
        canvas.enabled = false;
    }

    private void Update()
    {
        canvas.transform.forward = target.transform.forward;
    }

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
