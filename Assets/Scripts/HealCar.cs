using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCar : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Sprite destroyedSprite;
    private SpriteRenderer rend;
    [SerializeField] public GameObject hrac;
    [SerializeField] bool putBehind = false;
    public Bullet bullet;
    float timeCount = 0;
    bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            transform.position += Vector3.left * speed * 2 * Time.deltaTime;
            return;
        }
        else
            transform.position += Vector3.left * speed * Time.deltaTime;

        if (timeCount > 1 && Vector3.Distance(gameObject.transform.position, hrac.transform.position) < 50f)
        {
            if (hrac.transform.position.x < transform.position.x || putBehind)
            {
                Bullet shoot = Instantiate(bullet, transform.position + (Vector3.left * 6.5f), Quaternion.identity);
                shoot.SetDirection(-1);
                timeCount = 0;
            }
            else
            {
                Bullet shoot = Instantiate(bullet, transform.position + (Vector3.right * 6.5f), Quaternion.identity);
                shoot.SetDirection(1);
                timeCount = 0;
            }
        }
        timeCount = timeCount + Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            rend.sprite = destroyedSprite;
            destroyed = true;
            Destroy(collision.gameObject);
        }
    }
}
