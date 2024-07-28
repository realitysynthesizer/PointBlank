using UnityEngine;

public class FirstAId : MonoBehaviour
{
    public GameObject player;
    public Health health;
    public AudioClip healthSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (health.GetCurrentHealth() > 350)
            {
                health.AddHealth(500-health.GetCurrentHealth());
                
            }
            else
            {
                health.AddHealth(150);
            }
            
            AudioSource.PlayClipAtPoint(healthSound, transform.position);
            Destroy(gameObject);
        }
    }
}
