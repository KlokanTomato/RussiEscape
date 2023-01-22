using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

//    Animator anim;
    public Bullet bullet;
    int health = 150;
    bool left;
    float speedHorizontal = 0.18f * 80f;
    float speedVertical = 0.16f * 80f;
    public Text healthText = null;

    private SpriteRenderer rend;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite deadSprite;
    [SerializeField] Sprite woundedSprite;

    [SerializeField] LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        if (healthText != null)
            healthText.text = (health / 10).ToString();
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
        if (healthText != null)
            healthText.text = (health / 10).ToString();

        if (health <= 0)
        {
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
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right") || Input.GetKeyUp("up") || Input.GetKeyUp("down"))
        {
            //anim.SetBool("walk", false);
        }

        if (Input.GetKeyDown("a"))
        {
            if (left)
            {
                Bullet shoot = Instantiate(bullet, transform.position + (Vector3.left * 2f) + (Vector3.up * 0.5f), Quaternion.identity);
                shoot.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                shoot.SetDirection(-1);
            }
            else
            {
                Bullet shoot = Instantiate(bullet, transform.position + (Vector3.right * 2f) + (Vector3.up * 0.5f), Quaternion.identity);
                shoot.SetDirection(1);
            }
            //anim.SetTrigger("shoot");
        }
        if (left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "enemy_bullet")
        {
            collision.gameObject.SetActive(false);
            health -= 10;
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
        if (collision.gameObject.tag == "level_end")
        {
            levelManager.NextScene();
        }
    }
}
