using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bomb : PowerUpBase 
{

    /*
        Locate all game objects in the scene, only target those that are in a range, then destroy them.
        Leaned on the Unity documentation to make this work https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
     */
    void RangeDestruct()
    {
        GameObject[] AllSpawnedEnemies;
        List<GameObject> EnemiesToDestruct = new List<GameObject>();
        AllSpawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = 100f;
        Vector3 position = transform.position;
        foreach (GameObject go in AllSpawnedEnemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                EnemiesToDestruct.Add( go );
            }
        }

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