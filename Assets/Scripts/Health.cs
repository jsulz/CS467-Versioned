using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Health : PowerUpBase
{
    /*
        Reset the player's size to their original size when they pick this up.
        Make sure to set the mass as we do this so we don't run into a problem with the
        player getting smaller after we reset. 
     */
    protected override void powerUpEffect( PlayerBehaviour player )
    {
        base.powerUpEffect( player );
        player.transform.localScale = new Vector3( 1, 1, 1 );
        player.GetComponent<Rigidbody2D> ().mass = 1;
    }

}