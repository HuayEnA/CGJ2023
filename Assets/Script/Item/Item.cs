using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{


    [SerializeField, Header("道具参数")] private float boomTime;//激活后多少秒爆炸
    [SerializeField] private float hitTime;
    [SerializeField] private float boomForce;//爆炸冲击力
    [SerializeField,Tooltip("在爆炸范围内的玩家")]public List<GameObject> players = new List<GameObject>();

    [SerializeField]private Animator anim;
    [SerializeField]public bool beGrab;
    public bool isBoom;
    public float HitTime
    {
        get => hitTime;
    }
    public float BoomForce
    {
        get => boomForce;
    }
    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isBoom = false;
    }

    private void Update()
    {
    }



    /// <summary>
    /// 被摸到后激活
    /// </summary>
    public virtual void BeEnable()
    {
        StartCoroutine(ExecuteItem());
    }

    public virtual IEnumerator ExecuteItem()
    {
        yield return new WaitForSeconds(boomTime);
        if(anim==null)
            anim = transform.GetChild(0).GetComponent<Animator>();
        
        anim.SetTrigger("run");

    }


    public void Boom()
    {
        isBoom = true;
        foreach (GameObject player in players)
        {
            Vector2 dir = player.transform.position - transform.position;
            player.GetComponent<ActorControl>().BeBlownUp(dir.normalized * boomForce, hitTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Add(collision.gameObject);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Remove(collision.gameObject);
        }
    }

}
