using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuestor : MonoBehaviour
{
    public GameObject questCanvas;
    public Text questCanvasText;
    private bool collided = false;

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator PauseForAWhile() {
        yield return new WaitForSeconds(5.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("yown enter");
            collided = true;
            questCanvas.SetActive(collided);        
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("yown exit");
            collided = false;
            if (!collided) StartCoroutine(PauseForAWhile());
            questCanvas.SetActive(collided);        

        }
    }
}
