using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public  List<GameObject> players = new List<GameObject>();
    public static CameraControl instance;

    private void Awake()
    {
        instance = this;
    }



    private void Update()
    {
        if (players.Count > 0)
        {
            Vector3 pos_Num = Vector3.zero;

            foreach (var player in players)
            {
                pos_Num += player.transform.position;
            }
            pos_Num /= players.Count;


            transform.position = pos_Num;
        }
        
    }


}
