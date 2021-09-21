using System.Collections.Generic;
using UnityEngine;

public abstract class Mushroom : Items
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;

        if (collision.TryGetComponent<CharacterController>(out var character))
        {
            OnPlayerEnter(character, rb);
        }
    }
}

public class YellowMushroom : Items, ICollectable
{
    protected override void OnPlayerEnter(CharacterController character, Rigidbody2D rb = null, Collision2D collision = null)
    {
        Collect();
    }

    protected override void ResetItem()
    {
        base.ResetItem();

        LevelManager.Instance.YellowMushroomCollected = null;
    }


    public void Collect()
    {
        ChangeState(States.Collected);

        AudioManager.Instance.PlayFX(hitSound);
        LevelManager.Instance.YellowMushroomCollected = this;
    }

    public override void ChangeState(States state)
    {
        base.ChangeState(state);

        if(state == States.Available)
        {
            LevelManager.Instance.YellowMushroomCollected = null;
        }
    }
}
