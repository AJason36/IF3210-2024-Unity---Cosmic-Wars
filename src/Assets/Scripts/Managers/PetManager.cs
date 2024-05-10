using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    // Pets Object
    public GameObject rdPetObject;
    public GameObject bbPetObject;

    // Start is called before the first frame update
    void Start()
    {
        // Uninstantiate the pets
        rdPetObject.SetActive(false);
        bbPetObject.SetActive(false);

        // Instantiate the active pet
        int petId = DataPersistenceManager.Instance.GetGameData().petId;

        if (petId == 0)
        {
            rdPetObject.SetActive(true);
        }
        else if (petId == 1)
        {
            bbPetObject.SetActive(true);
        }
    }
}
