using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicStoneUI : MonoBehaviour
{
    protected int currentStoneCount;
    private Text _txtStone;
    // Start is called before the first frame update
    void Awake()
    {
        _txtStone = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        print("current stone count..... " + currentStoneCount);
        _txtStone.text = currentStoneCount.ToString();
    }

    public void SetStoneCount(int newStoneCount)
    {
        print("stone count" + currentStoneCount);
        currentStoneCount = newStoneCount;

/*        if (newStoneCount >= 1) {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
        }*/
    }
}
