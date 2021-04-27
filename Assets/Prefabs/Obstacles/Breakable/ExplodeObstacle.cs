using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class ExplodeObstacle : MonoBehaviour
{
    private float radius = 10.0F;
    private float power = 500.0F;

    void Start()
    {

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExplodeMe();
        }

    }

    public void ExplodeMe()
    {
        foreach (Transform aux in this.transform)
        {
            aux.GetComponent<Rigidbody>().useGravity = true;
            aux.GetComponent<Rigidbody>().AddExplosionForce(power , this.transform.position + Vector3.down * 0.5f, radius, 3.0f);
        }
        this.transform.GetComponent<BoxCollider>().isTrigger = true;
        Destroy(this.gameObject, 3.0f);
    }
}