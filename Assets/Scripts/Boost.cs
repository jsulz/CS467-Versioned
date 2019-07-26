using UnityEngine;
using System.Collections;
using System;

public class Boost : PowerUpBase
{

    /*
        Slow time down by 50%
     */
    protected override void powerUpEffect( PlayerBehaviour player )
    {
        base.powerUpEffect( player );
        Time.timeScale *= 0.5f;
    }

    /*
        Speed time back up to 100%
     */
    protected override void removePowerUpEffect( PlayerBehaviour player )
    {
        base.removePowerUpEffect( player );
        Time.timeScale = 1.0f;
    }

    /*
        Add our unique animation
     */
    protected override void doSpecialEffects( PlayerBehaviour player )
    {
		base.doSpecialEffects( player );
        player.setAnimation( "Boost" );

    }

}