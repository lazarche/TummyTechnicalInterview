using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour
{
    public int minV = 5, maxV = 20;
    public Vector3 spawnPoint;
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        int height = Random.Range(minV, maxV);
        // new Vector3(-0.045f, 0.986f * (floorFinished + floorParts), 0.353f)
        for(int i = 1; i < height; i++)
        {
            var yy = 1 + 0.986f * i;
            Instantiate(floor, new Vector3(spawnPoint.x, yy, spawnPoint.z), Quaternion.Euler(-90, 180, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
