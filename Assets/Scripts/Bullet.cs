using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 0.3f;
    public int damage;
    int direction = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime * 80 * direction;
    }

    public void SetDirection(int direction) 
    {
        this.direction = direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "destroyed")
        {
            gameObject.SetActive(false);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
