using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : Manager<HUDManager>
{
    private GameManager gameManager = null;
    private LevelManager levelManager = null;

    [Header("Text Fields")]
    public TMP_Text lifesCounter = null;
    public TMP_Text cratesCounter = null;
    public TMP_Text mushroomsCounter = null;
    public Image yellowMushroomHolder = null;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        levelManager = LevelManager.Instance;
    }

    private void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        lifesCounter.text = gameManager.PlayerLifes.ToString();
        cratesCounter.text = levelManager.CratesAmount.ToString() + "/" + levelManager.maxCratesAmount.ToString();
        mushroomsCounter.text = levelManager.GreenMushroomsCollected.ToString();

        yellowMushroomHolder.gameObject.SetActive(levelManager.YellowMushroomCollected != null);
    }
}
