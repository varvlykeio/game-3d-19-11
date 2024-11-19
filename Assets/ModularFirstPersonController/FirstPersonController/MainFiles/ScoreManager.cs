
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameEv;
using System;
using System.IO;


public class ScoreManager : MonoBehaviour
{

    #region Important
    [SerializeField]    GameEvents          events                  = null;
     public Text scoreText;
    
    public void Start(){
        scoreText.text = "Score: " + events.TotalScore.ToString() ;
        Debug.Log("KK" + events.TotalScore);
    }
    
    public void Awake(){
        scoreText.text = "Score: " + events.TotalScore.ToString() ;
        Debug.Log("KK" + events.TotalScore);
    }

    public void Update(){
        scoreText.text = "Score: " + events.TotalScore.ToString() ;


    }
    #endregion Important

 /*   
   

    public static int score;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = " Score_Snene " + Mathf.Round(score);
        
    }

public static ScoreManager instance;
   
    private void Awake()
    {
        instance = this;
    } 
    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag("Coin"))
        if(other.gameObject.name == "Player")
        //if(other.gameObject.CompareTag("player"))
        {
            Destroy(other.gameObject);
            other.gameObject.SetActive(false);
           ScoreManager.instance.Addpoints();
            //Addpoints();
           // Debug.Log(Coin);
            //return;
        }
        else
        {
            
            
            //ScoreManager.instance.Addpoints();
           
        }


        
    }
    

    // 
    public void Addpoints()
    {
        score += 10;
        scoreText.text = "Score_Snene" + score.ToString();
    }*/
}

