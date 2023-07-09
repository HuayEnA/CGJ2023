using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInCommon : MonoBehaviour
{

    public GameObject Item;
    public float speed;
    int flag = 0;
    public Sprite Image;


    public virtual void CreateItem(Transform pos)
    {

        var obj = Instantiate(Item);
        obj.transform.position = pos.position - Vector3.up * 0.5f;
        obj.transform.SetLocalScaleX(-pos.localScale.x);
        Destroy(gameObject);
    }

    protected void Update()
    {
        if (transform.localScale.x>=1f)
        {
            flag = -1;
            
        }
        else if (transform.localScale.x<=0.8f)
        {
            flag = 1;
            

        }
        if (flag == -1)
        {
            transform.SetLocalScaleX(transform.localScale.x - Time.deltaTime * speed);
            transform.SetLocalScaleY(transform.localScale.y - Time.deltaTime * speed);
        }
        else if (flag == 1)
        {
            transform.SetLocalScaleX(transform.localScale.x + Time.deltaTime * speed);
            transform.SetLocalScaleY(transform.localScale.y + Time.deltaTime * speed);
        }
    }
}
