using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Sound uiButton = null;
    public void PlayButton()
    {
        AudioManager.Instance.PlayFX(uiButton);

        SceneManager.Instance.ChangeScene(1);
    }

    public void CloseGame()
    {
        AudioManager.Instance.PlayFX(uiButton);

        Application.Quit();
    }
}
