using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject gun;  
    public GameObject lightSaber;  
    public GameObject basoka;  
    public GameObject current;  
    public Animator animator;
    public Transform attachmentPoint;

    private int weaponIndex = 4; 

    void Start() {
        current = null;
        SetWeapon(weaponIndex);
    }

    void Update()
    {
        float scroll = Input.GetAxis("Weapon Wheel");
        if (scroll != 0)
        {
            ChangeWeaponByScroll(scroll);
        } else {
            if (Input.GetButtonDown("Weapon 0"))
            {
                SetWeapon(4);
            }
            else if (Input.GetButtonDown("Weapon 1"))
            {
                SetWeapon(1);
            }
            else if (Input.GetButtonDown("Weapon 2"))
            {
                SetWeapon(2);
            }
            else if (Input.GetButtonDown("Weapon 3"))
            {
                SetWeapon(3);
            }
        }
    }

    void ChangeWeaponByScroll(float scrollDirection)
    {
        if (scrollDirection > 0)
        {
            weaponIndex++;
            if (weaponIndex > 4) weaponIndex = 1;
        }
        else
        {
            weaponIndex--;
            if (weaponIndex < 1) weaponIndex = 4; 
        }

        SetWeapon(weaponIndex);
    }

    void SetWeapon(int index)
    {
        if (current != null) {
            Destroy(current);
            current.transform.SetParent(null);
        }

        animator.SetInteger("Weapon", index);

        switch (index)
        {
            case 1:
                current = Instantiate(gun, attachmentPoint.position, attachmentPoint.rotation);
                break;
            case 2:
                current = Instantiate(basoka, attachmentPoint.position, attachmentPoint.rotation);
                break;
            case 3:
                current = Instantiate(lightSaber, attachmentPoint.position, attachmentPoint.rotation);
                break;
            case 4: 
                current = null;
                break;
        }

        if (current != null)
            current.transform.SetParent(attachmentPoint, true);
    }
}
