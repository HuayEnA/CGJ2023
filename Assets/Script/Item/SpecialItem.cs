using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SpecialItem:MonoBehaviour
{

    [SerializeField]protected bool isRun;

    [Tooltip("未被拾取的图标")]public Sprite ImageinRun;
    [Tooltip("拾取的图标")] public Sprite ImageinRest;


    private void OnEnable()
    {
        isRun = false;
    }
    protected virtual void EnableItem()
    {
        isRun = true;
    }

    


}
