using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    private int currentWeaponIndex = 0;
    public Weapon CurrentWeapon { get; private set; }
    public List<GameObject> weaponObjects = new List<GameObject>(); // List of weapon GameObjects
    public List<GameObject> tipObjects = new List<GameObject>(); // List of tip GameObjects
    public List<RawImage> weaponImages = new List<RawImage>(); // List of weapon UI Images
    public RawImage activeWeaponImage; // UI Image to show active weapon

    public Animator animator; // Animator component for weapon switching animations
    public GameObject CurrentTip { get; private set; }



    void Start()
    {
        if (weapons.Count > 0)
        {
            
            CurrentWeapon = weapons[0];
            CurrentTip = tipObjects[0];
            UpdateWeaponObjects();
            UpdateWeaponUI();


        }

        foreach (Weapon weapon in weapons)
        {
            
            weapon.currentAmmo = weapon.maxAmmo;
            weapon.reserveAmmo = weapon.maxReserveAmmo;
        }
        
    }

    void UpdateWeaponObjects()
    {
        for (int i = 0; i < weaponObjects.Count; i++)
        {
            weaponObjects[i].SetActive(i == currentWeaponIndex);
        }
    }
    void UpdateWeaponUI()
    {
        for (int i = 0; i < weaponImages.Count; i++)
        {
            weaponImages[i].color = (i == currentWeaponIndex) ? Color.white : Color.gray; // Highlight the current weapon
        }
        activeWeaponImage.texture = weaponImages[currentWeaponIndex].texture; // Update active weapon RawImage
    }
    public void SelectWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = index;
            CurrentWeapon = weapons[currentWeaponIndex];
            CurrentTip = tipObjects[currentWeaponIndex];
            StartCoroutine(SwitchWeapon());
            // Update UI or other necessary components to reflect weapon change

        }
    }

    public void SelectNextWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        SelectWeapon(currentWeaponIndex);
    }

    public void SelectPreviousWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Count) % weapons.Count;
        SelectWeapon(currentWeaponIndex);
    }

    IEnumerator SwitchWeapon()
    {
        animator.SetTrigger("Holster");
        UpdateWeaponObjects();
        UpdateWeaponUI();
        yield return new WaitForSeconds(0.5f);

        



        Debug.Log("Switched to weapon: " + CurrentWeapon.weaponName);
    }
}
