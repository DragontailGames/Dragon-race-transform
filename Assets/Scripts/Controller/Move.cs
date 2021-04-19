using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;

    public float speedModifier = 1;

    public float speed;

    public bool climb = false;

    public bool flying = false;

    protected float startY = 0;

    public virtual void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.rb.velocity = climb==false ? 
            new Vector3(0, flying?0:rb.velocity.y, speed * Time.deltaTime * speedModifier):
            new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void InFlying(bool fly)
    {
        flying = fly;
        if(fly)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }
}
