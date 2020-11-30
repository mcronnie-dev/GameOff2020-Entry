using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MagicStone : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    public int StoneCount;
    public GameObject[] stoneAware;

    public void SetStoneCount(int newStoneCount)
    {
        foreach (GameObject go in stoneAware)
        {
            go.SendMessage(nameof(SetStoneCount), newStoneCount, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void ModifyStoneCount()
    {
        StoneCount += 1;

        SetStoneCount(StoneCount);

        if (StoneCount == 10) {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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

    public void Win()
    {

    }

}
