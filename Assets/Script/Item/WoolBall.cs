using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WoolBall : MonoBehaviour
{
    [SerializeField] private bool isRun;
    [SerializeField] private float tieTime=2;
    [SerializeField] private bool hasPlayer;

    private void OnEnable()
    {
        StartCoroutine(RunItem());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hasPlayer)
        {
            return;
        }
        if (collision.tag == "Player")
        {
            if (isRun)
            {
                hasPlayer = true;
                collision.GetComponent<ActorControl>().BeTie(tieTime);
                StartCoroutine(EndTie());
            }
        }
    }

    IEnumerator RunItem()
    {
        yield return new WaitForSeconds(2f);
        isRun = true;
    }
    IEnumerator EndTie()
    {
        yield return new WaitForSeconds(tieTime);
        Destroy(gameObject);
    }

}
