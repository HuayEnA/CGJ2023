using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandle : MonoBehaviour
{
    private Vector3 originPos;

    private void Awake()
    {
        originPos = transform.position;
    }
    private void OnEnable()
    {
        transform.position = originPos;
        transform.rotation = Quaternion.identity;
    }


    public void StartMove()
    {
        GetComponent<Rigidbody2D>().simulated = true;
    }
}
