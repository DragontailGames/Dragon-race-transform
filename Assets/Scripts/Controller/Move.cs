using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;

    public float speedModifier = 1;

    public float speed;

    public virtual void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.rb.velocity = new Vector3(0, rb.velocity.y, speed * Time.deltaTime * speedModifier);
    }
}
