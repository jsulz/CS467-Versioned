using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{

    public GameObject PauseScreenOverlay;
    public AudioMixer mixer;
    public static bool GamePaused = false;

    PlayerBehaviour player;

    void Start()
    {
        GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		player = player_go.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) ){
            if( GamePaused && !player.dead ){
                Resume();
            } else if( !player.dead ) {
                Pause();
            }
        }
        
    }

    /*
        Click Handler to the "Resume" button in the pause menu
        If the user presses "Escape" while in the pause menu, or clicks on this button
        the game returns to playing
     */
    public void Resume()
    {
        PauseScreenOverlay.SetActive( false );
        Time.timeScale = 1f;
        GamePaused = false; 
    }


    /*
        Handles the player pressing "Escape" during the game
        If the player does, the game pauses and an overlay menu appears
     */
    public void Pause()
    {
        PauseScreenOverlay.SetActive( true );
        Time.timeScale = 0f;
        GamePaused = true;        

    }

    /*
        Click Handler to the "Restart Level" button in the pause menu
        If the player clicks this element, this click handler fires and the level reloads
     */
    public void Restart()
    {

        PauseScreenOverlay.SetActive( false );
        Time.timeScale = 1f;
        GamePaused = false;
        SceneManager.LoadScene( "Scene" );

    }

    /*
        Click Handler to the "Exit to Main Menu" button in the pause menu
        If the player clicks this element, this click handler fires and the Menu scene loads
     */
    public void ExitGame()
    {

        Debug.Log("We are quitting. Comment this out as we build.");
        Application.Quit();

    }
    /*
        Click Handler to the "Quit" button in the main menu
        If the player clicks this element, the application closes. 
        Since we don't have the application built yet, this won't work
     */
    public void UpdateVolume( float volume )
    {
        mixer.SetFloat( "mainVolume", volume );
    }
}
