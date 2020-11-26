using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfinityStones : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public GameObject infinityStones;
    public CharacterController characterCharacter;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnTheInfinityStones();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnTheInfinityStones() {
        Vector3 pos = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2),
            3,
            Random.Range(-size.z / 2, size.z / 2)
        );

        Instantiate(infinityStones, pos, Quaternion.identity);
    }
}
