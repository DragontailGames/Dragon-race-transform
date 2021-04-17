using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI rewardText;

    void Start()
    {
        Manager.Instance.canvasManager = this;
    }

    public void ChangeForm(int transformiceIndex)
    {
        Manager.Instance.gameManager.playerController.ChangeTransformice(transformiceIndex);
    }

    public void EndGame(int reward)
    {
        endGamePanel.SetActive(true);
        rewardText.text = reward.ToString();
    }
}
