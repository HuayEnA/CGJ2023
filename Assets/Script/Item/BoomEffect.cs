using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class BoomEffect : MonoBehaviour
{

    private Item _item;



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            if (_item == null)
            {
                _item = GetComponentInParent<Item>();
            }
            Vector2 dir = collision.transform.position - transform.position;
            collision.GetComponent<ActorControl>().BeBlownUp(dir.normalized * _item.BoomForce, _item.HitTime);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
