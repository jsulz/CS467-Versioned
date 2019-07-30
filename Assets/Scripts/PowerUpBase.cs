using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PowerUpBase : MonoBehaviour
{
    // We don't use this currently, but it's just a string; needs to be set in Unity
    public string powerUpName;
    
    // Needs to be added on the prefab in unity so we know what clip to play
    public AudioClip soundclip;
	// Our audio mixer - needs to be added on the prefab in unity so we know what mixer to user
	public AudioMixer audioMixer;

    // A reference to our player - we grab it on collision, so it's not necessary to worry about doing anything in Unity
    public PlayerBehaviour player;

    // Some class variables that we use to determine if the powerup is destroyed immediately or after some time
    // set in Unity or in the inherited class
    public bool expiresOnTime;
    public bool destoryable = false;
    private IEnumerator coroutine;

    void Update () {
		// Check for game being paused before we delete the enemies
		if (Time.timeScale == 0 && !PauseScreen.GamePaused ) 
        {
            Destroy(gameObject);
        }
	}

    /*
        When a player hits a powerup, disable the visible/interactable components of the powerup
        and apply the powerup either immediately or over the course of time.
     */
    protected virtual void OnTriggerEnter2D( Collider2D other )
    {

        // if the game object has been tagged as a player
        if( other.gameObject.tag == "Player" )
        {

            // Then get the player
            player = other.GetComponent<PlayerBehaviour>();

            // Remove the visible components of the powerup
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            // Do whatever special effects are needed
            doSpecialEffects( player );

            // And determine how to manage the powerup
            if( expiresOnTime )
            {
                //coroutine = CountDown( player );
                //StartCoroutine(coroutine);
            }
            else 
            {
                //powerUpEffect( player );
            }

        } 
        // Otherwise don't do anything
        else 
        {
            return;
        }
    }

    /*
        Kind of a stub function - the specific powerup classes do most of this work this is just here in case we need it
     */
    protected virtual void powerUpEffect( PlayerBehaviour player )
    {
    }

    /*
        Destroy the powerup and reset animation
     */
    protected virtual void removePowerUpEffect( PlayerBehaviour player )
    {
        // Then we destroy the powerup
        if( destoryable && !expiresOnTime )
        {
            Destroy( gameObject.transform.root.gameObject );
        }
        // and in all cases, reset the animation back
        player.setAnimation("Normal");
    }

    /*
        Play an audio clip when hitting a powerup
     */
    protected virtual void doSpecialEffects( PlayerBehaviour player )
    {
		// Getting the master mixer
		AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");
		AudioSource soundfx = gameObject.GetComponent<AudioSource>();
		// And make sure we play through the proper mixer
		soundfx.outputAudioMixerGroup = audioMixGroup[0];
        // Then play our single powerup clip
       soundfx.PlayOneShot(soundclip, 1.0f);

    }

    /*
        If the powerup is one that should work over a period of time then we 
        call the effect, wait for 10 seconds, destroy the game object and remove the powerup effect if needed
     */
    protected virtual IEnumerator CountDown( PlayerBehaviour player )
    {

        powerUpEffect( player );
        yield return new WaitForSeconds(10);
        removePowerUpEffect( player );
        Destroy( gameObject.transform.root.gameObject );

    }
}
