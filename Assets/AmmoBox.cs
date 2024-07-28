using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject player;
    public PlayerShooting playerShooting;
    public AudioClip ammoSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerShooting = player.GetComponent<PlayerShooting>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerShooting.AddAmmo(30);
            AudioSource.PlayClipAtPoint(ammoSound, transform.position);
            Destroy(gameObject);

        }
    }
}
