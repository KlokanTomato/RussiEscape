using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGeneratorScript : MonoBehaviour
{
    public GameObject stribe;
    float timer = 0;
    float random = 0;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0.5f, 6f); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > random)
        {
            Instantiate(stribe, transform.position, Quaternion.identity);
            timer = 0;
            random = Random.Range(0.5f, 6f);
        }
    }
}
