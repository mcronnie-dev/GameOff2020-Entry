using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Moonster : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    public int MoonsterCount;
    public GameObject[] moonsterAware;

    public void SetMoonsterCount(int newMoonsterCount)
    {
        foreach (GameObject go in moonsterAware)
        {
            go.SendMessage(nameof(SetMoonsterCount), newMoonsterCount, SendMessageOptions.DontRequireReceiver);
        }
    }


    public void ModifyMoonsterCount()
    {
        MoonsterCount += 1;
        print("moonster count " + MoonsterCount);

        SetMoonsterCount(MoonsterCount);

        if (MoonsterCount == 9)
        {
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
}
