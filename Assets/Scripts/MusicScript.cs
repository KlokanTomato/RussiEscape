using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Music") != null)
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
