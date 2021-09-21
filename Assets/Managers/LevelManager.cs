using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState
{
    Started,
    Won,
    Loose,
    WatchAd
}

public class LevelManager : Manager<LevelManager>
{
    public Dictionary<Items, States> objectsStates = new Dictionary<Items, States>();

    [Header("Level Info")]
    [SerializeField] private PlayerData data = null;
    private int minDeathsToScore = 3;
    private float startedTime = 0;

    [Header("Crates")]
    public int maxCratesAmount = 0;

    private int createsAmount = 0;
    public int CratesAmount {
        get => createsAmount;

        set 
        {
            createsAmount = value;

            HUDManager.Instance.UpdateHUD();
        }
    }

    [Header("Mushrooms")]
    private int greenMushroomsCollected = 0;
    public int GreenMushroomsCollected
    {
        get => greenMushroomsCollected;

        set
        {
            greenMushroomsCollected = value;

            if (greenMushroomsCollected >= maxGreenMushroom)
            {
                greenMushroomsCollected -= maxGreenMushroom;
                GameManager.Instance.LifeUp();
            }

            HUDManager.Instance.UpdateHUD();
        }
    }

    public const int maxGreenMushroom = 100;

    private YellowMushroom yellowMushroom = null;
    public YellowMushroom YellowMushroomCollected 
    { 
        get => yellowMushroom; 

        set 
        {
            yellowMushroom = value;

            HUDManager.Instance.UpdateHUD();
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.onPlayerDie += ResetCrateAmount;
        GameManager.Instance.onLevelEnd += SavePlayerData;
    }

    private void Start()
    {
        SetMaxCratesAmount();

        startedTime = Time.time;
    }

    public void AddItemState(Items item, States state)
    {
        objectsStates.Add(item, state);
    }

    private List<T> FindAllItemsOfType<T>() where T : Items
    {
        List<Items> returnedList = new List<Items>(objectsStates.Keys);
        return returnedList.FindAll(item => item is T).ConvertAll(item => (T)item);
    }

    private void SetMaxCratesAmount()
    {
        maxCratesAmount = FindAllItemsOfType<Crates>().Count;
    }

    public void CollectCrate()
    {
        CratesAmount++;
    }

    public void ResetCrateAmount()
    {
        var allCrates = FindAllItemsOfType<Crates>();

        CratesAmount = allCrates.FindAll(c => c.GetCurrentState() == States.Collected).Count;
    }

    public void CollectGreenMushroom()
    {
        GreenMushroomsCollected++;
    }

    public void SavePlayerData()
    {
        data.cratesBroken = CratesAmount;
        data.maxCrates = maxCratesAmount;

        data.remainingLifes = GameManager.Instance.PlayerLifes;
        data.deathTimes = GameManager.Instance.GetDeathCount();
        data.minDeathsToScore = minDeathsToScore;

        data.yellowMushroomCollected = YellowMushroomCollected != null;
        data.time = Time.time - startedTime;
    }
}
