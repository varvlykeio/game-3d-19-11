using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuizVars;
using MyVars;
using Unity.VisualScripting;
using System;
using QuizCol;
using System.Numerics;
using GameEv;



namespace Gamemanagera{

    public class GameManager : MonoBehaviour {

        #region Variables

        public              bool                levlchecksum            = false;
        public              bool                levlcheckturn           = true;   
        public              bool                levelcheck1             = false;
        public              bool                levelcheck2             = false;
        public              bool                levelcheck3             = false;
        public              GameObject          cc                      ;
        private             Data                data                    = new Data();

        [SerializeField]    GameEvents          events                  = null;

        [SerializeField]    Animator            timerAnimtor            = null;
        [SerializeField]    TextMeshProUGUI     timerText               = null;
        [SerializeField]    Color               timerHalfWayOutColor    = Color.yellow;
        [SerializeField]    Color               timerAlmostOutColor     = Color.red;
        private             Color               timerDefaultColor       = Color.white;
        public              GameObject          HMM                     ;

        private             List<AnswerData>    PickedAnswers           = new List<AnswerData>();
        private             List<int>           FinishedQuestions       = new List<int>();
        private             int                 currentQuestion         = 0;

        private             int                 timerStateParaHash      = 0;

        private             IEnumerator         IE_WaitTillNextRound    = null;
        private             IEnumerator         IE_StartTimer           = null;

        private             bool                IsFinished
        {
            get
            {
                return (FinishedQuestions.Count < data.Questions.Length) ? false : true;
            }
        }

        #endregion

        #region Default Unity methods

        /// <summary>
        /// Function that is called when the object becomes enabled and active
        /// </summary>
        private void OnEnable()
        {
            events.UpdateQuestionAnswer += UpdateAnswers;
        }
        /// <summary>
        /// Function that is called when the behaviour becomes disabled
        /// </summary>
        private void OnDisable()
        {
            events.UpdateQuestionAnswer -= UpdateAnswers;
        }

        /// <summary>
        /// Function that is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            //If current level is a first level, reset the final score back to zero.
            if (events.level == 1) { events.CurrentFinalScore = 0; }

            
        }

        QuizCols1 scriptInstance1 = null;
        QuizCols2 scriptInstance2 = null;
        QuizCols3 scriptInstance3 = null;
        /*MyVarsClass score = null;*/

        public void Update()
        {
            /*GameObject tempObj = GameObject.Find("Control Center");
            score = tempObj.GetComponent<MyVarsClass>();*/
            GameObject tempObj2 = GameObject.Find("ATM2(Clone)");
            scriptInstance2 = tempObj2.GetComponent<QuizCols2>();
            GameObject tempObj1 = GameObject.Find("ATM1(Clone)");
            scriptInstance1 = tempObj1.GetComponent<QuizCols1>();
            GameObject tempObj3 = GameObject.Find("ATM3(Clone)");
            scriptInstance3 = tempObj3.GetComponent<QuizCols3>();
            
            /*levelcheck1 = scriptInstance1.pusher1;
            levelcheck2 = scriptInstance2.pusher2;
            levelcheck3 = scriptInstance3.pusher3;
            if(levelcheck1 == true || levelcheck2==true || levelcheck3 == true){

                levlchecksum = levlcheckturn;
            }*/

            
            //Debug.Log(events.CurrentFinalScore);
            
            //Debug.Log("1" + levlchecksum1 + "2" + levlchecksum2 + "3" + levlchecksum3);
            //Debug.Log("Levelchecksum Value: " + levlchecksum + "\nLevelcheck Value: " + levelcheck + "\n Levlcheck2 Value: " + levlcheck2);


            if (events.QuizStart){


                
                events.StartupHighscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

                timerDefaultColor =  Color.white;
                LoadData();
                   HMM.SetActive(true);
                timerStateParaHash = Animator.StringToHash("TimerState");

                var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                UnityEngine.Random.InitState(seed);
                
                Display();
                 
                events.QuizStart = false;
                
            }
        }

        #endregion

        /// <summary>
        /// Function that is called to update new selected answer.
        /// </summary>
        public void UpdateAnswers(AnswerData newAnswer)
        {
            if (data.Questions[currentQuestion].Type == AnswerType.Single)
            {
                foreach (var answer in PickedAnswers)
                {
                    if (answer != newAnswer)
                    {
                        answer.Reset();
                    }
                }
                PickedAnswers.Clear();
                PickedAnswers.Add(newAnswer);
            }
            else
            {
                bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
                if (alreadyPicked)
                {
                    PickedAnswers.Remove(newAnswer);
                }
                else
                {
                    PickedAnswers.Add(newAnswer);
                }
            }
        }

        /// <summary>
        /// Function that is called to clear PickedAnswers list.
        /// </summary>
        public void EraseAnswers()
        {
            PickedAnswers = new List<AnswerData>();
        }

        /// <summary>
        /// Function that is called to display new question.
        /// </summary>
        void Display()
        {
           
            EraseAnswers();
            
            var question = GetRandomQuestion();
            
            if (events.UpdateQuestionUI != null)
            {
                events.UpdateQuestionUI(question);
            } else { Debug.LogWarning("Ups! Something went wrong while trying to display new Question UI Data. GameEvents.UpdateQuestionUI is null. Issue occured in GameManager.Display() method."); }
             
            if (question.UseTimer)
            {
                UpdateTimer(question.UseTimer);
            }
           
        }

        public void CursorLockOnQuizEnd(){
            events.CursorLock = true;
        }

