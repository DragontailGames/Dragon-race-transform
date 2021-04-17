using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public override void Start()
    {
        base.Start();
        Manager.Instance.gameManager.playerController = this;
    }

    public override void FinishRun()
    {
        base.FinishRun();
    }
}
