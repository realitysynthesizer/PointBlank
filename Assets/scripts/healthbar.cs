using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform target; // The enemy this health bar follows
    public Vector3 offset; // Offset to position the health bar above the enemy
    private Slider healthSlider;
    private Health enemyHealth;

    void Start()
    {
        healthSlider = GetComponentInChildren<Slider>();
        if (target != null)
        {
            enemyHealth = target.GetComponent<Health>();
            if (enemyHealth == null)
            {
                Debug.LogError("Health component not found on target.");
            }
        }

        
    }

    void Update()
    {
        if (target != null && enemyHealth != null)
        {
            // Update health bar position
            transform.position = target.position + offset;

            
            

            // Update health bar value
            healthSlider.value = (float)enemyHealth.GetCurrentHealth() / enemyHealth.GetMaxHealth();
        }
        
        if (enemyHealth.GetCurrentHealth()<=0)
        {
            Destroy(gameObject);
        }
    }
}
