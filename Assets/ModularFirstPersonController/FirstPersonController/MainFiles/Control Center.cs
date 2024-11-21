using UnityEngine;
using QuizCol;
using Doors;
using System.Diagnostics;
using System.Numerics;
using Scenes;
using GameEv;




namespace MyVars{
    public class MyVarsClass : MonoBehaviour{
        
        public bool[] Maze = {false,false,false,true};
        public bool Maze1 = false;
        public bool Maze2 = false;
        public bool Maze3 = false;
        public bool Maze4 = true;
        public bool diadromos = true; // Checks if the player has entered a diadromos level to spawn the coins
        public bool changelevel = false; // Checks whether the level has changed to spawn Question machines. 
        public int level;
        public int levelch = -1;
        public int Question;

        [SerializeField]   public  GameEvents          events                  = null;



        public GameObject[] QuestionList = new GameObject[3];

                                // CoinR[level , x/y/z....]                    v-int-v
                                //     x1  ,   x2  ,    y   ,   z1  ,  z2  ,   ammount 
        public float[,] CoinR =  { {  -51  ,  -32  ,  0.5f  ,  -24   , -20  ,     5     },    //Level 0
                                   {   0   ,   0   ,   0    ,   0    ,  0   ,     2     },    //Level 1 -   Math      }
                                   {   0   ,   0   ,   0    ,   0    ,  0   ,     0     },    //Level 2 -  Coding     |
                                   {   0   ,   0   ,   0    ,   0    ,  0   ,     0     },    //Level 3 - History     |  Coordinates to 
                                   {   0   ,   0   ,   0    ,   0    ,  0   ,     0     },    //Level 4 - Language    |  be figured out
                                   {   0   ,   0   ,   0    ,   0    ,  0   ,     0     }, }; //Level 5 - Geometry    }
 

                                  //                                   QuestionR[ Question , level , x/y/z ]
                                  //              Level 1              ,            Level 2               ,      Level 3     ,      Level 4     ,      Level 5
                                  //        x    ,   y   ,    z              x      ,   y    ,    z             x , y , z          x , y , z          x , y , z
        public float[,,] QuestionR ={ {    {  -82f , 0f , -12f  }      , { -241.6f ,  0.1f  ,  18.1f  }  ,   { 0 , 0 , 0 }  ,   { 0 , 0 , 0 }  ,   { 0 , 0 , 0 } },   //Question 1
                                      {    { -211.6f , 0f , 53f }      , { -78.6f  ,  0.01f , -11.57f }  ,   { 0 , 0 , 0 }  ,   { 0 , 0 , 0 }  ,   { 0 , 0 , 0 } },   //Question 2
                                      { { -800.6f , 0.01f , -15.65f }  , { -78.6f  ,  0.01f , -15.65f }  ,   { 0 , 0 , 0 }  ,   { 0 , 0 , 0 }  ,   { 0 , 0 , 0 } } }; //Question 3
        public GameObject coin;

        QuizCols1 scriptInstance1 = null;
        QuizCols2 scriptInstance2 = null;
        QuizCols3 scriptInstance3 = null;
        SceneChanger scriptInstance4 = null;

        public void Start(){
                           

            
            if (changelevel == true){
                UnityEngine.Debug.Log("ping");
                //for(Question = 0; Question >= 2; Question++){   SpawnQuestions(level,Question); }  s  
                //Doesn't work with the for(), for some reason...

                /*UnityEngine.Vector3  QuestionPos = new UnityEngine.Vector3(QuestionR[0,level,0],QuestionR[0,level,1],QuestionR[0,level,2]);
                UnityEngine.Quaternion QuestionQ = new UnityEngine.Quaternion(0,0,0,0);
                Instantiate(QuestionList[Question], QuestionPos, QuestionQ);
                UnityEngine.Vector3  QuestionPos2 = new UnityEngine.Vector3(QuestionR[1,level,0],QuestionR[1,level,1],QuestionR[1,level,2]);
                UnityEngine.Quaternion QuestionQ2 = new UnityEngine.Quaternion(0,0,0,0);
                Instantiate(QuestionList[1], QuestionPos2, QuestionQ2);
                UnityEngine.Vector3  QuestionPos3 = new UnityEngine.Vector3(QuestionR[2,level,0],QuestionR[2,level,1],QuestionR[2,level,2]);
                UnityEngine.Quaternion QuestionQ3= new UnityEngine.Quaternion(0,0,0,0);
                Instantiate(QuestionList[2], QuestionPos3, QuestionQ3);*/
                    

                changelevel = false;
                UnityEngine.Debug.Log("ping");
            }
            
            level = events.currentlevel;
            if( level != levelch ){

                levelch = level;
                changelevel = true;
                UnityEngine.Debug.Log("Start " + levelch + " " + changelevel + " " + level);
            }


        }

        public void Update()
        {
            
            

     
            if( level != levelch ){

                levelch = level;
                changelevel = true;
                UnityEngine.Debug.Log("Hm " + levelch + " " + changelevel + " " + level);
            }


            #region Coins

            if (diadromos ==true){
                
                for(float i=0; i<= CoinR[level,5]-1; i++){
                    Invoke("SpawnCoin",0.3f);
                }
                diadromos= false;

            }
            #endregion Coins



            #region Questions

            #endregion Questions



            GameObject tempObj1 = GameObject.Find("ATM1(Clone)");
            scriptInstance1 = tempObj1.GetComponent<QuizCols1>();
            GameObject tempObj2 = GameObject.Find("ATM2(Clone)");
            scriptInstance2 = tempObj2.GetComponent<QuizCols2>();
            GameObject tempObj3 = GameObject.Find("ATM3(Clone)");
            scriptInstance3 = tempObj3.GetComponent<QuizCols3>();
            GameObject tempObj4 = GameObject.Find("Control Center");
            scriptInstance4 = tempObj4.GetComponent<SceneChanger>();
            
            //level = events.level;
            
    }

        public void SpawnCoin(){

            UnityEngine.Vector3 randCoinPos = new UnityEngine.Vector3(UnityEngine.Random.Range(CoinR[level,0], CoinR[level,1]), CoinR[level,2], UnityEngine.Random.Range(CoinR[level,3], CoinR[level,4]));
            UnityEngine.Quaternion CoinQ = new UnityEngine.Quaternion(0,0,0,0);
            Instantiate(coin, randCoinPos, CoinQ);

        }

        public void OnApplicationQuit(){       
           events.TotalScore = 0;
           events.currentlevel = 0;
           events.CursorLock = true;
           events.QuizStart = false;
        }

       
    }
}

