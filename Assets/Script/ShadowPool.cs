using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowPool : MonoBehaviour
{
    Queue<GameObject> shadowPool = new Queue<GameObject>();
    public GameObject shadowsPrefab;
    public int shadowNumber;
    public static ShadowPool instance;

    private void Awake()
    {
        instance = this;
        FillShadowPool();
    }

    public void FillShadowPool()
    {
        for(int i = 0; i < shadowNumber; i++)
        {
            var shadow = Instantiate(shadowsPrefab);
            shadow.transform.SetParent(transform);
            ReturnToShadowPool(shadow);
        }
    }

    public void ReturnToShadowPool(GameObject shadow)
    {
        shadowPool.Enqueue(shadow);
        shadow.SetActive(false);
    }

    public void TakeFromShadowPool(Transform player)
    {
        if (shadowPool.Count <= 0)
        {
            FillShadowPool();
        }
        if(shadowPool.Count > 0)
        {

            var shadow = shadowPool.Dequeue();
            shadow.GetComponent<Shadow>().playerTransform = player;
            shadow.SetActive(true);
        }
    }

}
