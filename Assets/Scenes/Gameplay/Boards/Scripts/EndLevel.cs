using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : Board
{
    public override void OnPlayerEnter(CharacterController character)
    {
        base.OnPlayerEnter();

        GameManager.Instance.EndLevel();
    }
}
