using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.CompilerServices;

public class ActorControl : MonoBehaviour
{
    [SerializeField, Header("��������")] private string key_Jump;
    [SerializeField] private string key_Dash;
    [SerializeField] private string key_Attack;
    [SerializeField] private string key_Skill;

    


    [SerializeField, Header("��������")] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int hpCount;//����ֵ




    public int _HpCount => hpCount;//����ֵ


    [SerializeField] private GroundSensor _groundSensor;
    private Rigidbody2D _rigidbody;
    private Animator anim;
    private Collider2D coll;

    private ActorInputAction _inputAction;
    private BulletTimeObject _BulletTimeObject;

    public GameObject cur_Item;//��ǰ����

    #region ״̬
    [SerializeField, Header("״̬")] private bool canDashing;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool isHit;

    [SerializeField] private bool canAttack;
    [SerializeField] private bool canJumpDown => moveVec.y == -1 && _groundSensor.isPlatform;
    [SerializeField] private Vector2 boomVelocity;
    [SerializeField] private bool inWoolBall;
    #endregion



    #region ����

    [SerializeField]private LayerMask hitLayer;


    [SerializeField] private List<Item> _items = new List<Item>();//��Χ�ڵĵ���
    private Vector2 moveVec => _inputAction.KeyboardInput.Move.ReadValue<Vector2>();
    private Vector2 dashVec;




    [SerializeField,Header("����")]private Vector2 createShadowTime=new Vector2(0,2f);
    #endregion

    #region ����в���
    [SerializeField,Header("����в���")]private float DashShakeTime=0.15f;
    [SerializeField] private float DashShakeStrength = 0.4f;
    [SerializeField] private float DashTimePause = 0.1f;
    
    #endregion

    private void Awake()
    {
        _inputAction ??= new ActorInputAction();
        _BulletTimeObject ??= GetComponent<BulletTimeObject>();
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        
        canDashing = true;
        canAttack = true;

        

    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }



