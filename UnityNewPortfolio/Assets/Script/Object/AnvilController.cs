using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnvilController : MonoBehaviour
{
    public Canvas canvas;
    GameObject target;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = canvas.transform.Find("Name").GetComponent<Text>();

        target = GameObject.FindGameObjectWithTag("MainCamera");

        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.forward = target.transform.forward;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
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
