using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    public List<GameObject> itemList = new List<GameObject>();


    private void Start()
    {
        StartCoroutine(CreateItem());
    }




    IEnumerator CreateItem()
    {
        yield return new WaitForSeconds(10);
        int num = itemList.Count;
        int index = Random.Range(0, num);

        int num2 = points.Count;

        int index2 = Random.Range(0, num2);


        var obj = Instantiate(itemList[index]);
        obj.transform.position = points[index2].position;
        StartCoroutine(CreateItem());
    }



}
