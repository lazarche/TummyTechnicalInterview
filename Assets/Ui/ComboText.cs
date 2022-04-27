using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboText : MonoBehaviour
{
    public float DestroyTime = 0.5f;
    public Vector3 offSet, randomP, randomS;
    private Quaternion randomR;
    // Start is called before the first frame update
    void Start()
    {
        float sign = Mathf.Sign(Random.RandomRange(-1, 1));
        offSet = new Vector3(1.5f * sign, 1, -1);
        randomP = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        randomS = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
        randomR = new Quaternion(0f, 0f, Random.Range(0f, 0.2f), 1f); 
        Destroy(gameObject, DestroyTime);

        transform.localPosition += offSet + randomP;
        transform.localScale += randomS;
        transform.rotation = randomR;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
