using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextLoader : MonoBehaviour
{
    public Animator transition;

    public bool needText;
    public GameObject questWindow;
    public Text questWindowText;
    public string questText;
    // Start is called before the first frame update
    void Start()
    {
        if (needText)
        {
            StartCoroutine(DisplayQuestWindow());
        }

    }

    IEnumerator DisplayQuestWindow()
    {
        yield return new WaitForSeconds(1f);
        questWindow.SetActive(true);
        questWindowText.text = questText;
        yield return new WaitForSeconds(8f);
        questWindow.SetActive(false);
    }

}
