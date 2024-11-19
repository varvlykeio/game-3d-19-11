using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MyVars;
using OWDoor;
using UnityEditor;

namespace Doors {
public class Door : MonoBehaviour
{

    public Vector3 rotation;
    public bool IsOpen = false;
    
    [SerializeField]
    private float Speed = 3f;

    [Header("Sliding Configs")]
    [SerializeField]
    
    
    private Vector3 SlideDirection = Vector3.back;
    [SerializeField]
    private float SlideAmount = 2.6f;
    private Vector3 StartPosition;

    private Coroutine AnimationCoroutine;
    public bool activate;
    public bool FirstDoor;

    private void Awake()
    {
        StartPosition = transform.position;
    
    }
    
     public void OnTriggerEnter(){

        activate = true;
        
    }

    public void OnTriggerExit(){

        activate = false;

    }
    OWDoors instance = null;
    public bool firstdoorch;
    public void Update(){

        

            rotation = transform.eulerAngles;
            /*if(IsOpen && firstdoorch){
                AnimationCoroutine = StartCoroutine(DoSlidingClose());
            }*/
            
            if (activate == true)
            {
                if (!IsOpen)
                {
                    if (Input.GetKeyDown("e") && !FirstDoor)
                    {
                    
                        AnimationCoroutine = StartCoroutine(DoSlidingOpen()); 
                    }
                }
                else
                {
                    if (Input.GetKeyDown("e"))
                    {            
                        AnimationCoroutine = StartCoroutine(DoSlidingClose());
                    }
                }
            }
            GameObject tempObj1 = GameObject.FindGameObjectWithTag("SDL");
            instance = tempObj1.GetComponent<OWDoors>();
            firstdoorch = instance.sas;
        }

    private IEnumerator DoSlidingOpen(){
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    

    private IEnumerator DoSlidingClose(){
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;
        float time = 0;

        IsOpen = false;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}
}