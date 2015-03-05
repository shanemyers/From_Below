using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	// all public variables that can be set for the player
	public GameObject textController;
	public string levelName = "";
	public string levelName2 = "";
	public Transform feet1;
	public Transform feet2;
	public Transform hand;
	public Transform head;
	public LayerMask groundLayers;
	public LayerMask wallLayers;
	public float speed = 0;
	public float jumpJuiceNum = 0;
	public float wallJuiceNum = 0;
	public float gravity = 0;

	// speed an jumping numbers of the player
	float speedX = 0;
	float speedY = 0;
	float jumpJuice = 0;
	float wallJuice = 0;
	float wallJump = 0;

	bool prevOnWall = false;
	bool hitHead = false;
	bool gameOver = false;
	bool pauseDown = false;

	// audio variables
	public AudioClip win;
	public AudioClip lose;
	public AudioClip pausing;

	// function runs upon creation of this object
	// makes sure that not previos iterations of the game will kepp this new one from running
	// @Return void
	void Start()
	{
		GameSettings.gameOver = false;
		GameSettings.pause = false;
		GameSettings.victory = false;

	}

	// physics update of the player
	// tests for player collision with any terrain object
	// and defines its movement speed
	// @ Return void
	void FixedUpdate () {

		// tests to see if the player needs a reference to the text controller
		if(textController == null)
		{
			textController = GameObject.FindWithTag ("textController");

		}


		// game will player if it isn't paused, over or beaten
		if (!GameSettings.pause)
		{

			if (!GameSettings.gameOver) 
			{

				if (!GameSettings.victory) 
				{

					bool onGround = Physics2D.Linecast (transform.position, feet1.position, groundLayers) 
							     || Physics2D.Linecast (transform.position, feet2.position, groundLayers);

					bool onWall = Physics2D.Linecast (transform.position, hand.position, wallLayers);

					bool hitHead = Physics2D.Linecast (transform.position, head.position, groundLayers) 
								|| Physics2D.Linecast (transform.position, head.position, wallLayers);

					// gets the directional input from the player
					float moveH = Input.GetAxis ("Horizontal");
	
					// gets the spacebar or 'A' button press input from the player
					float jump = Input.GetAxisRaw ("Jump");

					// sets the horizontal speed of the player
					speedX = moveH * speed * Time.deltaTime;

					// stops the player from sticking to platforms when hit head on
					if (hitHead) 
					{
						jumpJuice = 0;
						wallJump = 0;

						if (speedY > 0)
						{
							speedY = 0;
						}
					}

					// left
					// if facing left, but turning right, on a wall and while not on the ground
					// perform a wall jump
					if (transform.rotation.y == 1 && moveH > 0 && onWall && !onGround)
					{
						wallJump = 3.5f;
						speedY = 0;
						jumpJuice = 0;
					}


					// Right
					// if facing left, but turning right, on a wall and while not on the ground
					// perform a wall jump
					else if (transform.rotation.y == 0 && moveH < 0 && onWall && !onGround) 
					{
						wallJump = 3.5f;
						speedY = 0;
						jumpJuice = 0;
					}


					// allows the player to resist gravity when wall jumping
					if (wallJump > 0) 
					{
						speedY += wallJump;
						wallJump -= .25f;
					}



		
					// flip the player to face the direction of movement for climbing
					if (speedX > 0) 
					{
						transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
						GetComponentInChildren<Animator> ().SetBool ("climbing", true);

					} 
					else if (speedX < 0) 
					{
						transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
						GetComponentInChildren<Animator> ().SetBool ("climbing", true);
					} 
					else 
					{
						GetComponentInChildren<Animator> ().SetBool ("climbing", false);
					}


					// stops the effects gravity if the player is on the ground
					if (onGround) 
					{
						speedY = 0;
					}

					// if on a wall set the climbing animation to true reguardless of speed
					// drain all jump juice to stop normal jumps
					else if (onWall) 
					{
						GetComponentInChildren<Animator> ().SetBool ("climbing", true);

						if (jumpJuice > 0)
						{
							jumpJuice = 0;
						}

						// if the player haas just grabbed a wall
						// refills the wall jump ability and stops gravity for a time
						if (!prevOnWall)
						{
							prevOnWall = true;
							speedY = 0;

							if (jump == 0)
							{
								wallJuice = wallJuiceNum;
							}
						}

						speedY += (gravity / 6) * Time.deltaTime;

					} 
					else
					{
						speedY += gravity * Time.deltaTime;
					}


					// if player is pressing jump
					if (jump > 0) 
					{
						if (jumpJuice > 0) 
						{
							speedY += jumpJuice;
							jumpJuice -= .25f;
						}

						// if player presses arrow key of opposite facing direction, jump in that direction
						if (wallJuice > 0 && onWall)
						{
							speedY += wallJuice;
							wallJuice -= .25f;
						}
					} 
					// if the player is not pressing jump
					else 
					{
						if (onGround)
						{
							jumpJuice = jumpJuiceNum;
						}
						else 
						{
							jumpJuice = 0;
						}
					}

					// sets weather the player is still on a wall or not
					prevOnWall = onWall;

					Vector2 p = rigidbody2D.position;

					p += new Vector2 (speedX, speedY) * Time.deltaTime;
					rigidbody2D.MovePosition (p);
						

					// possible location of player attack logic
					if (Input.GetButton ("Fire1"))
					{
						// attacking not yet implemented
					}


					// tests if the player is requesting a pause
					if(Input.GetButtonDown("Pause") && !pauseDown)
					{
						audio.PlayOneShot(pausing, 0.7f);
						textController.GetComponent<textController>().SetPause();
						GameSettings.pause = true;
						pauseDown = true;
					}
					else
					{
						pauseDown = false;
					}
				}
				else if(GameSettings.victory)
				{
					rigidbody2D.MovePosition (gameObject.transform.position);
					
					if(Input.GetButtonDown("Jump"))
					{
						textController.GetComponent<textController>().RemoveVictory();
						textController.GetComponent<textController>().RemovePause();

						if(levelName2 != "Exit")
						{
							Application.LoadLevel(levelName2);
						}
						else
						{
							Destroy(textController);
							Application.LoadLevel("Title");
							//Application.Quit();
						}
					}
				}
			}
			else if(GameSettings.gameOver)
			{
				rigidbody2D.MovePosition (gameObject.transform.position);

				if(Input.GetButtonDown("Jump"))
				{
					textController.GetComponent<textController>().RemoveRestart();
					textController.GetComponent<textController>().RemovePause();
					Application.LoadLevel(levelName);
				}
			}
		
		}
		else if(GameSettings.pause)
		{

			if(Input.GetButtonDown("Pause") && !pauseDown)
			{
				audio.PlayOneShot(pausing, 0.7f);
				textController.GetComponent<textController>().RemovePause();
				GameSettings.pause = false;
				pauseDown = true;
			}
			else
			{
				pauseDown = false;
			}
		}
	}

	// sets the game over or victory conditions depending on player collision
	//@ returns void
	void OnTriggerEnter2D(Collider2D other)
	{

		// kill player and reset the level (or if implemented reset to check mark)
		if(other.gameObject.tag == "Deadly" && !GameSettings.gameOver && !GameSettings.victory)
		{

			audio.PlayOneShot(lose, 0.7f);

			textController.GetComponent<textController>().SetRestart();

			// play death animation
			if(!gameOver)
			{
				GetComponentInChildren<Animator> ().SetBool ("dead", true);
			}

			GameSettings.gameOver = true;

		}

		// set the victory condition of this level to true and play the victory music
		if(other.gameObject.tag == "Victory" && !GameSettings.victory)
		{
			audio.PlayOneShot(win, 0.7f);
		
			textController.GetComponent<textController>().SetVictory();

			GameSettings.victory = true;
			
		}
	}
}
