using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSense : MonoBehaviour
{

    public static AttackSense instance;

    public bool isShake;

    public bool inSlowMotion;
    public bool slowMotionTarget;
    public float slowMotionTime;
    private float slowMotionTimeLerp;
    public float slowMotionTimeScale = 0.3f;


    private void Update()
    {
        //if (inSlowMotion)
        //{
            
        //    //Debug.Log("agg");
        //    slowMotionTimeLerp += Time.deltaTime;
        //    Time.timeScale = Mathf.Lerp(Time.timeScale, slowMotionTimeScale, slowMotionTimeLerp / slowMotionTime);
        //    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, GameUtility.cameraSize_InSlowTime, slowMotionTimeLerp / slowMotionTime);
        //}

    }

    public static AttackSense Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Transform.FindObjectOfType<AttackSense>();
            }
            return instance;
        }
    }

    public void CameraShake(float duration, float strength)
    {
        if (!isShake)
        {
            StartCoroutine(ShakeCamera(duration, strength));
        }
    }

    IEnumerator ShakeCamera(float duration,float strength)
    {
        isShake = true;
        Vector3 cameraPos_S = Camera.main.transform.position;
        Transform tempPos = Camera.main.transform;
        
        while(duration > 0)
        {
            tempPos.position = Random.insideUnitSphere * strength + cameraPos_S;
            duration -= Time.deltaTime;
            yield return null;
        }
        tempPos.position = cameraPos_S;
        isShake=false;
    }


    public void HitPause(float duration)
    {
        if (inSlowMotion)
        {
            return;
        }
        StartCoroutine(Pause(duration));
    }

    IEnumerator Pause(float duration)
    {
        float pauseTime = duration / 60.0f;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

    public void IntoSlowMotion(float duration,GameObject target)
    {
        slowMotionTimeLerp = 0;
        slowMotionTime = duration / 60.0f;
        inSlowMotion = true;
        StartCoroutine(IE_IntoSlowMotion());
    }
    IEnumerator IE_IntoSlowMotion()
    {
        yield return new WaitForSecondsRealtime(slowMotionTime);
        inSlowMotion = false;
        Time.timeScale = 1;
        //Camera.main.DOOrthoSize(GameUtility.cameraSize_Max, 0.5f);
    }
}
