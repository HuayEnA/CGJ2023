using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabItem : MonoBehaviour
{

    [SerializeField] public string grabKey;
    [SerializeField] public List<GameObject> _items;
    [SerializeField] public GameObject cur_GrabItem;
    public bool inGrab;


    private void Update()
    {


        if (Input.GetKeyDown(grabKey))
        {
            GetGrabItem();
            if (cur_GrabItem != null)
            {
                inGrab = true;
            }
        }
        if (Input.GetKeyUp(grabKey))
        {
            if (cur_GrabItem != null)
            {
                cur_GrabItem = null;
                inGrab = false;
            }
        }

        if (inGrab == false && cur_GrabItem != null)
        {
            cur_GrabItem = null;
        }

        if (inGrab && cur_GrabItem != null)
        {
            if (cur_GrabItem.GetComponent<Item>().isBoom)
            {
                cur_GrabItem = null;
                inGrab = false;
                return;
            }
            cur_GrabItem.transform.position = transform.position;
        }

    }

    
    public void ClearCur_GrabItem()
    {
        if (cur_GrabItem != null)
        {
            cur_GrabItem = null;
            inGrab = false;
        }
    }




    public void GetGrabItem()
    {
        foreach(GameObject _item in _items)
        {
           
            if (!_item.GetComponent<Item>().beGrab)
            {
                cur_GrabItem = _item;
                return;
            }
        }

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            _items.Add(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            _items.Remove(collision.gameObject);
        }
    }






}
