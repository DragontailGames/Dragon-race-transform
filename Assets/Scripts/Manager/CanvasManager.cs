using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    void Start()
    {
        Manager.Instance.canvasManager = this;
    }

    public void ChangeForm(int transformiceIndex)
    {
        Manager.Instance.gameManager.playerController.ChangeTransformice(transformiceIndex);
    }
}
