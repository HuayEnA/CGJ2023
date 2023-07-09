using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMouse : MonoBehaviour
{
    [SerializeField, Header("道具参数")] private float boomTime;//激活后多少秒爆炸
    [SerializeField] private float hitTime;
    [SerializeField] private float boomForce;//爆炸冲击力
    [SerializeField, Tooltip("在爆炸范围内的玩家")] public List<GameObject> players = new List<GameObject>();

    [SerializeField] public bool beGrab;

    [Header("爆炸参数")]
    public GameObject BoomEffect;
    public float TimePause;
    public float ShakeTime;
    public float ShakeStrength;

    private bool isRun;
    private Rigidbody2D rb;

    public bool isBoom;
    private float speed;
    private bool canExecute;
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
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        speed += Time.deltaTime * 20f;
        boomTime -= Time.deltaTime;
        if (boomTime <= 0&&!isBoom)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        isRun = true;
        Invoke("SetCanExecute", 0.2f);
    }
    private void OnEnable()
    {
        isBoom = false;
    }

    private void FixedUpdate()
    {
        if (isRun) 
            rb.velocity = new Vector2(transform.localScale.x * speed, rb.velocity.y);

    }

    public void Boom()
    {
        rb.velocity = Vector2.zero;
        transform.GetChild(0).DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.1f);
        AttackSense.Instance.HitPause(TimePause);
        CameraShake.instance.CameraShake_CM(ShakeTime, ShakeStrength);
        BoomEffect.SetActive(true);
        isBoom = true;
        foreach (GameObject player in players)
        {
            Vector2 dir = player.transform.position - transform.position;
            player.GetComponent<ActorControl>().BeBlownUp(dir.normalized * boomForce, hitTime);
        }
        isRun = false;
    }

    public void SetCanExecute()
    {
        canExecute = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Add(collision.gameObject);
            if (!isBoom && canExecute)
                Boom();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Debug.Log("ag");
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