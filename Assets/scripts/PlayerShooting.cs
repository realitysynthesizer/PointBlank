using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public int maxAmmo = 30; // Maximum ammo per clip
    public int currentAmmo; // Current ammo in the clip
    public int reserveAmmo = 90; // Total reserve ammo
    public float reloadTime = 3.3f; // Time it takes to reload
    public float fireRate = 0.5f; // Time between shots
    public TextMeshProUGUI ammoText; // UI element to display ammo count
    public gun gunscript;
    public Animator animator;
    public AudioSource reloadSound;
    public AudioSource emptyshotsound;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        animator = GetComponent<Animator>();
        animator.SetFloat("reloadspeedmultiplier", 3.3f/reloadTime);
        reloadSound = GetComponent<AudioSource>();
        
        
    }

    void Update()
    {
        if (isReloading)
            return;

        if (gunscript.inputasset.Player.Fire.inProgress && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            if (reserveAmmo > 0)
            {
                StartCoroutine(Reload());
            }
            else
            {
                // Play out of ammo sound or UI feedback if desired
                animator.SetInteger("fire", 0);
                Debug.Log("Out of ammo!");
            }
            return;
        }

        currentAmmo--;
        UpdateAmmoUI();
        gunscript.shoot();
        // Implement shooting mechanics here (e.g., instantiating bullets, applying damage)
        Debug.Log("Shot fired!");
    }

    IEnumerator Reload()
    {
        isReloading = true;
        // Display reload UI or animation if needed

        reloadSound.Play();
        animator.SetBool("Reloading", true);
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);


        int ammoNeeded = maxAmmo - currentAmmo;
        if (reserveAmmo >= ammoNeeded)
        {
            currentAmmo += ammoNeeded;
            reserveAmmo -= ammoNeeded;
        }
        else
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
        }

        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = $"{currentAmmo}/{reserveAmmo}";
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoUI();
    }
}
