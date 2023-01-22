using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrderScript : MonoBehaviour
{
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.sortingOrder = (int)-gameObject.transform.position.y;
    }
}
