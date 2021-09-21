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


    //Atualiza o estado do item no dicionario, assim, o estado continuará o mesmo caso o jogador morra, 
    //não precisando assim coletar o item novamente
    protected virtual void UpdateItemState() 
    {
        LevelManager.Instance.objectsStates[this] = GetCurrentState();
    }
    //Reseta o item no estado que está salvo no dicionario, chamado principlamente quando o jogador morre. 
    //Ou seja, se o jogador tiver coletado um item, mas não tiver atingido um novo check point, 
    //o item terá seu estado revertido, e terá que ser coletado novamente.
    
    protected virtual void ResetItem() 
    {
        ChangeState(LevelManager.Instance.objectsStates[this]);
    }

    //Altera o estado atual do item, mas ainda não o atualiza no dicionario no LevelManager, o estado só é atualizado quando 
    //o jogador atinge um novo check point.
   
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
