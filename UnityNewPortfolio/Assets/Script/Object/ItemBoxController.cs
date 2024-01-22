using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ItemBoxController : MonoBehaviour
{
    public Item dropItem;
    public Canvas canvas;

    GameObject target;
    Text text;

    private void Start()
    {
        Instantiate(dropItem.itemPrefab, transform);

        text = canvas.transform.Find("Name").GetComponent<Text>();

        target = GameObject.FindGameObjectWithTag("MainCamera");

        text.text = dropItem.itemName;
        canvas.enabled = false;
    }

    private void Update()
    {
        canvas.transform.forward = target.transform.forward;

        if(canvas.enabled)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Inventory.Instance.AddItem(dropItem);
                Destroy(gameObject);
            }
        }
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
