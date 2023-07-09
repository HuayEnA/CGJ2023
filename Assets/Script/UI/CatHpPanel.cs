using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHpPanel : MonoBehaviour
{

    public List<PlayerHPUISlot> playerSlot = new List<PlayerHPUISlot>();





    public void OpenPlayerHPUI(int playerid,ActorControl ac)
    {
        playerSlot[playerid - 1].gameObject.SetActive(true);
        playerSlot[playerid - 1].ac = ac;
        playerSlot[playerid - 1].UpdateTheUI();
    }

    public void UpdateUI()
    {
        foreach(var slot in playerSlot)
        {
            if(slot.gameObject.activeSelf)
                slot.UpdateTheUI();
        }
    }


}
