using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class LevelGenerator : MonoBehaviour {

	public static LevelGenerator instance;
	//all level pieces blueprints used to copy from
	public List<LevelPiece> levelPrefabs = new List<LevelPiece>();
	//starting point of the very first level piece
	public Transform levelStartPoint;
	//all level pieces that are currently in the level
	public List<LevelPiece> pieces = new List<LevelPiece>();

	public Canvas newLevelCanvas;


	void Awake() {
		instance = this;
	}

	void Start() {
		GenerateInitialPieces();
	}

	public void GenerateInitialPieces() {
		for (int i=0; i<2; i++) {
			AddPiece();
		}
	}

	public void AddPiece() {

		//pick the random number
		int randomIndex = Random.Range(0, levelPrefabs.Count);
		//Instantiate copy of random level prefab and store it in piece variable
		LevelPiece piece = (LevelPiece)Instantiate(levelPrefabs[randomIndex]);
		piece.transform.SetParent(this.transform, false);

		Vector3 spawnPosition = Vector3.zero;

		/*
		//Generate Coins
		//get all collectables in piece and put into an array
		Collectable[] c = piece.GetComponentsInChildren<Collectable>();

		//Hides all coins
		foreach (var cx in c) {
			cx.Hide();
		}
		//shows some coins. Freq controls frequency of coins.
		foreach (var cx in c) {
			float freq = 0.65f;
			bool boolean  = (Random.value > freq);
			if(boolean)
				cx.Show();
		}*/

		if (pieces.Count == 0) {
			//first piece
			spawnPosition = levelStartPoint.position;
		}
		else {
			//take exit point from last piece as a spawn point to new piece
			spawnPosition = pieces[pieces.Count-1].exitPoint.position;
		}

		piece.transform.position = spawnPosition;
		/*
		int coins = GameManager.instance.collectedCoins;

		if (coins >= 10) {
			Debug.Log ("LEVEL OFFICIALLY OVER.");
			//Call UI here

			PlayerController.instance.animator.SetBool("isAlive", false);

			//If a killtrigger exists, turn it off.
			if(piece.GetComponentInChildren<KillTrigger>())
				piece.GetComponentInChildren<KillTrigger>().turnOffTrigger();

			GameManager.instance.newLevel();
			Time.timeScale = 0.0f;
		}
		*/
		pieces.Add(piece);

	}

	public void RemoveOldestPiece() {

		LevelPiece oldestPiece = pieces[0];

		pieces.Remove(oldestPiece);
		Destroy(oldestPiece.gameObject);
	}


}

