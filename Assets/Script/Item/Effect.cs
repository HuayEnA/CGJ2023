using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public void DisableEffect()
    {
        gameObject.SetActive(false);
        
    }

    public void DisableObj()
    {
        gameObject.SetActive(false);
        transform.parent.parent.gameObject.SetActive(false);
        transform.parent.parent.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        Invoke("InitGameObj", GameUtility.instance.ItemRecoverTime);
    }

    public void InitGameObj()
    {
        transform.parent.parent.gameObject.SetActive(true);
    }

}
