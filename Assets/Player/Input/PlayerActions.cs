using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Mobile,
    Platform
}

public class PlayerActions : MonoBehaviour //Desativa os inputs de teclado/mouse etc caso o jogo esteja rodando em dispositivos mobile
{                                           //Script para controlar os inputs do jogador
    #if UNITY_EDITOR
        InputType type = InputType.Platform;
    #elif UNITY_ANDROID || UNITY_IOS
        InputType type = InputType.Mobile;
    #else
        InputType type = InputType.Platform;
    #endif

    [SerializeField] private CharacterController character = null;
    void Update()
    {
        if(type == InputType.Platform)
        {
            SetDirection(Input.GetAxisRaw("Horizontal"));
            CallJump(Input.GetButtonDown("Jump"));
        }
    }

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    public void SetDirection(float direction)
    {
        character.SetDirection(direction);
    }

    public void CallJump(bool jump)
    {
        if(jump)
        {
            character.Jump();
        }
    }
}
