using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Sound drown = null;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterController>(out var character))
        {
            if (character.enabled)
            {
                character.Die();

                AudioManager.Instance.PlayFX(drown);
            }
        }
    }
}
