using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EndScreenScript : MonoBehaviour {
	public AudioClip gameOver;
	AudioSource audioSource;

	// Our audio mixer
	public AudioMixer audioMixer;


	PlayerBehaviour player;
	public float deathCooldown = 2f;

	static int score;
	static int highScore;

	// Pause screen object
	PauseScreen pause;

	void Start () {
		// Load up the audio mixer
		AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");

		audioSource = GetComponent<AudioSource>();

		// play the audio clips through the master mixer
		audioSource.outputAudioMixerGroup = audioMixGroup[0];

		GetComponent<Text> ().enabled = false;
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		player = player_go.GetComponent<PlayerBehaviour>();
	}
	
	void Update () {
		if (player.dead) {

			// Pull in our pause screen script so we can call a method in it
			pause = GameObject.FindObjectOfType(typeof(PauseScreen)) as PauseScreen;	

			deathCooldown -= Time.deltaTime;
			if (deathCooldown <= 0f) {
				Time.timeScale = 0;
				GetComponent<Text>().enabled = true;
				audioSource.clip = gameOver;
				audioSource.loop = true;
				if (!audioSource.isPlaying) audioSource.Play();

				// Added in the ability to access the menu from the end screen
				// If the game is paused, then we don't want to show the end screen text
				if( !PauseScreen.GamePaused ){
					GetComponent<Text>().text = "Distance: " + Score.score + "\nBest Distance: " + Score.highScore +"\n\nTap to Restart" + "\n\nPress 'M' for Menu";
				} else{
					// Clear our text
					GetComponent<Text>().enabled = false;
				}
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !PauseScreen.GamePaused ) {
					SceneManager.LoadScene("Scene");
				} else if( Input.GetKeyDown( KeyCode.M ) ){
					// Call our pause method from the pause screen script
					// So we can load our menu
					pause.Pause();
				}
		
			}
		}
	}
}
