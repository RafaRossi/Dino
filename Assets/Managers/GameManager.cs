using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [Header("Player")]
    public CharacterController player;
    public bool isDead = false;

    [SerializeField] private int playerLifes = 3;
    public int PlayerLifes
    {
        get => playerLifes;

        set
        {
            playerLifes = value;

            if(playerLifes < 0)
            {
                playerLifes = 0;
            }

            if(playerLifes > maxPlayerLifes)
            {
                playerLifes = maxPlayerLifes;
            }
            HUDManager.Instance.UpdateHUD();
        }
    }
    
    [SerializeField] private const int maxPlayerLifes = 99;
    [SerializeField] private const int minPlayerLifes = 0;

    private float timeToRespawn = 4f;
    private int deathCounter = 0;

    [SerializeField] private Sound lifeUp = null;

    [Header("Check Point Options")]
    [SerializeField] private CheckPoint lastCheckPoint = null;

    public delegate void PlayerEvents();
    public PlayerEvents onReacheNewCheckPoint;
    public PlayerEvents onPlayerDie;
    public PlayerEvents onResetItems;

    public delegate void EndLevelEvent();
    public EndLevelEvent onLevelEnd;

    public delegate void WatchAdCallBack();
    public WatchAdCallBack onWatchRewardedVideo;

    [Header("Pause Options")]
    [SerializeField] private GameObject pauseMenu = null;

    private void Start()
    {
        AnalyticsManager.ReportLevelState(LevelState.Started);

        player.transform.SetPositionAndRotation(GetLastCheckPointPosition(), Quaternion.identity);
        player.enabled = true;
    }

    public void ReachNewCheckPoint(CheckPoint newCheckPoint) //Chamado toda vez que o jogador atinge um novo check point
    {
        lastCheckPoint = newCheckPoint;

        onReacheNewCheckPoint();
    }

    public void PlayerDie() //Função para quando o jogador morre
    {
        isDead = true;

        deathCounter++;

        SceneManager.Instance.Fade("Fade Out", timeToRespawn/2); //Chama o fade, com um delay de alguns segundos

        StartCoroutine(PlayerDie());

        IEnumerator PlayerDie()
        {
            yield return new WaitForSeconds(timeToRespawn);

            onResetItems(); //Reseta o estado de todos os itens coletados após o ultimo check point
            onPlayerDie();

            if (PlayerLifes >= 1) //Verifica se o jogador tem vidas sobrando
            {
                ResetToCheckPoint();
                PlayerLifes--;

                SceneManager.Instance.Fade(SceneManager.Instance.fadeInAnimation);
            }
            else // Se ele morreu sem ter nenhuma vida sobrando, Game Over
            {
                GameOverManager.Instance.OnGameOver();
            }
        }
    }

    public void LifeUp() //Jogador conseguiu 100 cogumelos, recebe uma vida extra
    {
        PlayerLifes++;
        AudioManager.Instance.PlayFX(lifeUp);
    }

    private void ResetToCheckPoint()
    {
        player.Respawn();
    }

    public void EndLevel()
    {
        onLevelEnd();

        AnalyticsManager.ReportLevelState(LevelState.Won);
        SceneManager.Instance.ChangeScene(2);
    }

    public Vector3 GetLastCheckPointPosition() => lastCheckPoint.GetSpawnPoint();

    public int GetDeathCount() => deathCounter;

    public void RewardVideo() //Dá ao jogador mais uma tentativa caso termine de assistir por inteiro o ad
    {
        ResetToCheckPoint();
        SceneManager.Instance.Fade(SceneManager.Instance.fadeInAnimation);

        onWatchRewardedVideo();
    }

    public void Pause(bool pause)
    {
        pauseMenu.SetActive(pause);

        Time.timeScale = pause ? 0 : 1;
    }
}
