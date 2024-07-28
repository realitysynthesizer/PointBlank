using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDying = false;
    public GameObject deatheffect;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (gameObject == null)
        {
            return;
        }
    }
    public void TakeDamage(int damage)
    {
        if (isDying) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    void Die()
    {
        isDying = true;
        Debug.Log(gameObject.name + " died.");
        Instantiate(deatheffect, transform.position, Quaternion.identity);
        // Play the destroy animation
        /*if (animator != null)
        {
            animator.SetTrigger("Destroy");
        }*/
        // Add a delay to allow the animation to play before destroying the object
        Destroy(gameObject);
        if (gameObject.CompareTag("Player"))
        {

            
            GameData.LastScene = SceneManager.GetActiveScene().name;
            //StartCoroutine(GameOver());




        }


    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
    }
}
