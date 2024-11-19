using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MyVars;

namespace OWDoor {
    public class OWDoors : MonoBehaviour
    {

        public bool sas = false;


        public void OnTriggerEnter(){
            sas = true;
        }
        public void OnTriggerExit(){
            sas = false;
        }

    
    }
}