using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bomb : PowerUpBase 
{

    /*
        Locate all game objects in the scene, only target those that are in a range, then destroy them.
        Leaned on the Unity documentation to make this work https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
        
        @todo refactor and test this with destroying the enemy in the first loop
     */
    void RangeDestruct()
    {
        // Container to hold all enemies
        GameObject[] AllSpawnedEnemies;
        // Container to hold enemies we want to kill
        List<GameObject> EnemiesToDestruct = new List<GameObject>();
        // Get all of the enemies that have been spawned
        AllSpawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        // The distance we use to determine if we need to kill the enemy
        float distance = 100f;
        // our current position
        Vector3 position = transform.position;
        // Loop over all of the spawned enemies
        foreach (GameObject go in AllSpawnedEnemies)
        {
            // Get the distance from the player to the enemy 
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            // If the enemy is in range
            if (curDistance < distance)
            {
                // Add them to our other container
                EnemiesToDestruct.Add( go );
            }
        }

        // Then loop over and destroy the enemy
        foreach (GameObject go in EnemiesToDestruct )
        {
            Destroy( go );
        }

    }

    /*
        Set some variables when the player picks this up
     */
    protected override void powerUpEffect( PlayerBehaviour player )
    {
        base.powerUpEffect( player );
        destoryable = false;
        RangeDestruct();
    }

    /*
        After the player has fired this, then destroy it.
     */
    protected override void removePowerUpEffect( PlayerBehaviour player )
    {
        destoryable = true;
        base.removePowerUpEffect( player );
    }

}