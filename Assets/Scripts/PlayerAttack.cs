using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Bomb;

    PlayerMovment player;

    bool isPlaced = false;
    
    float timer = 3f;
    float time = 0f;
    private void Start()
    {
        player = GetComponent<PlayerMovment>();
    }

    private void Update()
    {
        

        if (isPlaced)
        {
            time += Time.deltaTime;
            if (time > timer)
            {
                time = 0;
                isPlaced = false;
            }
        }
    }

    public void DropBomb()
    {
        if (!isPlaced && !player.hit2D.collider)
        {
            isPlaced = true;
            GameObject newBomb = Instantiate(Bomb, gameObject.transform.position + player.Direction, Bomb.transform.rotation);
            newBomb.SetActive(true);
        }
    }
}
