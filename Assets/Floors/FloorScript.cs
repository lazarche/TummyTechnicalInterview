using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public bool destroy = false;
    public float doAfter = -1;
    private float timeFor = 0;
    // Start is called before the first frame update
    void Start()
    {
        Quaternion d;
        float temp = Random.Range(0, 4);
        if (temp < 1)
            d = Quaternion.Euler(-90, 0, 0);
        else if(temp < 2)
            d = Quaternion.Euler(-90, 90, 0);
        else if (temp < 3)
            d = Quaternion.Euler(-90, 180, 0);
        else
            d = Quaternion.Euler(-90, 270, 0);

        transform.rotation = d;
    }

    public void DestroyAfter(float time)
    {
        destroy = true;
        doAfter = Time.time + time;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            if (Time.time > doAfter)
            {
                doAfter = 1000;
                PlayAnimationAndDestroy();
            }
        }
    }

    void PlayAnimationAndDestroy()
    {
        gameObject.GetComponent<Animator>().Play("FloorDestroy");
    }
}
