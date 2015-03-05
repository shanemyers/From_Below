using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {

	public GameObject introBack;
	
	// Update is called once per frame, checks for the destruction of the logo to
	// move onto the next scene
	//@ return void
	void Update () {

		// if the intorduction's background is destroyed, move onto the next scene
		if(introBack == null)
		{
			Application.LoadLevel("Title");
		}
	}
}
