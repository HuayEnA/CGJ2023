using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneEffectManager : MonoBehaviour
{
    public static SceneEffectManager instace;

    public ParticleSystem jumpFX;
    public ParticleSystem landFX;
    public ParticleSystem deadFx;
    private void Awake()
    {
        instace = this;
    }
    public void Jump(Vector3 pos)
    {
        jumpFX.transform.position = pos - Vector3.up * 0.8f;
        jumpFX.Play();
    }
    public void Land(Vector3 pos)
    {
        landFX.transform.position = pos - Vector3.up * 0.8f;

        landFX.Play();
    }

}
