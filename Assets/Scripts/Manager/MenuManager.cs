using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dragontailgames.Utils;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI textPoint;

    void Start()
    {
        Manager.Instance.menuManager = this;
    }

    private void Update()
    {
        textPoint.text = "Points: " + PlayerManager.Instance.Money;
    }

    public void GotoGame()
    {
        SceneLoadManager.instance.gotoNext();
    }
}
