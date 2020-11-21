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
        _txtStone.text = currentStoneCount.ToString();
    }

    public void SetStoneCount(int newStoneCount)
    {
        currentStoneCount = newStoneCount;
    }
}
