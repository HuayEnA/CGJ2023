using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolBallInCommon : ItemInCommon
{
    public override void CreateItem(Transform pos)
    {

        var obj = Instantiate(Item);
        obj.transform.position = pos.position - Vector3.up * 0.5f;
        Destroy(gameObject);
    }
}
