using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{

    public static CameraShake instance;

    public bool isShake;
    public CinemachineBasicMultiChannelPerlin channel;
    public CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {

        
        instance = this;
    }
    private void Start()
    {
        channel = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (channel == null)
        {
            Debug.Log("agag");
        }
    }
    public void CameraShake_CM(float duration, float strength)
    {
        if (!isShake)
        {
            StartCoroutine(ShakeCamera(duration, strength));
        }
    }

    IEnumerator ShakeCamera(float duration, float strength)
    {
        isShake = true;
        channel.m_AmplitudeGain = strength;
        Vector3 cameraPos_S = Camera.main.transform.position;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        channel.m_AmplitudeGain = 0;
        isShake = false;
    }
}
