using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [Header("Store")]
    public Store<Material> store;

    void Start()
    {
        store.CreateCanvas(PlayerManager.Instance.Money);
        store.spendMoney = (money) => { PlayerManager.Instance.Money -= money; };
    }

    private void Update()
    {
        store.currentMoney = PlayerManager.Instance.Money;
    }
}
