using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScreen : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private PlayerData playerData = null;

    [Header("HUD Fields")]

    [SerializeField] private TMP_Text deathCounter = null;
    [SerializeField] private Toggle deathCounterToggle = null;

    [SerializeField] private TMP_Text cratesCounter = null;
    [SerializeField] private Toggle cratesCounterToggle = null;

    [SerializeField] private Toggle yellowMushroomToggle = null;

    [SerializeField] private TMP_Text timeTakenText = null;

    [SerializeField] private Sound uiSoundButton;

    private void Start()
    {
        LoadHUD();
    }

    private void LoadHUD()
    {
        deathCounter.text = playerData.deathTimes.ToString() + "/" + playerData.minDeathsToScore.ToString();
        deathCounterToggle.isOn = playerData.deathTimes <= playerData.minDeathsToScore;

        cratesCounter.text = playerData.cratesBroken.ToString() + "/" + playerData.maxCrates.ToString();
        cratesCounterToggle.isOn = playerData.cratesBroken == playerData.maxCrates;

        yellowMushroomToggle.isOn = playerData.yellowMushroomCollected;

        var time = System.TimeSpan.FromSeconds(playerData.time);
        timeTakenText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);

        AnalyticsManager.ReportPlayerInfo(playerData);
    }

    public void ChangeScene(int levelIndex)
    {
        SceneManager.Instance.ChangeScene(levelIndex);

        AudioManager.Instance.PlayFX(uiSoundButton);
    }
}