        /// <summary>
        /// Function that is called to accept picked answers and check/display the result.
        /// </summary>
        /// //
        public void Accept()
        {
            UpdateTimer(false);
            bool isCorrect = CheckAnswers();
            FinishedQuestions.Add(currentQuestion);

            UpdateScore(isCorrect ? data.Questions[currentQuestion].AddScore : -data.Questions[currentQuestion].AddScore);

            if (IsFinished)
            {
                events.level++;
                if (events.level > GameEvents.maxLevel)
                {
                    events.level = 1;
                }
                SetHighscore();
            }

            var type 
                = (IsFinished) 
                ? UIManager.ResolutionScreenType.Finish 
                : (isCorrect) ? UIManager.ResolutionScreenType.Correct 
                : UIManager.ResolutionScreenType.Incorrect;

            events.DisplayResolutionScreen?.Invoke(type, data.Questions[currentQuestion].AddScore);

            AudioManager.Instance.PlaySound((isCorrect) ? "CorrectSFX" : "IncorrectSFX");

            if (type != UIManager.ResolutionScreenType.Finish)
            {
                if (IE_WaitTillNextRound != null)
                {
                    StopCoroutine(IE_WaitTillNextRound);
                }
                IE_WaitTillNextRound = WaitTillNextRound();
                StartCoroutine(IE_WaitTillNextRound);
            }
        }

        #region Timer Methods

        void UpdateTimer(bool state)
        {
            switch (state)
            {
                case true:
                    IE_StartTimer = StartTimer();
                    StartCoroutine(IE_StartTimer);

                    timerAnimtor.SetInteger(timerStateParaHash, 2);
                    break;
                case false:
                    if (IE_StartTimer != null)
                    {
                        StopCoroutine(IE_StartTimer);
                    }

                    timerAnimtor.SetInteger(timerStateParaHash, 1);
                    break;
            }
        }
        IEnumerator StartTimer()
        {
            var totalTime = data.Questions[currentQuestion].Timer;
            var timeLeft = totalTime;

            timerText.color = timerDefaultColor;
            while (timeLeft > 0)
            {
                timeLeft--;

                AudioManager.Instance.PlaySound("CountdownSFX");

                if (timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
                {
                    timerText.color = timerHalfWayOutColor;
                }
                if (timeLeft < totalTime / 4)
                {
                    timerText.color = timerAlmostOutColor;
                }

                timerText.text = timeLeft.ToString();
                yield return new WaitForSeconds(1.0f);
            }
            Accept();
        }
        IEnumerator WaitTillNextRound()
        {
            yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
            Display();
        }

        #endregion

        /// <summary>
        /// Function that is called to check currently picked answers and return the result.
        /// </summary>
        bool CheckAnswers()
        {
            if (!CompareAnswers())
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Function that is called to compare picked answers with question correct answers.
        /// </summary>
        bool CompareAnswers()
        {
            if (PickedAnswers.Count > 0)
            {
                List<int> c = data.Questions[currentQuestion].GetCorrectAnswers();
                List<int> p = PickedAnswers.Select(x => x.AnswerIndex).ToList();

                var f = c.Except(p).ToList();
                var s = p.Except(c).ToList();

                return !f.Any() && !s.Any();
            }
            return false;
        }

        /// <summary>
        /// Function that is called to load data from the xml file.
        /// </summary>
        void LoadData()
        {
            //var path = Path.Combine(GameUtility.FileDir, GameUtility.FileName + events.level + ".xml");
            var path = Path.Combine(GameUtility.FileDir,"Q3.xml");
          
            data = Data.Fetch(path); 
         
        }

        /// <summary>
        /// Function that is called restart the game.
        /// </summary>
        /// 
        #region RESTART
        //public GameObject player;
        public void RestartGame()
        {
            //If next level is the first level, meaning that we start playing a game again, reset the final score.

            //Debug.Log(player.transform);
            if (events.level == 1) 
            { 
                events.CurrentFinalScore = 0; 
            }
            //cc.GetComponent<MyVarsClass>().enabled = false;
            //cc.GetComponent<MyVarsClass>().enabled = true; 
            
            // Vector = (1f, 1f, 1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Debug.Log(player.transform);
        }
        #endregion RESTART
        /// <summary>
        /// Function that is called to quit the application.
        /// </summary>
        public void QuitGame()
        {
            //On quit reset the current level back to the first level.
            events.level = 1;

            Application.Quit();
        }

        /// <summary>
        /// Function that is called to set new highscore if game score is higher.
        /// </summary>
        private void SetHighscore()
        {
            var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
            if (highscore < events.CurrentFinalScore)
            {
                PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);
            }
        }
        /// <summary>
        /// Function that is called update the score and update the UI.
        /// </summary>
        private void UpdateScore(int add)
        {
            events.CurrentFinalScore += add;
            events.ScoreUpdated?.Invoke();
        }

        #region Getters

        Question GetRandomQuestion()
        {
            
            var randomIndex = GetRandomQuestionIndex();
           
            
            currentQuestion = randomIndex; 
            
            #region PROBELM
            return data.Questions[1];
            #endregion PROBELM
        }  
        int GetRandomQuestionIndex()
        {
            var random = 0;
            
            if (FinishedQuestions.Count < data.Questions.Length)
            {
               
                do{  
                    
                    random = UnityEngine.Random.Range(0, data.Questions.Length);
                } while (FinishedQuestions.Contains(random) || random == currentQuestion);
                
            }Debug.Log(data.Questions.Length);
            return random;
        }

        public void OnApplicationQuit(){
         FinishedQuestions.Clear();
         }

        #endregion
    }
 

}