using System.Collections;
using UnityEngine;

public class SceneManager : Manager<SceneManager>
{
    [SerializeField] private Animator fade = null;
    public Animator Animator
    {
        get
        {
            if (!fade)
            {
                fade = GetComponentInChildren<Animator>();
            }

            return fade;
        }
    }

    public string fadeInAnimation = "Fade In";
    public string fadeOutAnimation = "Fade Out";

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        Fade(fadeOutAnimation);
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);

        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void Fade(string fadeAnimation, float animationDelay = 0f)
    {
        StartCoroutine(Fade(fadeAnimation, animationDelay));

        IEnumerator Fade(string animationName, float animationCallDelay)
        {
            yield return new WaitForSeconds(animationCallDelay);
            Animator.SetTrigger(animationName);
        }
    }
}