using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody bulletrb;
    public float bulletspeed;
    public int damage = 10;
    public string targetTag; // Set this to "Player" for enemy projectiles and "Enemy" for player projectiles

    // Start is called before the first frame update
    void Start()
    {
        bulletrb = GetComponent<Rigidbody>();        
        bulletrb.AddForce(transform.forward * bulletspeed, ForceMode.VelocityChange);
        Destroy(gameObject, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log("whatever");
        Debug.Log("Bullet hit " + other.name);
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        else Debug.Log("No health script found on target object");*/
        Destroy(gameObject);



    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Destroy(gameObject);
    }

    
}
