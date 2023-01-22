using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCar : MonoBehaviour
{
    public Bullet bullet;
    public GameObject bomb;
    private SpriteRenderer rend;
    [SerializeField] Sprite woundedSprite;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite deadSprite;
    int health = 150;
    bool left = false;
    float speedHorizontal = 0.18f * 100f;
    float speedVertical = 0.16f * 100f;
    int bomb_count = 5;
    public Text healthText;
    public Text bombText;

    [SerializeField] LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        healthText.text = (health / 10).ToString();
        bombText.text = bomb_count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            levelManager.RestartScene();
        }
        if (health <= 0)
        {
            rend.sprite = deadSprite;
            return;
        }
        if (Input.GetKey("up"))
        {
            transform.position += Vector3.up * speedVertical * Time.deltaTime;
            //            anim.SetBool("walk", true);
        }
        if (Input.GetKey("down"))
        {
            transform.position += Vector3.down * speedVertical * Time.deltaTime;
            //          anim.SetBool("walk", true);
        }
        if (Input.GetKey("left"))
        {
            transform.position += Vector3.left * speedHorizontal * Time.deltaTime;
            left = true;
            //anim.SetBool("walk", true);
        }
        if (Input.GetKey("right"))
        {
            transform.position += Vector3.right * speedHorizontal * Time.deltaTime;
            left = false;
            //anim.SetBool("walk", true);
        }
        if (Input.GetKey(KeyCode.R))
        {
            levelManager.RestartScene();
        }
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right") || Input.GetKeyUp("up") || Input.GetKeyUp("down"))
        {
            //anim.SetBool("walk", false);
        }


        if (Input.GetKeyDown("a"))
        {
            Bullet shoot = Instantiate(bullet, transform.position + (Vector3.right * 6f) + Vector3.down, Quaternion.identity);
            shoot.SetDirection(1);
        }
        if (Input.GetKeyDown(KeyCode.Space) && bomb_count > 0)
        {
            Instantiate(bomb, transform.position + (Vector3.left * 6f) + Vector3.down, Quaternion.identity);
            bomb_count--;
        }

        healthText.text = (health / 10).ToString();
        bombText.text = bomb_count.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "bomb_collect")
        {
            collision.gameObject.SetActive(false);
            bomb_count++;
        }
        if (collision.gameObject.tag == "enemy_bullet")
        {
            collision.gameObject.SetActive(false);
            health -= 10;
            if(health <= 50) 
            {
                rend.sprite = woundedSprite;
            }
        }
        if (collision.gameObject.tag == "tank_bullet")
        {
            collision.gameObject.SetActive(false);
            health -= 30;
            if (health <= 50)
            {
                rend.sprite = woundedSprite;
            }
        }
        if (collision.gameObject.tag == "heal_bullet")
        {
            collision.gameObject.SetActive(false);
            health += 10;
            if (health > 40)
                rend.sprite = normalSprite;
        }
    }

}
