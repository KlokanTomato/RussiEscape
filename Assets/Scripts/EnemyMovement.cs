using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public GameObject hrac;
    private SpriteRenderer rend;

    [SerializeField] Sprite woundedSprite;
    [SerializeField] Sprite destroyedSprite;
    public Bullet bullet;
    bool rotate;
    bool left;
    [SerializeField] bool is_car = true;
    bool destroyed = false;
    [SerializeField] int health = 50;
    float speedHorizontal = 0.18f * 30f * 0;
    float speedVertical = 0.16f * 30f * 0;
    float timeCount = 0;
    static int kill_count = 0;
    [SerializeField] float activeTime = 0;

    public Text text;
    [SerializeField] LevelManager levelManager = null;
    // Start is called before the first frame update
    void Start()
    {
        kill_count = 0;
        rend = gameObject.GetComponent<SpriteRenderer>();
        left = true;
        float speedAdd = Random.Range(5f, 8f);
        speedHorizontal = speedHorizontal + speedAdd;
        speedVertical = speedVertical + speedAdd;
    }

    // Update is called once per frame
    void Update()
    {
        if (kill_count == 20 && levelManager != null)
            levelManager.NextScene();
        if (destroyed == true)
        {
            if(is_car)
                transform.position += Vector3.left * 30f * Time.deltaTime;
            return;
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, hrac.transform.position) > 60f && is_car != true)
                return;
            if (hrac.transform.position.y + 1 < transform.position.y)
            {
                transform.position += Vector3.down * speedVertical * Time.deltaTime;
            }
            else if (hrac.transform.position.y - 1 > transform.position.y)
            {
                transform.position += Vector3.up * speedVertical * Time.deltaTime;
            }

            if (timeCount > (is_car ? 1.5 : 0.5) && Vector3.Distance(gameObject.transform.position, hrac.transform.position) < 50f)
            {
                float offest = is_car ? 6f : 2f;
                if (hrac.transform.position.x < transform.position.x)
                {
                    Bullet shoot = Instantiate(bullet, transform.position + (Vector3.left * offest), Quaternion.identity);
                    shoot.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                    shoot.SetDirection(-1);
                    timeCount = 0;
                }
                else
                {
                    Bullet shoot = Instantiate(bullet, transform.position + (Vector3.right * offest), Quaternion.identity);
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
        }

        if (rotate)
        {
            rotate = false;
        }
        timeCount = timeCount + Time.deltaTime;

        if (text != null)
            text.text = kill_count.ToString() + "/20";
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bomb" || collision.gameObject.tag == "tank_bullet")
        {
            health -= 25;
            rend.sprite = woundedSprite;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            health -= 10;
            if (health == 30)
                rend.sprite = woundedSprite;
        }
        if (destroyed)
            return;
        if (health <= 0)
        {
            kill_count++;
            Debug.Log(kill_count);
            destroyed = true;
            rend.sprite = destroyedSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "destroyed" && destroyed && levelManager == null)
        {
            gameObject.SetActive(false);
        }
    }
}
