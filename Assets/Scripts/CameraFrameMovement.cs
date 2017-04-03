using UnityEngine;
using System.Collections;

public class CameraFrameMovement : MonoBehaviour {



	public GameObject frame;

	public static float movementRate = .02f;
	//Could have different movementRates as player progresses OR for different difficulties.
	Vector3 temp = new Vector3(0,movementRate,0);


	// Use this for initialization
	void Start () {
	}
		
	void Update () {
		frame.transform.position += temp;
	}

	void OnTriggerExit2D(Collider2D other) {
		

		if (other.tag == "Player") {
			Debug.Log ("Player exits frame.");
			Vector3 frameCenter = frame.transform.position;
			GameObject player = GameObject.FindGameObjectWithTag ("Player"); //will need to change when we have 2 players



			player.transform.position = frameCenter;


			Fox_Controller.instance.lifeLost ();
			//Polygon collider seems to work OK to have player stand on cloud.
		}
	}

	public void turnOffTrigger() {
		this.GetComponent<BoxCollider2D> ().isTrigger = false;
	}
		
}
