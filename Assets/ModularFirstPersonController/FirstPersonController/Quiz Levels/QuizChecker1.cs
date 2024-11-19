using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MyVars;
using System.Data.Common;
using JetBrains.Annotations;
using System;
using GameEv;


namespace QuizCol {

    public class QuizCols1 : MonoBehaviour{

       private bool activate;
        public bool pusher1;
           [SerializeField]    GameEvents          events                  = null;
       
         public void OnTriggerEnter(){
            
            activate = true;

        }

        public void Update(){

            if (activate == true){
                if(Input.GetKeyDown("i")){

                    pusher1 = true;
                    events.CursorLock = false;
                    events.QuizStart = true;

                }
            }
        }

        public void OnTriggerExit(){
            
            activate = false;
            
        }
        
    }

}
