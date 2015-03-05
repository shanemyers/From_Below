using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject prefabCrate;
	float timeUntilNextCrate = 5f; // 5 seconds


	// updates every frame and 
	// @ Return void
	void Update () {
		timeUntilNextCrate -= Time.deltaTime;

		if(timeUntilNextCrate <= 0)
		{
			Instantiate(prefabCrate);
			timeUntilNextCrate = 2f; // 2 seconds

			// generate multiple random numbers and add them together for good bell curve
		}
	}
}
