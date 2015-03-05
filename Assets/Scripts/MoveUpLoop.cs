using UnityEngine;
using System.Collections;

public class MoveUpLoop : MonoBehaviour {

	public float speed = 100f;
	public float width = 10f;
	public GameObject target;
	

	// function runs at the creation of this object. sets the player object
	// @return void
	void Start()
	{
		target = GameObject.FindWithTag ("Player");
	}

	// physics update of this object
	// constantly moves this object upwards
	// @ Return void
	void FixedUpdate () {

		if (!GameSettings.pause)
		{
			//Debug.Log("paused");

		Vector3 p = transform.position;
		p += new Vector3 (0, speed * Time.deltaTime, 0);
		p = new Vector3 (target.transform.position.x, p.y, gameObject.transform.position.z);
		
		transform.position = p;
		}

	}
}
