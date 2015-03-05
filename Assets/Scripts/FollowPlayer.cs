using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	// varibale holding the player's position
	public Transform player;
	
	// Update is called once per frame
	// makes one object follow the player on all but the z axis
	// @ Return void
	void FixedUpdate () {

		transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10);
	}
}
