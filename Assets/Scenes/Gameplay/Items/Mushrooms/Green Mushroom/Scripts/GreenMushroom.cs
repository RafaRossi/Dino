using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroom : Mushroom, ICollectable
{
    protected Transform player;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float minDistance = 0.1f;
    [SerializeField] private float delayTime = 0.01f;

    public void Collect() // Movimenta o cogumelo assim que próximo até o player, para dar um efeito de atração e deixar mais fácil a coleta.
    {
        StartCoroutine(MoveTowardsPlayer());

        IEnumerator MoveTowardsPlayer()
        {
            yield return new WaitForSeconds(delayTime);

            while (Vector2.Distance(transform.position, player.position) > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                yield return null;
            }

            ChangeState(States.Collected);

            AudioManager.Instance.PlayFX(hitSound);

            LevelManager.Instance.CollectGreenMushroom();
        }
    }

    protected override void OnPlayerEnter(CharacterController character, Rigidbody2D rb = null, Collision2D collision = null)
    {
        player = character.transform;

        Collect();
    }

}
