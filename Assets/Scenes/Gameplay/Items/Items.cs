using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum States
{
    Available,
    Collected
}

public interface ICollectable
{
    void Collect();
}

public abstract class Items : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider2D[] _collider;

    [Header("State")]
    [SerializeField] private States currentState = States.Available;

    [Header("Sounds")]
    [SerializeField] protected Sound hitSound = default;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponents<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;

        if (collision.TryGetComponent<CharacterController>(out var character))
        {
            OnPlayerEnter(character, rb);
        }
    }

    protected abstract void OnPlayerEnter(CharacterController character, Rigidbody2D rb = null, Collision2D collision = null);

    protected virtual void OnEnable()
    {
        GameManager.Instance.onReacheNewCheckPoint += UpdateItemState;
        GameManager.Instance.onResetItems += ResetItem;

        LevelManager.Instance.AddItemState(this, GetCurrentState());
    }

    protected virtual void UpdateItemState()
    {
        LevelManager.Instance.objectsStates[this] = GetCurrentState();
    }

    protected virtual void ResetItem()
    {
        ChangeState(LevelManager.Instance.objectsStates[this]);
    }

    public virtual void ChangeState(States state)
    {
        if (GetCurrentState() != state)
        {
            currentState = state;

            bool active = state == States.Available;

            _renderer.enabled = active;

            for(int i = 0; i < _collider.Length; i++)
            {
                _collider[i].enabled = active;
            }
        }
    }

    public States GetCurrentState() => currentState;
}
