using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SpecialItem:MonoBehaviour
{

    [SerializeField]protected bool isRun;

    [Tooltip("δ��ʰȡ��ͼ��")]public Sprite ImageinRun;
    [Tooltip("ʰȡ��ͼ��")] public Sprite ImageinRest;


    private void OnEnable()
    {
        isRun = false;
    }
    protected virtual void EnableItem()
    {
        isRun = true;
    }

    


}
