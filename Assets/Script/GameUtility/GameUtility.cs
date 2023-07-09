using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtility : MonoBehaviour
{

    public static GameUtility instance;

    public List<Transform> bornPoints = new List<Transform>();


    private void Awake()
    {
        instance = this;
    }


    public float ItemRecoverTime = 5f;

}
