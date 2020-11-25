using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStonePickup : MonoBehaviour
{

    public float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MagicStone stone = other.GetComponent<MagicStone>();

            if (stone != null)
            {
                print("magic stone destroyed");
                stone.ModifyStoneCount();
                Destroy(gameObject);
            }
        }
    }
}
