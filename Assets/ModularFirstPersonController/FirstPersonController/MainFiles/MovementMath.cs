using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMath : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cam;
    void Awake(){
        Cam.transform.position = new Vector3(-208.6f, 1.95f, 25.08f);
    }
}
