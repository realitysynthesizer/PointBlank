using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int maxAmmo;
    public int currentAmmo;
    public int maxReserveAmmo;
    public int reserveAmmo;
    public float reloadTime;
    public float fireRate;
    //public GameObject bulletPrefab;
    //public AudioClip shootSound;
    public AudioClip reloadSound;
    public GameObject muzzleFlash;
    public GameObject hitImpact;
    public GameObject bloodImpact;
    public AudioClip shotclip;
    
    
}
