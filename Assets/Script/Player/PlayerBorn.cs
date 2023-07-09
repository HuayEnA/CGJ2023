using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerBorn : MonoBehaviour
{
    public ActorControl ac;
    public BulletTimeObject bto;
    [SerializeField] private int playerId;//选择的第n个角色


    private void Awake()
    {
        ReBorn();
    }

    private void Start()
    {
        CameraControl.instance.players.Add(gameObject);
        UIManager.instance.OpenPlayerHPUI(playerId, ac);
    }

    public void ReBorn()
    {
        int num = GameUtility.instance.bornPoints.Count;
        int index = UnityEngine.Random.Range(0, num);
        transform.position = GameUtility.instance.bornPoints[index].transform.position;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        gameObject.SetActive(true);
        Invoke("StartMove", 0.5f);
    }
    public void StartMove()
    {
        if (ac == null)
        {
            ac = GetComponent<ActorControl>();
        }
        if (bto == null)
        {
            bto = GetComponent<BulletTimeObject>();
        }

        ac.enabled = true;
        bto.enabled = true;
    }
}
