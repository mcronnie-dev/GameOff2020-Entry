using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int index = (nextIndex >= SceneManager.sceneCountInBuildSettings) ? 0 : nextIndex;
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // start animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // play scene
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
