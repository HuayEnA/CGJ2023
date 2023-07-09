using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    //public SpriteRenderer playerSR;
    public Transform selfTransform;
    public Transform playerTransform;
    
    public float activeTime = 2f;
    public float activeStart;
    public float alpha;
    public float alphaMulitply = 0.8f;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //playerSR = GameObject.Find("PlayerHandle").GetComponentInChildren<SpriteRenderer>();
        //playerTransform = GameObject.Find("PlayerHandle").transform.Find("Image").GetComponent<Transform>();
        
    }
    private void OnEnable()
    {

        if (playerTransform == null)
        {
            return;
        }
        alpha = 1;

        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, alpha);
        activeStart = Time.time;
        Vector3 pos = playerTransform.transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.z);
        Vector3 rotation = playerTransform.transform.eulerAngles;
        Vector3 localScale = playerTransform.localScale;
        transform.eulerAngles = new Vector3(rotation.x, rotation.y, rotation.z);
        transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        //spriteRenderer.sprite = playerSR.sprite;
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f, 0), 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= activeStart + activeTime)
        {
            ShadowPool.instance.ReturnToShadowPool(gameObject);
        }
    }
}
