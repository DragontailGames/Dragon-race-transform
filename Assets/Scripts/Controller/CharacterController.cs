using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Move
{
    public List<Transformice> transformices = new List<Transformice>()
    {
        new Transformice()
        {
            name = "Blue",
            speed = 250.0f,
            level = 0,
            levelMultiplier = 0.20f,
            transformiceType = EnumDT.TransformiceType.runner,
        },
        new Transformice()
        {
            name = "Brown",
            speed = 200.0f,
            level = 0,
            levelMultiplier = 0.20f,
            transformiceType = EnumDT.TransformiceType.breaker,
        },
        new Transformice()
        {
            name = "Purple",
            speed = 100.0f,
            level = 0,
            levelMultiplier = 0.20f,
            transformiceType = EnumDT.TransformiceType.specialFloor,
        },
        new Transformice()
        {
            name = "Red",
            speed = 200.0f,
            level = 0,
            levelMultiplier = 0.20f,
            transformiceType = EnumDT.TransformiceType.climb,
        },
        new Transformice()
        {
            name = "Green",
            speed = 200.0f,
            level = 0,
            levelMultiplier = 0.20f,
            transformiceType = EnumDT.TransformiceType.fly,
        },
    };

    public Transformice currentTransformice;

    public Transform interactObject;

    public override void Start()
    {
        base.Start();
        ChangeTransformice(0);
    }

    public void ChangeTransformice(int index)
    {
        currentTransformice = transformices[index];
        this.GetComponent<MeshRenderer>().material = currentTransformice.material;
        speed = currentTransformice.speed + ((currentTransformice.speed * currentTransformice.levelMultiplier) * currentTransformice.level);
    }

    public virtual void HasScenarioInteract(EnumDT.TransformiceType type){}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Barrier")
        {
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.breaker)
            {
                Destroy(collision.gameObject);
                HasScenarioInteract(EnumDT.TransformiceType.runner);
            }
            HasScenarioInteract(EnumDT.TransformiceType.breaker);
        }
        if (collision.transform.tag == "ClimbWall")
        {
            HasScenarioInteract(EnumDT.TransformiceType.climb);
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.climb)
            {
                climb = true;
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Barrier")
        {
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.breaker)
            {
                Destroy(collision.gameObject);
                HasScenarioInteract(EnumDT.TransformiceType.runner);
            }
        }
        if (collision.transform.tag == "ClimbWall" && climb == false)
        {
            HasScenarioInteract(EnumDT.TransformiceType.climb);
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.climb)
            {
                climb = true;
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "ClimbWall")
        {
            climb = false;
            HasScenarioInteract(EnumDT.TransformiceType.runner);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "FinishLine")
        {
            FinishRun();
        }
        if(other.transform.tag == "SpecialFloor")
        {
            if(currentTransformice.transformiceType != EnumDT.TransformiceType.specialFloor)
            speedModifier = 0.5f;
            HasScenarioInteract(EnumDT.TransformiceType.specialFloor);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "SpecialFloor")
        {
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.specialFloor)
            {
                speedModifier = 2f;
            }
            else
            {
                speedModifier = 0.5f;
            }
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "SpecialFloor")
        {
            speedModifier = 1f;
            HasScenarioInteract(EnumDT.TransformiceType.runner);
        }
        if (other.transform.tag == "FlyMarker")
        {
            if (!flying)
            {
                if (currentTransformice.transformiceType == EnumDT.TransformiceType.fly)
                {
                    InFlying(true);
                }
                HasScenarioInteract(EnumDT.TransformiceType.fly);
            }
            else
            {
                InFlying(false);
                HasScenarioInteract(EnumDT.TransformiceType.runner);
            }
        }
    }

    public virtual void FinishRun()
    {
        Manager.Instance.gameManager.CharacterFinishRun(this);
        this.enabled = false;
    }
}
