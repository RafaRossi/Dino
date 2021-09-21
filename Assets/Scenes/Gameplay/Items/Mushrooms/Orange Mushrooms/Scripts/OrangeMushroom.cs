using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMushroom : Mushroom
{
    protected override void OnPlayerEnter(CharacterController character, Rigidbody2D rb = null, Collision2D collision = null)
    {
        ChangeState(States.Collected);

        if(character.enabled)
        {
            character.Die();

            AudioManager.Instance.PlayFX(hitSound);
        }
    }
}
