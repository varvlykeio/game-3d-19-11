using System.Collections;
using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using UnityEngine.UI;
using Unity.VisualScripting;
using MyVars;



public class Maze3 : MonoBehaviour
{
	public static bool levelcheck;
	public Image ImgMaze3;
	
	
	void Start() {

		
	}
	public bool FirstRun = true;

	MyVarsClass scriptInstance = null;
	void Update() {
		GameObject tempObj1 = GameObject.Find("Control Center");
		scriptInstance = tempObj1.GetComponent<MyVarsClass>();

        // Access the variable from MyClass       
		levelcheck = scriptInstance.Maze3;
		

		if( levelcheck == true ){
			if(FirstRun == true) {

				ImgMaze3.enabled = true;
				FirstRun = false;
				
			}
			else{

				//script for Maze 3
						

			}
		}
	}
}