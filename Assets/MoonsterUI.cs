using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonsterUI : MonoBehaviour
{
    protected int currentMoonstersCount;
    private Text _txtMoonsters;
    // Start is called before the first frame update
    void Awake()
    {
        _txtMoonsters = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _txtMoonsters.text = "Moonsters Killed: " + currentMoonstersCount.ToString();
    }

    public void SetMoonsterCount(int newMoonstersCount)
    {
        print("setmoonstercount " + newMoonstersCount);
        currentMoonstersCount = newMoonstersCount;
    }
}
