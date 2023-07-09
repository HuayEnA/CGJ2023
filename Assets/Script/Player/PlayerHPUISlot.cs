using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUISlot : MonoBehaviour
{
    public ActorControl ac;

    public Text Hp;

    public GameObject deadLine;

    public Image _image;

    public void UpdateTheUI()
    {
        
        Hp.text = ac._HpCount.ToString();
        if (ac.cur_Item == null)
        {
            _image.sprite = null;
            _image.gameObject.SetActive(false);
        }
        else
        {
            _image.gameObject.SetActive(true);
            _image.sprite = ac.cur_Item.GetComponent<ItemInCommon>().Image;
        }
        if (ac._HpCount <= 0)
        {
            deadLine.SetActive(true);
        }
    }




}
