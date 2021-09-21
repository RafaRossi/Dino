using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroom : Mushroom, ICollectable
{
    protected Transform player;
    public virtual void Collect()
    {
        ChangeState(States.Collected);

        AudioManager.Instance.PlayFX(hitSound);

        LevelManager.Instance.CollectGreenMushroom();
    }

    protected override void OnPlayerEnter(CharacterController character, Rigidbody2D rb = null, Collision2D collision = null)
    {
        player = character.transform;

        Collect();
    }

}
