using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    public Sprite Farmer_down;
    public Sprite Farmer_up;
    public Sprite Farmer_left;
    public Sprite Farmer_right;

    public Sprite Farmer_angry_down;
    public Sprite Farmer_angry_up;
    public Sprite Farmer_angry_left;
    public Sprite Farmer_angry_right;

    public Sprite Farmer_dirty_down;
    public Sprite Farmer_dirty_up;
    public Sprite Farmer_dirty_left;
    public Sprite Farmer_dirty_right;

    bool canMove = true;
    bool isDirty = false;

    float horizontalMove = 0.14f;
    float verticalMove = 1f;
    float moveDirection = 0;
    float runSpeed = 1.5f;
    float damage = 1;
    float timer = 5f;
    float timer2 = 2f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (verticalMove < 0)
            {
                spriteRenderer.sprite = Farmer_down;
            }
            else if (verticalMove > 0)
            {
                spriteRenderer.sprite = Farmer_up;
            }
            else if (horizontalMove > 0)
            {
                spriteRenderer.sprite = Farmer_right;
            }
            else if (horizontalMove < 0)
            {
                spriteRenderer.sprite = Farmer_left;
            }
        }

        if (canMove)
        {
            gameObject.transform.position += new Vector3(horizontalMove * runSpeed, verticalMove * runSpeed) * Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 5f;
                canMove = true;
                isDirty = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDirty)
        {
            if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Stone" || collision.gameObject.tag == "Bush" || collision.gameObject.tag == "Enemy")
            {
                horizontalMove = 0;
                verticalMove = 0;
                moveDirection = Random.Range(0, 2);
                switch (moveDirection)
                {
                    case 0:
                        {
                            while (horizontalMove == 0)
                                horizontalMove = Random.Range(-1, 2);
                            break;
                        }
                    case 1:
                        {
                            while (verticalMove == 0)
                                verticalMove = Random.Range(-1, 2);

                            if (verticalMove < 0)
                                horizontalMove = -0.14f;
                            else if (verticalMove > 0)
                                horizontalMove = 0.14f;

                            break;
                        }
                    default:
                        break;
                }
            }
            else if (collision.gameObject.GetComponent<ActorHP>())
            {
                audioSource.PlayOneShot(audioSource.clip);
                verticalMove = 0;
                horizontalMove = 0;
                collision.gameObject.GetComponent<ActorHP>().SetHP(-damage);
                if (spriteRenderer.sprite == Farmer_right)
                    spriteRenderer.sprite = Farmer_angry_right;
                else if (spriteRenderer.sprite == Farmer_left)
                    spriteRenderer.sprite = Farmer_angry_left;
                else if (spriteRenderer.sprite == Farmer_up)
                    spriteRenderer.sprite = Farmer_angry_up;
                else if (spriteRenderer.sprite == Farmer_down)
                    spriteRenderer.sprite = Farmer_angry_down;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        timer2 -= Time.deltaTime;
        if (timer2 < 0)
        {
            timer2 = 2f;
            OnCollisionEnter2D(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isDirty)
        {
            if (collision.gameObject.GetComponent<ActorHP>())
            {
                while (verticalMove == 0)
                    verticalMove = Random.Range(-1, 1);
                if (verticalMove < 0)
                    horizontalMove = -0.14f;
                else
                    horizontalMove = 0.14f;
            }
        }
    }

    public void GetDirty()
    {
        canMove = false;
        isDirty = true;
        if (spriteRenderer.sprite == Farmer_right)
            spriteRenderer.sprite = Farmer_dirty_right;
        else if (spriteRenderer.sprite == Farmer_left)
            spriteRenderer.sprite = Farmer_dirty_left;
        else if (spriteRenderer.sprite == Farmer_up)
            spriteRenderer.sprite = Farmer_angry_up;
        else if (spriteRenderer.sprite == Farmer_down)
            spriteRenderer.sprite = Farmer_dirty_down;
    }
}
