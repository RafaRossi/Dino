using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState //Estado do Level para Analytics
{
    Started,
    Won,
    Loose,
    WatchAd
}

public class LevelManager : Manager<LevelManager>
{
    public Dictionary<Items, States> objectsStates = new Dictionary<Items, States>(); //Dicionario de todos os itens no nivel, e seus respectivos estados
                                                                                       //Utilizado principlamente quando o jogador morre.

    [Header("Level Info")]
    [SerializeField] private PlayerData data = null;
    private int minDeathsToScore = 3;
    private float startedTime = 0;

    [Header("Crates")]
    public int maxCratesAmount = 0; //Quantidade maxima de caixas disponiveis no nivel, seu valor é setado automaticamente

    private int createsAmount = 0; // Propriedade de contagem de quantas caixas o jogador destruiu
    public int CratesAmount {
        get => createsAmount;

        set 
        {
            createsAmount = value;

            HUDManager.Instance.UpdateHUD();
        }
    }

    [Header("Mushrooms")]
    private int greenMushroomsCollected = 0; //Contador de cogumelos verdes que o jogador coletou, chegando na quantidade maxima (const maxGreenMushroom), zera-se a contagem e ganha uma vida extra
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

    private YellowMushroom yellowMushroom = null; //Referencia de objeto do cogumelo amarelo, unico por nivel. Refencia ao objeto em si para caso queira-se adicionar mais funcionalidades que dependam da classe no futuro.
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
        objectsStates.Add(item, state);//Adiciona um item e seu estado inicial na lista de itens
    }

    private List<T> FindAllItemsOfType<T>() where T : Items //Procura todos os itens de um determinado tipo nas chaves do dicionario, depois retorna os itens encontrados como uma lista.
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

    public void ResetCrateAmount() //Procura todas as caixas no dicionario, e ve quais delas estão com os estados como coletados, retorna a quantidade encontrada. Usado quando o player morre e tem que resetar as caixas que foram coletadas, mas não tiveram seu estado atualizado no dicionario.
    {
        var allCrates = FindAllItemsOfType<Crates>();

        CratesAmount = allCrates.FindAll(c => c.GetCurrentState() == States.Collected).Count;
    }

    public void CollectGreenMushroom()
    {
        GreenMushroomsCollected++;
    }

    public void SavePlayerData() //Salva as informações do jogador no level para a tela de pontuação e analytics
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
