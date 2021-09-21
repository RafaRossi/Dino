using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public int remainingLifes = 3;
    public int deathTimes = 0;
    public int minDeathsToScore = 3;

    public int cratesBroken = 0;
    public int maxCrates = 0;

    public bool yellowMushroomCollected = false;

    public float time = 0f;
}
