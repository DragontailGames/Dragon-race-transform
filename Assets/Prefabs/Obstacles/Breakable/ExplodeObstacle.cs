using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class ExplodeObstacle : MonoBehaviour
{
    private float radius = 10.0F;
    private float power = 30.0F;
    public void ExplodeMe()
    {
        this.transform.GetComponent<BoxCollider>().isTrigger = true;
        foreach (Transform aux in this.transform)
        {
            aux.GetComponent<Rigidbody>().useGravity = true;
            aux.GetComponent<Rigidbody>().AddExplosionForce(power , this.transform.position + Vector3.down * 0.5f, radius, 2.0f);
        }
        Destroy(this.gameObject, 3.0f);
    }
}