using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//τηλεμεταφορα (στον ανεμιστηρα) στα ATM απο το κουιζ
//περιεργη τηλεμεταφορά του παίκτη: -279 , -8.11 , 17.24 ...

public class RestartTrigger : MonoBehaviour 
{
    public Transform Tel_Target, Tel_Target2;
    public GameObject the_player;

    //Vector3 player_location;

    void Start()
    {
        //player_location = the_player.transform.position; 
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            the_player.SetActive(false);
            Tel_Target2.transform.position = Tel_Target.transform.position;
            the_player.SetActive(false);
        }
    }

}