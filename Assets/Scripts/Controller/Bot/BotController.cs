using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : CharacterController
{
    float speedToChange = 1.0f;

    public override void HasScenarioInteract(EnumDT.TransformiceType newObj)
    {
        base.HasScenarioInteract(newObj);
        CheckCollision(newObj);
    }

    public void CheckCollision(EnumDT.TransformiceType type)
    {
        if (type == EnumDT.TransformiceType.runner)
        {
             StartCoroutine(CanChangeTranformice(0));
        }
        if (type == EnumDT.TransformiceType.breaker)
        {
             StartCoroutine(CanChangeTranformice(1));
        }
        if(type == EnumDT.TransformiceType.specialFloor)
        {
             StartCoroutine(CanChangeTranformice(2));
        }
        if (type == EnumDT.TransformiceType.climb)
        {
             StartCoroutine(CanChangeTranformice(3));
        }
        if (type == EnumDT.TransformiceType.fly)
        {
            StartCoroutine(CanChangeTranformice(4, true));
            InFlying(true);
            startY = this.transform.position.y;
        }
    }

    public IEnumerator CanChangeTranformice(int index, bool immediately = false)
    {
        if(!immediately)
            yield return new WaitForSeconds(speedToChange + Random.Range(-0.5f, 0.5f));

        ChangeTransformice(index);
    }
}
