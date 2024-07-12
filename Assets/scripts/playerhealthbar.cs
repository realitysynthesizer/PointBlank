using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Slider healthSlider;
    private Health playerHealth;
    private Image fillImage;    
    private Color fullHealthColor = Color.green;
    private Color lowHealthColor = Color.red;
    private bool isFlashing = false;

    void Start()
    {
        healthSlider = GetComponent<Slider>();
        fillImage = healthSlider.fillRect.GetComponent<Image>();

        playerHealth = FindAnyObjectByType<movements>().GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("Health component not found on Player.");
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            float healthPercent = (float)playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();
            healthSlider.value = healthPercent;

            if (healthPercent < 0.3f)
            {
                fillImage.color = lowHealthColor;
                if (!isFlashing)
                {
                    StartCoroutine(FlashLowHealth());
                }
            }
            else
            {
                fillImage.color = fullHealthColor;
                StopFlashing();
            }
        }
    }

    private System.Collections.IEnumerator FlashLowHealth()
    {
        isFlashing = true;
        while (true)
        {
            fillImage.enabled = !fillImage.enabled;
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void StopFlashing()
    {
        isFlashing = false;
        fillImage.enabled = true;
        StopAllCoroutines();
    }
}
