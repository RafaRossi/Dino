using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : Items, ICollectable
{
    [Header("Physics")]
    [SerializeField] private float impulseForce = 20;

    [Header("Drops")]
    [SerializeField] private GreenMushroom prefab = null;
    [SerializeField] private int maxPrefabsSpawn = 5;
    [SerializeField] private float spawnRadius = .5f;

    public void Collect()
    {
        ChangeState(States.Collected);

        SpawnDrops();

        AudioManager.Instance.PlayFX(hitSound);

        LevelManager.Instance.CollectCrate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;

        if (rb.TryGetComponent<CharacterController>(out var character))
        {
            OnPlayerEnter(character, rb, collision);
        }
    }

    protected override void OnPlayerEnter(CharacterController character, Rigidbody2D rb = null, Collision2D collision = null)
    {
        //Verifica a direção da colisão do jogador, as caixas só são quebradas se o jogador pular em cima delas, ou dar uma cabeçada por baixo delas, 
        //assim como nos jogos do Crash. Colidir nos lados, ou até mesmo andar por cima delas sem estar caindo, não as destroem.
        Vector2 normal = collision.GetContact(0).normal;

        if (!character.IsGrounded && (normal.normalized == Vector2.up || normal.normalized == Vector2.down))
        {

            Vector2 dir = collision.GetContact(0).point - (Vector2)character.transform.position;
            dir = -dir.normalized;

            rb.velocity = Vector2.zero;

            if(dir.y < 0)
            {
                dir /= Physics.gravity.y;
            }
            else
            {
                character.IsGrounded = true; //Reseta a variavel, permitindo o jogador realizar mais pulos
            }
            
            rb.AddForce(dir * impulseForce * Time.deltaTime, ForceMode2D.Impulse);

            Collect();
        }
    }

    private void SpawnDrops() //Randomiza uma quantidade de cogumelos verdes assim que destruida
    {
        int randomAmount = Random.Range(0, maxPrefabsSpawn+1);

        for (int i = 0; i < randomAmount; i++)
        {
            Instantiate(prefab, (Vector2)transform.position + Random.insideUnitCircle * spawnRadius, Quaternion.identity);
        }
    }
}
