﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurnManagerScript : MonoBehaviour {

	public int turnNum = 0;
	public int playerNum = 2;
	public int iaNum = 0;
	public bool inTurn = true;
	public List<List<GameObject> > unitList;

	public List<int> terrain;
	public GameObject[,] unitMap;
	public Vector2 mapSize;

	private int actualPlayer;
	private int actualNumChars;
	private int actualNumFinishedChars;

	public GameObject cursor;
	public Image image_move;
	public Image image_attack;

	private float accTime;
	private float timeStep;
	private float timeExp;
	private float timeExpStep;
	private float timeExpLimit;


	private bool unitSelected;
	private bool showActions;
	private Image i1;
	private Image i2;

	// Use this for initialization
	void Start () {
		
		//Estamos en turno
		inTurn = true;
		actualPlayer = 0;
		actualNumChars = 1;//charList [actualPlayer].Count;
		// Characters that have finished its actions
		actualNumFinishedChars = 0;

		timeStep = 0.3f;
		timeExp = 0.0f;
		timeExpStep = 0.02f;
		timeExpLimit = 0.15f;

		cursor = Instantiate (cursor, new Vector3 (1.0f, 1.5f, 1.0f), Quaternion.identity) as GameObject;
		cursor.transform.Rotate (new Vector3(90, 0, 0));

		unitSelected = false;
		showActions = false;
	}
	
	// Update is called once per frame
	void Update () {

		accTime += Time.deltaTime;

		if (!showActions) {
			if (Input.GetKey (KeyCode.A) && accTime > (timeStep - timeExp)) {
				accTime -= (timeStep - timeExp);
				if (timeExp < timeExpLimit)
					timeExp += timeExpStep;
				cursor.transform.Translate (-1f, 0, 0);
			} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				timeExp = 0.0f;
			}

			if (Input.GetKey (KeyCode.D) && accTime > (timeStep - timeExp)) {
				accTime -= (timeStep - timeExp);
				if (timeExp < timeExpLimit)
					timeExp += timeExpStep;
				cursor.transform.Translate (1f, 0, 0);
			} else if (Input.GetKeyUp (KeyCode.D)) {
				timeExp = 0.0f;
			}

			if (Input.GetKey (KeyCode.W) && accTime > (timeStep - timeExp)) {
				accTime -= (timeStep - timeExp);
				if (timeExp < timeExpLimit)
					timeExp += timeExpStep;
				cursor.transform.Translate (0, 1f, 0);
			} else if (Input.GetKeyUp (KeyCode.W)) {
				timeExp = 0.0f;
			}

			if (Input.GetKey (KeyCode.S) && accTime > (timeStep - timeExp)) {
				accTime -= (timeStep - timeExp);
				if (timeExp < timeExpLimit)
					timeExp += timeExpStep;
				cursor.transform.Translate (0, -1f, 0);
			} else if (Input.GetKeyUp (KeyCode.S)) {
				timeExp = 0.0f;
			}
		} else if (showActions) {
			if (Input.GetKey (KeyCode.Escape)) {
				showActions = false;
				unitSelected = false;
				DestroyObject (i1);
				DestroyObject (i2);
			}

		}


		if (Input.GetKeyUp(KeyCode.Space) && !unitSelected) {
			Vector3 tilePosition = cursor.transform.position;
			int ipos = (int)tilePosition.z;
			int jpos = (int)tilePosition.x;
			GameObject c = unitMap [ipos, jpos];
			//UnitBehaviour ub = c.GetComponent<UnitBehaviour> ();
			if (c != null) {
				unitSelected = true;
				showActions = true;
				GameObject canv =  GameObject.Find ("Canvas");
				i1 = Instantiate (image_move);
				i1.transform.SetParent (canv.transform, false);
				i2 = Instantiate (image_attack);
				i2.transform.SetParent (canv.transform, false);
			}

			//Obtener GameObject de la matriz luego extraer sus funciones
			//Mostrar opciones y elegir una
			//Ejecutar acción
			//Profit
		}

		if (actualNumFinishedChars == actualNumChars) {
			changeTeam ((actualPlayer + 1) % playerNum);
		}
	}

	// Attack position (i,j) -> row i, column j
	void execute(Action<GameObject> act, int i, int j) {
		GameObject c = unitMap [i, j];
		act (c);
	}

	void attack(int i, int j, params int[] altParams ) {
	
	}

	void attackRange (int i, int j, int k, int l, params int[] altParams ) {
	
	}

	void changeTeam (int newTeam) {
		actualPlayer = newTeam;
		//actualNumChars = charList [actualPlayer].Count;
		actualNumFinishedChars = 0;
	}

	/*void OnGUI(){
		if (showActions) {
			GUI.Box (new Rect(0, 0, 100, 100), "Move");
			GUI.Box (new Rect(100, 0, 100, 100), "Attack");
		}
	}*/
}

