using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DoorInstructions : MonoBehaviour {

    public GameObject InstructionsText;
    public int timer;

    public void Start(){
        InstructionsText.SetActive(false);
    }
    public void OnTriggerEnter(){
        InstructionsText.SetActive(true);
    }
    public void OnTriggerExit(){
        InstructionsText.SetActive(false);
    }


}