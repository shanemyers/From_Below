using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	// stores lifetime of an object
	public float lifeTime;

	// function fires at the creation of the object and sets it to die in a certain amount of time
	//@ Return void
	void Start () {

		// destroy this object after set amount of time
		Destroy (gameObject, lifeTime);
	}

}
