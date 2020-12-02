using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    int index = 0;
    public Text dialogueName;
    public Text dialogueText;
    private Dialogue[] dialogues;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogues = FindObjectOfType<DialogueTrigger>().dialogue;
        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        dialogueName.text = dialogue.name;

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Return)

) {
            DisplayNextSentence();
        }

    }

    public void DisplayNextSentence() { 
        if (sentences.Count == 0)
        {
            if (index < dialogues.Length - 1)
            {
                index += 1;
                StartDialogue(dialogues[index]);
            }
            else {
                EndDialogue();
            }
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.0366666666666667f);
        }
    }
    public void EndDialogue() {
        print("End dialogue");
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
