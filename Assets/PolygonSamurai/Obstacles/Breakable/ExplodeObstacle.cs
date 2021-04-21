using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class ExplodeObstacle : MonoBehaviour
{
    public float radius = 10.0F;
    public float power = 300.0F;

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
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                hit.enabled = false;
                
        }
}
}