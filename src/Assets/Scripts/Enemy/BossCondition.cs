using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare{
    public class BossCondition : MonoBehaviour
    {
        private Quest4 quest4;

        void Awake(){
            quest4 = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<Quest4>();
        }  
        // Start is called before the first frame update
        void OnDestroy(){
            quest4.SetIsWon(true);
        }
    }
}