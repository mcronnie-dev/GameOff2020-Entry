using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStone : MonoBehaviour
{

    public int StoneCount;
    public GameObject[] stoneAware;

    public void SetStoneCount(int newStoneCount)
    {
        foreach (GameObject go in stoneAware)
        {
            print("stoneaware ");
            go.SendMessage(nameof(SetStoneCount), newStoneCount, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void ModifyStoneCount()
    {
        StoneCount += 1;
        print("Modify stone count now " + StoneCount);

        //if (StoneCount > 5) Win();
        //else
        SetStoneCount(StoneCount);
    }

    public void Win()
    {

    }

}
