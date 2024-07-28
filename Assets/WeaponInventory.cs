using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponInventory : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    private int currentWeaponIndex = 0;
    public Weapon CurrentWeapon { get; private set; }
    public List<GameObject> weaponObjects = new List<GameObject>(); // List of weapon GameObjects
    public List<GameObject> tipObjects = new List<GameObject>(); // List of tip GameObjects
    public Animator animator; // Animator component for weapon switching animations
    public GameObject CurrentTip { get; private set; }



    void Start()
    {
        if (weapons.Count > 0)
        {
            
            CurrentWeapon = weapons[0];
            CurrentTip = tipObjects[0];
            UpdateWeaponObjects();

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

        yield return new WaitForSeconds(0.5f);

        UpdateWeaponObjects();

        

        Debug.Log("Switched to weapon: " + CurrentWeapon.weaponName);
    }
}
