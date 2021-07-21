using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    public Sprite Dog_down;
    public Sprite Dog_up;
    public Sprite Dog_left;
    public Sprite Dog_right;

    public Sprite Dog_angry_down;
    public Sprite Dog_angry_up;
    public Sprite Dog_angry_left;
    public Sprite Dog_angry_right;

    public Sprite Dog_dirty_down;
    public Sprite Dog_dirty_up;
    public Sprite Dog_dirty_left;
    public Sprite Dog_dirty_right;

    bool canMove = true;
    bool isDirty = false;

    float horizontalMove = 1f;
    float verticalMove = 0f;
    float runSpeed = 1.5f;
    float damage = 1;
    float timer = 5f;

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
            if (horizontalMove < 0)
            {
                spriteRenderer.sprite = Dog_left;
            }
            else if (horizontalMove > 0)
            {
                spriteRenderer.sprite = Dog_right;
            }
        }

        if (canMove)
        {
            gameObject.transform.position += new Vector3(horizontalMove * runSpeed, verticalMove) * Time.deltaTime;
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
                horizontalMove *= -1;
            }
            else if (collision.gameObject.GetComponent<ActorHP>())
            {
                audioSource.PlayOneShot(audioSource.clip);
                horizontalMove = 0;
                collision.gameObject.GetComponent<ActorHP>().SetHP(-damage);
                if (spriteRenderer.sprite == Dog_right)
                    spriteRenderer.sprite = Dog_angry_right;
                else if (spriteRenderer.sprite == Dog_left)
                    spriteRenderer.sprite = Dog_angry_left;
                else if (spriteRenderer.sprite == Dog_up)
                    spriteRenderer.sprite = Dog_angry_up;
                else if (spriteRenderer.sprite == Dog_down)
                    spriteRenderer.sprite = Dog_angry_down;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isDirty)
        {
            if (collision.gameObject.GetComponent<ActorHP>())
                while (horizontalMove == 0)
                    horizontalMove = Random.Range(-1, 1);
        }
    }

    public void GetDirty()
    {
        canMove = false;
        isDirty = true;
        if (spriteRenderer.sprite == Dog_right)
            spriteRenderer.sprite = Dog_dirty_right;
        else if (spriteRenderer.sprite == Dog_left)
            spriteRenderer.sprite = Dog_dirty_left;
        else if (spriteRenderer.sprite == Dog_up)
            spriteRenderer.sprite = Dog_angry_up;
        else if (spriteRenderer.sprite == Dog_down)
            spriteRenderer.sprite = Dog_dirty_down;
    }
}
