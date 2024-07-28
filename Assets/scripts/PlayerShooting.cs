using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using static Unity.VisualScripting.Member;

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
    public AudioClip reloadSound;
    public AudioSource emptyshotsound;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;
    public movements movements;
    public WeaponInventory weaponInventory;
    public Weapon currentWeapon;
    public GameObject currenttip;
    public GameObject shootpoint;



    void Start()
    {
        
        animator = GetComponent<Animator>();
        //reloadSound = GetComponent<AudioSource>();
        movements = GetComponent<movements>();
        weaponInventory = GetComponent<WeaponInventory>();
        
        

        if (weaponInventory != null && weaponInventory.CurrentWeapon != null)
        {

            
            currentWeapon = weaponInventory.CurrentWeapon;
            currenttip = weaponInventory.CurrentTip;
        }
        reloadSound = currentWeapon.reloadSound;
        animator.SetFloat("reloadspeedmultiplier", 3.3f / currentWeapon.reloadTime);
        

        UpdateAmmoUI();



    }

    void Update()
    {

        if (gameObject == null)
        {
            return;
        }

        if (isReloading)
            return;

        currentWeapon = weaponInventory.CurrentWeapon;
        currenttip = weaponInventory.CurrentTip;

        if (movements.inputasset.Player.Fire.inProgress && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + currentWeapon.fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        UpdateAmmoUI();

    }

    void Shoot()
    {
        if (currentWeapon.currentAmmo <= 0)
        {
            if (currentWeapon.reserveAmmo > 0)
            {
                StartCoroutine(Reload());
            }
            else
            {
                animator.SetInteger("fire", 0);
                Debug.Log("Out of ammo!");
            }
            return;
        }

        currentWeapon.currentAmmo--;
        UpdateAmmoUI();

        // Implement shooting mechanics here (e.g., instantiating bullets, applying damage)
        AudioSource.PlayClipAtPoint(currentWeapon.shotclip, transform.position);
        Instantiate(currentWeapon.muzzleFlash, currenttip.transform);
        if (Physics.Raycast(shootpoint.transform.position, shootpoint.transform.forward, out RaycastHit hit, 100f))
        {

            if (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Player")
            {
                Instantiate(currentWeapon.bloodImpact, hit.point, Quaternion.LookRotation(hit.normal));

            }
            else
            {
                GameObject impactinstance = Instantiate(currentWeapon.hitImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactinstance, 10f);
            }

            Health health = hit.collider.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);

            }

        }
        Debug.Log("Shot fired!");
    }

    IEnumerator Reload()
    {
        isReloading = true;
        // Display reload UI or animation if needed

        AudioSource.PlayClipAtPoint(currentWeapon.reloadSound, transform.position);
        animator.SetBool("Reloading", true);
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(currentWeapon.reloadTime);

        animator.SetBool("Reloading", false);


        int ammoNeeded = currentWeapon.maxAmmo - currentWeapon.currentAmmo;
        if (currentWeapon.reserveAmmo >= ammoNeeded)
        {
            currentWeapon.currentAmmo += ammoNeeded;
            currentWeapon.reserveAmmo -= ammoNeeded;
        }
        else
        {
            currentWeapon.currentAmmo += currentWeapon.reserveAmmo;
            currentWeapon.reserveAmmo = 0;
        }

        isReloading = false;
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = $"{currentWeapon.currentAmmo}/{currentWeapon.reserveAmmo}";
    }

    public void AddAmmo(int amount)
    {
        currentWeapon.reserveAmmo += amount;
        UpdateAmmoUI();
    }
}
