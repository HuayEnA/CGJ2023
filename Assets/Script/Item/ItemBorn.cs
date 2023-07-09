using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBorn : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        transform.DOScale(new Vector3(1, 1, 1), 0.5f);

    }

}
