using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyVars;
using UnityEngine.UI;
using GameEv;


namespace CoinsNS{
    public class Coin : MonoBehaviour{  

        MyVarsClass coins = null;
        [SerializeField]    GameEvents          events                  = null;

        public void OnTriggerEnter(){
            GameObject tempObj = GameObject.Find("Control Center");
            coins = tempObj.GetComponent<MyVarsClass>();
            events.TotalScore += 1;
            Destroy(gameObject);
        
            

        }
    }
}   
