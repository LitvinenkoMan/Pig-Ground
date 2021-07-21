using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public Joystick Movmentjoystick;
    public SpriteRenderer SpriteRenderer;

    public Sprite pig_down;
    public Sprite pig_up;
    public Sprite pig_left;
    public Sprite pig_right;

    public RaycastHit2D hit2D;

    public float RunSpeed;

    float verticalMove = 0f;
    float horizontalMove = 0f;

    public Vector3 Direction;

    private void Awake()
    {
        RunSpeed = 2f;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Movmentjoystick.Vertical >= 0.2f)
        {
            Direction = new Vector3(0, 1, 0);
            verticalMove = RunSpeed;
            SpriteRenderer.sprite = pig_up;
        }
        else if (Movmentjoystick.Vertical <= -0.2f)
        {
            Direction = new Vector3(0, -1, 0);
            verticalMove = -RunSpeed;
            SpriteRenderer.sprite = pig_down;
        }
        else verticalMove = 0f;

        if (Movmentjoystick.Horizontal >= 0.2f)
        {
            Direction = new Vector3(1, 0, 0);
            horizontalMove = RunSpeed;
            SpriteRenderer.sprite = pig_right;
        }
        else if (Movmentjoystick.Horizontal <= -0.2f)
        {
            Direction = new Vector3(-1, 0, 0);
            horizontalMove = -RunSpeed;
            SpriteRenderer.sprite = pig_left;
        }
        else horizontalMove = 0f;
        hit2D = Physics2D.Linecast(gameObject.transform.position, gameObject.transform.position + Direction, 1 << LayerMask.NameToLayer("Default"));
        gameObject.transform.position += new Vector3(horizontalMove, verticalMove) * Time.deltaTime;
    }
}
