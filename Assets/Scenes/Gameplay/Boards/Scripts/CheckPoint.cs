using UnityEngine;

public abstract class Board : MonoBehaviour
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

    public override void OnPlayerEnter(CharacterController character)
    {
        base.OnPlayerEnter();
        GameManager.Instance.ReachNewCheckPoint(this);

        _collider.enabled = false;
    }

    public Vector3 GetSpawnPoint() => spawnPoint.position;
}
