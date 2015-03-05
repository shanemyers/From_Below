using UnityEngine;
using System.Collections;

public class KeepObject : MonoBehaviour {

	// this makes an instance of this class to store for scene exchange
	private static KeepObject instance = null;
	
	public static KeepObject  Instance {
		
		get { return instance; }
		
	}

	// when this starts up the object will delete any exsisting instances of itself
	// this makkes sure there aren't multiple instances of sound
	//@Return void
	void Awake() 
	{
			
		if (instance != null && instance != this)
		{
				
			Destroy(this.gameObject);
			return;
				
		} else
		{
				
				instance = this;
			
		}

		// this object will not be destroyed on the loading of another scene
		DontDestroyOnLoad(this.gameObject);
		
	}

	// when the object is loaded it will play any audio attatched to it
	//@Return void
	public void PlayNew(AudioClip clip)
	{
	
		this.audio.clip = clip;
	
		this.audio.Play ();
	
	}

}
