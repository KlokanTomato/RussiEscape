using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private SpriteRenderer rend;
    [SerializeField] Sprite blue;
    [SerializeField] Sprite red;
    [SerializeField] Sprite green;
    [SerializeField] Sprite gray;

    [SerializeField] Sprite blue_destroyed;
    [SerializeField] Sprite red_destroyed;
    [SerializeField] Sprite green_destroyed;
    [SerializeField] Sprite gray_destroyed;

    bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        int num = Random.Range(1, 5);
        if (num == 1)
            rend.sprite = blue;
        else if (num == 2)
            rend.sprite = red;
        else if (num == 3)
            rend.sprite = green;
        else
            rend.sprite = gray;
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyed)
            transform.position += Vector3.left * speed * 2 * Time.deltaTime;
        else
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            if (rend.sprite == blue)
                rend.sprite = blue_destroyed;
            else if (rend.sprite == red)
                rend.sprite = red_destroyed;
            else if (rend.sprite == green)
                rend.sprite = green_destroyed;
            else
                rend.sprite = gray_destroyed;
            destroyed = true;
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "destroyed")
        {
            gameObject.SetActive(false);
        }
    }
}
