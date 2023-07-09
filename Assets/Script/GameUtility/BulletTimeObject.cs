using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BulletTimeObject : MonoBehaviour
{


    private PhysicsSimulation _physicsSimulation;
    private Rigidbody2D rb;
    private Animator anim;
    private bool inBulletTime;
    public float TimeScale_Cur => inBulletTime ? Settings.TimeScale_InTimeSlow : Settings.TimeScale_Common;


    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        #region 开启物理模拟
        _physicsSimulation = new PhysicsSimulation(rb, Time.fixedDeltaTime, rb.gravityScale, this);
        _physicsSimulation.Enable();
        rb.gravityScale = 0;

        #endregion
    }





    private void FixedUpdate()
    {
        _physicsSimulation.FixedUpdate();
    }


    public bool GetBulletTimeState()
    {
        return inBulletTime;
    }

    public void IntoBulletTimeState()
    {
        inBulletTime = true;
        anim.speed = TimeScale_Cur ;

    }

    public void Into_BulletTime_F()
    {
        rb.velocity*=TimeScale_Cur ;
    }
    public void ExitBulletTimeState()
    {
        inBulletTime = false;
        anim.speed = TimeScale_Cur;
        rb.velocity /= Settings.TimeScale_InTimeSlow;

    }

    public void DisablePhysicsSimulation()
    {
        _physicsSimulation.Disable();
    }

    public void EnablePhysicsSimulation()
    {
        _physicsSimulation.Enable();
    }
}    
public class PhysicsSimulation
{
    private float gravity;

    private Rigidbody2D rb;

    private float deltaTime;

    private BulletTimeObject bt;

    private bool enable;
    
    public PhysicsSimulation(Rigidbody2D _rb, float _deltaTime, float _gravity, BulletTimeObject _bt)
    {
        gravity = _gravity;
        deltaTime = _deltaTime;
        rb = _rb;
        bt = _bt;
    }

    public void FixedUpdate()
    {
        if (!enable)
        {
            return;
        }

        rb.velocity -= new Vector2(0, gravity * 9.8f * deltaTime * bt.TimeScale_Cur);
        
    }

    public void Enable()
    {
        enable = true;
    }

    public void Disable()
    {
        enable = false;
    }


}