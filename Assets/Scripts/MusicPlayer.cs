using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour {
	public AudioClip[] clips;
	private AudioSource audioSource;
	PlayerBehaviour player;

	// Need to add the master mixer here and in the component view as well
	public AudioMixer audioMixer;

	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		player = player_go.GetComponent<PlayerBehaviour>();
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
	}

	private AudioClip GetRandomClip() {
		return clips [Random.Range (0, clips.Length)];
	}
	
	void Update () {
		// Getting the master mixer
		AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");
		// And make sure we play through the proper mixer
		audioSource.outputAudioMixerGroup = audioMixGroup[0];

		if (!audioSource.isPlaying && Time.timeScale == 1 && !player.dead) {
			audioSource.clip = GetRandomClip ();
			audioSource.Play ();
		}
		if (player.dead) {
			audioSource.Stop ();
		}			
	}
}
