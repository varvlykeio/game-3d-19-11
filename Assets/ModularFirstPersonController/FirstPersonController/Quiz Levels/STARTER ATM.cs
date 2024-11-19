using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MyVars;
using System.Data.Common;
using JetBrains.Annotations;
using System;
using GameEv;


namespace StarterATM {

    public class StrtATM : MonoBehaviour{

        [SerializeField]    GameEvents          events                  = null;
        private bool activate;

        public GameObject StarterCanvas;

        public void Start(){

            StarterCanvas.SetActive(false);
        }
       
        public void OnTriggerExit(){
            
            activate = false;
            
        }
         public void OnTriggerEnter(){
            
            activate = true;

        }

        public void Update(){

            if (activate == true){
                if(Input.GetKeyDown("i")){

                    StarterCanvas.SetActive(true);
                    events.CursorLock = false;	
                    

                }
            }
            
        }

        
        
    }

}