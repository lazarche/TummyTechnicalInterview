using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour
{
    public float spd;
    // Start is called before the first frame update
    void Start()
    {
        spd = -Random.RandomRange(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(spd * Time.deltaTime, 0, 0);
        if (transform.position.x < -100)
            transform.Translate(200, 0, 0);
    }
}
