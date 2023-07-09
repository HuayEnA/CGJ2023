using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]private CatHpPanel _catHpPanel;

    private void Awake()
    {
        instance = this;
    }




    public void OpenPlayerHPUI(int id,ActorControl ac)
    {
        _catHpPanel.OpenPlayerHPUI(id,ac);
    }

    public void UpdateUI()
    {
        _catHpPanel.UpdateUI();
    }

}
