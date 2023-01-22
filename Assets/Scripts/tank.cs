using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : MonoBehaviour
{
    [SerializeField] public GameObject hrac;
    private SpriteRenderer rend;

    [SerializeField] Sprite woundedSprite1;
    [SerializeField] Sprite woundedSprite2;
    [SerializeField] Sprite woundedSprite3;
    [SerializeField] Sprite woundedSprite4;
    public Bullet bullet;
    bool rotate;
    bool left;
    bool destroyed = false;
    [SerializeField] int health = 50;
    float speedHorizontal = 0.18f * 30f * 0;
    float speedVertical = 0.16f * 30f * 0;
    float timeCount = 0;
    [SerializeField] float activeTime = 0;

    [SerializeField] LevelManager levelManager = null;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        left = true;
        float speedAdd = Random.Range(6f, 6f);
        speedHorizontal = speedHorizontal + speedAdd;
        speedVertical = speedVertical + speedAdd;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed == true)
        {
            transform.position += Vector3.left * 30f * Time.deltaTime;
            return;
        }
        else
        {
            if (hrac.transform.position.y + 1 < transform.position.y)
            {
                transform.position += Vector3.down * speedVertical * Time.deltaTime;
            }
            else if (hrac.transform.position.y - 1 > transform.position.y)
            {
                transform.position += Vector3.up * speedVertical * Time.deltaTime;
            }

            if (timeCount > 3 && Vector3.Distance(gameObject.transform.position, hrac.transform.position) < 50f)
            {
                if (hrac.transform.position.x < transform.position.x)
                {
                    Bullet shoot = Instantiate(bullet, transform.position + (Vector3.left * 10f) + Vector3.down, Quaternion.identity);
                    shoot.SetDirection(-1);
                    timeCount = 0;
                }
                else
                {
                    Bullet shoot = Instantiate(bullet, transform.position + (Vector3.right * 10f) + Vector3.down, Quaternion.identity);
                    shoot.SetDirection(1);
                    timeCount = 0;
                }
            }


            if (hrac.transform.position.x + 10 < transform.position.x)
            {
                if (!left)
                {
                    left = true;
                    rotate = true;
                }
                transform.position += Vector3.left * speedHorizontal * Time.deltaTime;
            }
            else if (hrac.transform.position.x - 10 > transform.position.x)
            {
                if (left)
                {
                    left = false;
                    rotate = true;
                }
                transform.position += Vector3.right * speedHorizontal * Time.deltaTime;
            }

            if (hrac.transform.position.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }

        if (rotate)
        {
            rotate = false;
        }
        timeCount = timeCount + Time.deltaTime;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            health -= 40;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            health -= 10;
        }
        if (health <= 0)
        {
            destroyed = true;
            levelManager.NextScene();
            rend.sprite = woundedSprite4;
        }
        if (health <= 750 && health > 500)
            rend.sprite = woundedSprite1;
        if (health <= 500 && health > 250)
            rend.sprite = woundedSprite2;
        if (health <= 250 && health > 0)
            rend.sprite = woundedSprite3;
    }
}
