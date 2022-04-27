using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawning : MonoBehaviour
{
    public int minX = -70;
    public int maxX = 70;

    public int minY = 30;
    public int maxY = 200;
    
    public int minZ = 1000;
    public int maxZ = 3000;

    public GameObject[] cloudsList;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Random.Range(20, 30); i++)
        {
            Instantiate(cloudsList[Random.Range(0, cloudsList.Length)],
                new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ)),
                Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
