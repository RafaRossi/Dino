using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : Manager<GameOverManager>
{
    public GameObject gameOverPanel = null;
    [SerializeField] private Sound gameOverSound = null;

    [SerializeField] private Button adButton = null;
    [SerializeField] private TMP_Text adWatchedText = null;
    [SerializeField] private string displayedTextOnWatched = "You've already watch the ad!";

    private void OnEnable()
    {
        GameManager.Instance.onWatchRewardedVideo += () =>
        {
            adButton.interactable = false;
            adWatchedText.text = displayedTextOnWatched;
            gameOverPanel.SetActive(false);

            AnalyticsManager.ReportLevelState(LevelState.WatchAd);
        };
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);

        AudioManager.Instance.PlayFX(gameOverSound);

        AnalyticsManager.ReportLevelState(LevelState.Loose);
    }
}
