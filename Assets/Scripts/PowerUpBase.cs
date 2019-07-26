using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PowerUpBase : MonoBehaviour
{
    public string powerUpName;
    
    public AudioClip soundclip;
	// Our audio mixer
	public AudioMixer audioMixer;

    public PlayerBehaviour Ship;

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

            Ship = other.GetComponent<PlayerBehaviour>();

            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            doSpecialEffects( Ship );

            if( expiresOnTime )
            {
                coroutine = CountDown( Ship );
                StartCoroutine(coroutine);
            }
            else 
            {
                powerUpEffect( Ship );
            }

        } 
        // Otherwise don't do anything
        else 
        {
            return;
        }
    }

    /*
        Kind of a stub function - the specific powerup classes do most of this work
     */
    protected virtual void powerUpEffect( PlayerBehaviour player )
    {
        Debug.Log( "Added" );
    }

    /*
        Destroy the powerup
     */
    protected virtual void removePowerUpEffect( PlayerBehaviour player )
    {
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
