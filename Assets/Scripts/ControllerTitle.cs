using UnityEngine;
using System.Collections;

public class ControllerTitle : MonoBehaviour {


	// varaibels holding transform positions
	Transform camTarget;
	
	public Transform menuMain;
	public Transform menuMainLava;

	public Transform menuOptions;
	public Transform menuCredits;
	public Transform menuControl;


	public TextMesh textVolume;
	int volume = 100;

	// time variables
	public float transitionTime = 1f;
	float transitionTimeElapsed = 0f;
	Vector3 transitionStartPosition = Vector3.zero;

	// menu counting variables
	int choice = 0;
	int menuType = 0;
	bool isStillDown = false;

	public AudioClip menuMove;
	public AudioClip menuSelect;
	
	// sets the initial voulume of the game
	// @ Return void
	void Awake()
	{
		volume = PlayerPrefs.GetInt ("Volume");
		if(PlayerPrefs.HasKey("Volume")) volume = PlayerPrefs.GetInt ("Volume");
		

		AudioListener.volume =  PlayerPrefs.GetInt ("Volume") / 100f;
	}
	
	// begins the title screen on the main menu
	// @ Return void
	void Start () {
		GotoMainMenu();

		if (PlayerPrefs.HasKey ("Volume"))
		{
			Debug.Log("in");
			volume = PlayerPrefs.GetInt ("Volume");
		}
		UpdateMenu();
	}


	// saves the player's chosen volume for following play throughs
	// @Return void
	void SavePreferences()
	{
		PlayerPrefs.SetInt ("Volume", volume);
		PlayerPrefs.Save();
		
	}
	
	// Update is called once per frame
	// @ Return void
	void FixedUpdate () {

		// checks for player input to move up and down the main menu
		// and player selections
		float moveMenu = Input.GetAxisRaw("Vertical");
		bool selectMenu = Input.GetButton("Jump");

		// if the input is not begin held down use the input
		if(!isStillDown)
		{
			if(moveMenu > 0)
			{
				isStillDown = true;

				audio.PlayOneShot(menuMove, 0.7f);

				if(menuType == 0)
				{
					if(choice == 0)
					{
						choice = 4;

						Vector3 lMove = new Vector3(0,menuMainLava.transform.position.y - 4.5f,12);
						menuMainLava.transform.position = lMove;
					}
					else
					{
						choice--;

						Vector3 lMove = new Vector3(0,menuMainLava.transform.position.y + 1.125f,12);
						menuMainLava.transform.position = lMove;
					}
				}
				else if(menuType == 1)
				{
					volume += 10;
					if(volume > 100) volume = 100;
					UpdateMenu();
					UpdateVolume();
				}
			}
			else if(moveMenu < 0)
			{
				isStillDown = true;

				audio.PlayOneShot(menuMove, 0.7f);

				if(menuType == 0)
				{
					if(choice == 4)
					{
						choice = 0;

						Vector3 lMove = new Vector3(0,menuMainLava.transform.position.y + 4.5f,12);
						menuMainLava.transform.position = lMove;
					}
					else
					{
						choice++;

						Vector3 lMove = new Vector3(0,menuMainLava.transform.position.y - 1.125f,12);
						menuMainLava.transform.position = lMove;
					}
				}
				else if(menuType == 1)
				{
					volume -= 10;
					if(volume < 0) volume = 0;
					UpdateMenu();
					UpdateVolume();
				}

			}

			// selecting an option from the menu moves the stage and changes the menu logic
			if(selectMenu)
			{

				isStillDown = true;

				switch(menuType)
				{
					case 0:
						audio.PlayOneShot(menuSelect, 0.7f);
						MenuMainChoice();
						break;

					case 1:
						audio.PlayOneShot(menuSelect, 0.7f);
						GotoMainMenu();
						//MenuOptionsChoice();
						break;

					case 2:
						audio.PlayOneShot(menuSelect, 0.7f);
						GotoMainMenu();
						break;

					case 3:
						audio.PlayOneShot(menuSelect, 0.7f);
						GotoMainMenu();
						break;
				}
			}
		}
		else
		{
			// allows the player to move and select the menu again
			if(moveMenu == 0 && !selectMenu)
			{
				isStillDown = false;
			}
		}




		// move camera linearly towards second menu
		if(transform.position != camTarget.position)
		{
			
			transitionTimeElapsed += Time.deltaTime;
			float percent = transitionTimeElapsed / transitionTime;
			if(percent > 1) percent = 1;
			float x = Mathf.SmoothStep(transitionStartPosition.x, camTarget.position.x, percent);
			float y = Mathf.SmoothStep(transitionStartPosition.y, camTarget.position.y, percent);
			float z = Mathf.SmoothStep(transitionStartPosition.z, camTarget.position.z, percent);
			transform.position = new Vector3(x,y,z);
		}
		
	}

	// logic involved with the options section of the menu
	// plays a confirmation sound
	// @ Return void
	public void MenuOptionsChoice()
	{
	}

	// logic involved with the main section of the menu
	// @ Return void
	public void MenuMainChoice()
	{

		switch(choice)
		{
			// game
			case 0:
				audio.PlayOneShot(menuSelect, 0.7f);
				Application.LoadLevel("Level1");
				break;

			// options
			case 1:
				audio.PlayOneShot(menuSelect, 0.7f);
				SavePreferences();
				GotoOptionsMenu();
				break;

			// credits
			case 2:
				audio.PlayOneShot(menuSelect, 0.7f);
				GotoCreditsMenu();
				break;

			// controls
			case 3:
				audio.PlayOneShot(menuSelect, 0.7f);
				GotoControlsMenu();
				break;

			// exit
			case 4:
				audio.PlayOneShot(menuSelect, 0.7f);
				Application.Quit();
				break;
		}
	}

	// sets the target position for camera movement to the Main menu
	// @ Return void
	public void GotoMainMenu()
	{
		menuType = 0;
		camTarget = menuMain;
		BeginAnimation();
	} 

	// sets the target position for camera movement to the options menu
	// @ Return void
	public void GotoOptionsMenu()
	{
		menuType = 1;
		camTarget = menuOptions;
		BeginAnimation();
		
	}

	// sets the target position for camera movement to the credits menu
	// @ Return void
	public void GotoCreditsMenu()
	{
		menuType = 2;
		camTarget = menuCredits;
		BeginAnimation();
		
	}

	// sets the target position for camera movement to the Controls menu
	// @ Return void
	public void GotoControlsMenu()
	{
		menuType = 3;
		camTarget = menuControl;
		BeginAnimation();
		
	}

	// changes the position of the camera to move to the selected screen
	// @ Return void
	void BeginAnimation()
	{
		transitionTimeElapsed = 0;
		transitionStartPosition = transform.position;
	}


	// updates the options display to convey the current volume
	//@Return void
	void UpdateMenu()
	{
		
		textVolume.text = "Volume: " + volume + "%";
	}

	// sets the current volume of the game
	//@Return void
	void UpdateVolume()
	{
		AudioListener.volume = volume / 100f;
	}
}
