using UnityEngine;

public abstract class Board : MonoBehaviour // Classe basica para ponto de check point/respawn
{
    [SerializeField] protected Sound sound = default;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterController character))
        {
            OnPlayerEnter(character);
        }
    }

    public virtual void OnPlayerEnter(CharacterController character = null)
    {
        AudioManager.Instance.PlayFX(sound);
    }
}

public class CheckPoint : Board
{
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Collider2D _collider = null;
    [SerializeField] private Animator animator = null;

    public override void OnPlayerEnter(CharacterController character)
    {
        base.OnPlayerEnter();
        GameManager.Instance.ReachNewCheckPoint(this); //Atribui esse check point como o atual para respawn do jogador

        animator.SetTrigger("CheckPointReached");
        _collider.enabled = false; //desativa o colisor para não ficar chamado a função toda vez que o jogar passar por ele
    }

    public Vector3 GetSpawnPoint() => spawnPoint.position;
}
