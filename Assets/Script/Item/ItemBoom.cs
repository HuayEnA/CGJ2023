using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoom : MonoBehaviour
{
    public GameObject BoomEffect;
    public float TimePause;
    public float ShakeTime;
    public float ShakeStrength;
    private Item _item;
    public void EnableBoomEffect()
    {
        AttackSense.Instance.HitPause(TimePause);
        CameraShake.instance.CameraShake_CM(ShakeTime, ShakeStrength);
        if (_item == null)
        {
            _item = transform.parent.GetComponent<Item>();
        }
        _item.Boom();


        BoomEffect.SetActive(true);
    }
}