    private void Update()
    {
        KeyInput();
        SwitchAnim();
        CheackCanStopHit();
        _groundSensor.Update();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isDashing&&!isHit&&!inWoolBall) 
        {
            _rigidbody.velocity = new Vector2(moveVec.x * speed, _rigidbody.velocity.y);
            //Vector2 targetVelocity = new Vector2(moveVec.x * speed, _rigidbody.velocity.y);
            //_rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, targetVelocity, 8f*Time.fixedDeltaTime);
            //_rigidbody.MovePosition(new Vector2(moveVec.x * speed*Time.deltaTime, _rigidbody.velocity.y)
        }

    }

    private void SwitchAnim()
    {
        if (moveVec.x != 0)
            transform.SetLocalScaleX(-moveVec.x);
        anim.SetFloat("velocityX", Mathf.Abs(_rigidbody.velocity.x));
        anim.SetFloat("velocityY", _rigidbody.velocity.y);

    }

    #region �������

    private void KeyInput()
    {
        if (Input.GetKeyDown(key_Jump)&&!inWoolBall)
        {
            if (canJumpDown)
            {
                coll.enabled = false;
                StartCoroutine(RecoverColl());
            }
            else
            {
                Jump();
            }
        }
        if (Input.GetKeyDown(key_Dash)&&canDashing && !inWoolBall)
        {
            Dash();
        }

        if (Input.GetKeyDown(key_Attack)&&canAttack)
        {
            anim.SetTrigger("touch");
            if (_items.Count > 0)
            {
                _items[0].BeEnable();
                _items.Remove(_items[0]);
            }
        }

        if (Input.GetKeyDown(key_Skill)&&cur_Item!=null)
        {
            cur_Item.GetComponent<ItemInCommon>().CreateItem(transform);
            cur_Item = null;
            UIManager.instance.UpdateUI();
        }


    }

    #endregion

    private void Jump()
    {
        anim.SetTrigger("jump");
    }

    IEnumerator RecoverColl()
    {
        yield return new WaitForSeconds(0.4f);
        coll.enabled = true;
    }

    private void Dash()
    {
        isHit = false;
        dashVec = moveVec.normalized;
        anim.SetTrigger("dash");
    }

    public void BeBlownUp(Vector2 force,float hitTime)
    {
        _rigidbody.AddForce(force);
        //boomVelocity = force;
        //Debug.Log(boomVelocity);
        anim.SetTrigger("hit");
        isHit = true;
        StartCoroutine(EndHitTime(hitTime));
    }

    IEnumerator EndHitTime(float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        isHit = false;


    }

    private void CheackCanStopHit()
    {
        if (isHit)
        {
            if (_rigidbody.IsTouchingLayers(hitLayer))
            {
                isHit = false;
            }
        }
    }

    /// <summary>
    /// ������ʱ�������
    /// </summary>
    public void BeTie(float _time)
    {
        inWoolBall = true;
        _rigidbody.velocity = Vector2.zero;
        StartCoroutine(StopTie(_time));
    }


    IEnumerator StopTie(float _time)
    {
        yield return new WaitForSeconds(_time);
        inWoolBall = false;
    }

    public void OutOfBound()
    {
        hpCount--;
        if (hpCount <= 0)
        {
            Dead();
            
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            

            GetComponent<BulletTimeObject>().enabled = false;
            this.enabled = false;
            
            AttackSense.Instance.HitPause(5f);
            CameraShake.instance.CameraShake_CM(0.2f, 20);
            Invoke("SelfActiveFalse", 0.1f);

            Invoke("ReBorn", 2f);
        }
        UIManager.instance.UpdateUI();
    }


    public void ClearOnCamera()
    {
        CameraControl.instance.players.Remove(this.gameObject);
    }

    public void SelfActiveFalse()
    {
        SceneEffectManager.instace.deadFx.transform.position = transform.position;

        SceneEffectManager.instace.deadFx.Play();
        
        gameObject.SetActive(false);
    }

    public void ReBorn()
    {
        GetComponent<PlayerBorn>().ReBorn();

    }

    public void Dead()
    {

        _rigidbody.velocity = Vector2.zero;
        GetComponent<BulletTimeObject>().enabled = false;
        this.enabled = false;

        AttackSense.Instance.HitPause(5f);
        CameraShake.instance.CameraShake_CM(0.2f, 20);
        Invoke("SelfActiveFalse", 0.1f);

        UIManager.instance.UpdateUI();
    }


    #region ����״̬��
    public void OnJumpEnter()
    {
        _rigidbody.AddForce(jumpForce * Vector2.up);
        SceneEffectManager.instace.Jump(transform.position);
    }

    //�ڿ�����δ����ʵ��ʱ���е�����
    public void OnJumpUpdate()
    {
        if (_groundSensor.isGround)
        {
            anim.SetTrigger("fall");
        }
    }

    public void OnDashEnter()
    {
        SceneEffectManager.instace.Jump(transform.position);

        _BulletTimeObject.DisablePhysicsSimulation();
        _rigidbody.velocity = Vector2.zero;
        canDashing = false;
        isDashing = true;

        _rigidbody.velocity += dashSpeed * dashVec;
        Debug.Log(dashSpeed * dashVec);
        AttackSense.Instance.HitPause(DashTimePause);
        CameraShake.instance.CameraShake_CM(DashShakeTime, DashShakeStrength);
        StartCoroutine(StopDash());

    }
    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(.15f);
        _rigidbody.velocity = Vector2.zero;
    }
    public void OnShadowEnableUpdate()
    {

        if (createShadowTime.x < 0)
        {
            createShadowTime.x = createShadowTime.y;
            ShadowPool.instance.TakeFromShadowPool(transform);
        }
        createShadowTime.x -= Time.deltaTime;
    }
    public void OnDashExit()
    {
        isDashing = false;

        _BulletTimeObject.EnablePhysicsSimulation();
    }

    public void OnGroundEnter()
    {
        SceneEffectManager.instace.Land(transform.position);
        canDashing = true;
    }


    public void OnTouchEnter()
    {
        canAttack = false;
    }


    public void OnTouchExit()
    {
        canAttack = true;
    }



    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Item item = collision.GetComponentInParent<Item>();
            if (item.beGrab)
                return;
            _items.Add(item);
        }
        if (collision.GetComponent<ItemInCommon>() != null && cur_Item == null) 
        {
            cur_Item = collision.gameObject;
            cur_Item.gameObject.SetActive(false);
            UIManager.instance.UpdateUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            _items.Remove(collision.GetComponentInParent<Item>());
        }
    }

}

[Serializable]
public class GroundSensor
{
    public bool isGround;
    public bool isPlatform;


    [SerializeField]private Transform checkPos;
    [SerializeField]private float cheackOffset = 0.5f;
    [SerializeField] private LayerMask cheackLayer;

    public void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos.transform.position, Vector2.down, cheackOffset, cheackLayer);
        if (hit.collider!=null)
        {
            if (hit.collider.GetComponent<PlatformEffector2D> ()!= null)
            {
                isPlatform = true;
            }
            else
            {
                isPlatform = false;
            }
            isGround = true;
        }
        else
        {
            isGround = false;
            isPlatform = false;
        }
    }
}
