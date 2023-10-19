using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Perception perception;
    public int numOfZombies = 10;
    GameObject[] zombies;

    // Start is called before the first frame update
    void Start()
    {
        zombies = new GameObject[numOfZombies];

        for (int i = 0; i < numOfZombies; ++i)
        {

            Vector3 pos = this.transform.position + Random.insideUnitSphere * 10;
            Vector3 randomize = Random.insideUnitSphere;
            pos.y = 0;

            zombies[i] = (GameObject)Instantiate(perception.zombiePrefab, pos,
                              Quaternion.LookRotation(randomize));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
