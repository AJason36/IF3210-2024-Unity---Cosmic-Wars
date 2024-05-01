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

    void Start() {
        current = null;
        animator.SetInteger("Weapon", 4);
    }

    void Update()
    {
        if (Input.GetButtonDown("Weapon 0"))
        {
            animator.SetInteger("Weapon", 4);
            if (current != null) {
                Destroy(current);
                current.transform.SetParent(null);
            }
        } else if (Input.GetButtonDown("Weapon 1"))
        {
            animator.SetInteger("Weapon", 1);
            if (current != null) {
                Destroy(current);
                current.transform.SetParent(null);
            }
            current = Instantiate(gun, attachmentPoint.position, attachmentPoint.rotation);
            current.transform.SetParent(attachmentPoint, true);
        } else if (Input.GetButtonDown("Weapon 2"))
        {
            animator.SetInteger("Weapon", 2);
            if (current != null) {
                Destroy(current);
                current.transform.SetParent(null);
            }
            current = Instantiate(basoka, attachmentPoint.position, attachmentPoint.rotation);
            current.transform.SetParent(attachmentPoint, true);
        } else if (Input.GetButtonDown("Weapon 3"))
        {
            animator.SetInteger("Weapon", 3);
            if (current != null) {
                Destroy(current);
                current.transform.SetParent(null);
            }
            current = Instantiate(lightSaber, attachmentPoint.position, attachmentPoint.rotation);
            current.transform.SetParent(attachmentPoint, true);
        }
        
    }
}
