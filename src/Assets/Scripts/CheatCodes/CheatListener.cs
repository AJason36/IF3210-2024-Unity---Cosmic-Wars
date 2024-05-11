using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class CheatListener : MonoBehaviour, ICheatCode
    {
        // Start is called before the first frame update
        void Start()
        {
            CheatCodeManager.instance.RegisterListener(
                this,
                CheatCodes.INF_MONEY
            );
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDestroy(){
            CheatCodeManager.instance.UnregisterListener(
                this,
                CheatCodes.INF_MONEY
            );
            }
        void ICheatCode.ActivateCheatCode(CheatCodes codes)
        {
            GetComponent<PlayerMovement>();
            switch (codes)
            {
                case CheatCodes.INF_MONEY:
                    DataPersistenceManager.Instance.setInfMoney();
                    Debug.Log("Infinite money cheat code activated");
                    break;
            }
        }
    }
}