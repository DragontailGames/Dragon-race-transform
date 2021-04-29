using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Move
{
    public List<Transformice> transformices = new List<Transformice>()
    {
        new Transformice()
        {
            name = "Peasant",
            speed = 250.0f,
            level = 0,
            levelMultiplier = 0.20f,
            modelName = "Peasant",
            transformiceType = EnumDT.TransformiceType.runner,
        },
        new Transformice()
        {
            name = "Samurai",
            speed = 200.0f,
            level = 0,
            levelMultiplier = 0.20f,
            modelName = "Samurai",
            transformiceType = EnumDT.TransformiceType.breaker,
        },
        new Transformice()
        {
            name = "Geisha",
            speed = 100.0f,
            level = 0,
            levelMultiplier = 0.20f,
            modelName = "Geisha",
            transformiceType = EnumDT.TransformiceType.specialFloor,
        },
        new Transformice()
        {
            name = "Ninja",
            speed = 200.0f,
            level = 0,
            levelMultiplier = 0.20f,
            modelName = "Ninja",
            transformiceType = EnumDT.TransformiceType.climb,
        },
        new Transformice()
        {
            name = "Sensei",
            speed = 200.0f,
            level = 0,
            levelMultiplier = 0.20f,
            modelName = "Sensei",
            transformiceType = EnumDT.TransformiceType.fly,
        },
    };

    public Transformice currentTransformice;

    public Transform interactObject;

    private Animator currentAnimator;

    public override void Start()
    {
        base.Start();
        ChangeTransformice(0);
    }

    public void ChangeTransformice(int index)
    {
        Transform models = this.transform.Find("Models");
        if (!string.IsNullOrEmpty(currentTransformice.modelName))
        {
            models.transform.Find(currentTransformice.modelName).gameObject.SetActive(false);
        }

        currentTransformice = transformices[index];

        GameObject currentModel = models.transform.Find(currentTransformice.modelName).gameObject;
        currentModel.SetActive(true);
        currentAnimator = currentModel.GetComponent<Animator>();
        currentAnimator.SetBool("isRunning", true);

        speed = currentTransformice.speed + ((currentTransformice.speed * currentTransformice.levelMultiplier) * currentTransformice.level);
    }

    public virtual void HasScenarioInteract(EnumDT.TransformiceType type){}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Barrier")
        {
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.breaker)
            {
                currentAnimator.SetTrigger("Attack");
                StartCoroutine(ExplodeBarrier(collision.transform));
                HasScenarioInteract(EnumDT.TransformiceType.runner);
            }
            HasScenarioInteract(EnumDT.TransformiceType.breaker);
        }
        if (collision.transform.tag == "ClimbWall")
        {
            HasScenarioInteract(EnumDT.TransformiceType.climb);
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.climb)
            {
                Climb(true);
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Barrier")
        {
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.breaker)
            {
                currentAnimator.SetTrigger("Attack");
                StartCoroutine(ExplodeBarrier(collision.transform));
                HasScenarioInteract(EnumDT.TransformiceType.runner);
            }
        }
        if (collision.transform.tag == "ClimbWall" && climb == false)
        {
            HasScenarioInteract(EnumDT.TransformiceType.climb);
            if (currentTransformice.transformiceType == EnumDT.TransformiceType.climb)
            {
                Climb(true);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "ClimbWall")
        {
            Climb(false);
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
            Debug.Log("Teste 1");
            if (!flying)
            {
                Debug.Log("Teste 2");
                if (currentTransformice.transformiceType == EnumDT.TransformiceType.fly)
                {
                    Debug.Log("Teste 3");
                    InFlying(true);
                }
                HasScenarioInteract(EnumDT.TransformiceType.fly);
            }
            else
            {
                Debug.Log("Teste 4");
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

    public IEnumerator ExplodeBarrier(Transform barrier)
    {
        yield return new WaitForSeconds(0.2f);
        barrier.GetComponent<ExplodeObstacle>().ExplodeMe();
    }

    public void Climb(bool startClimb)
    {
        currentAnimator.SetBool("isRunning", !startClimb);
        currentAnimator.SetBool("isClimbing", startClimb);
        climb = startClimb;
    }
}
