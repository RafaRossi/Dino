using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMushroon : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private Sound bounceSound = null;

    [Header("Animations")]
    [SerializeField] private Animator animator = null;

    [Header("Physics")]
    [SerializeField] private float impulseForce = 20;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;

        if (collision.TryGetComponent<CharacterController>(out var character))
        {
            OnPlayerEnter(character, rb);
        }
    }

    protected void OnPlayerEnter(CharacterController character, Rigidbody2D rb = null, Collision2D collision = null)
    {
        if (!character.IsGrounded && rb.velocity.y < 0)
        {
            character.IsGrounded = true;

            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * impulseForce * Time.deltaTime, ForceMode2D.Impulse);

            animator.SetTrigger("Bounce");
            AudioManager.Instance.PlayFX(bounceSound);
        }
    }
}
