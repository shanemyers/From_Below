using UnityEngine;
using System.Collections;

public class FollowPlayerSlow : MonoBehaviour {

	// variables holdinh the player's position and the slowing amount
	public float moveSlow;
	public GameObject target;

	// finds the player object
	//@Return void
	void Start()
	{
		target = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	// updates the position of the object in relation to the player
	//@Return void
	void Update () {


		Vector2 p = new Vector2(gameObject.transform.position.x, target.transform.position.y - moveSlow);
		rigidbody2D.MovePosition (p);

	}
}
