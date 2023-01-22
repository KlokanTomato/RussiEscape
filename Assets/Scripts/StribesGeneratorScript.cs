using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StribesGeneratorScript : MonoBehaviour
{
    public GameObject stribe;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.6f)
        {
            Instantiate(stribe, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
