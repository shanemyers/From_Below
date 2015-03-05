using UnityEngine;
using System.Collections;

public class textController : MonoBehaviour {

	// variable holding text information
	public GUIText pauseText;
	public GUIText restartText;
	public GUIText victoryText;

	// makes the word "pause" appear before the player on a pause
	//@Return void
	public void SetPause()
	{
		pauseText.text = "Paused";
	}
	
	// removes text from the pause GUI
	//@Return void
	public void RemovePause()
	{
		
		pauseText.text = "";
		
	}

	// removes text from the game over GUI
	//@Return void
	public void RemoveRestart()
	{
		
		restartText.text = "";
		
	}

	// removes text from the victory GUI
	//@Return void
	public void RemoveVictory()
	{
		
		victoryText.text = "";
		
	}

	// sets the victory text
	//@Return void
	public void SetVictory()
	{
		victoryText.text = "Level Completed";
		
	}

	// set the game over text
	//@Return void
	public void SetRestart()
	{
		restartText.text = "Press A or Space to restart";
		
	}
}
