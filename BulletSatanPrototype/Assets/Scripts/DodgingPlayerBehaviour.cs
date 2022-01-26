using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingPlayerBehaviour : MonoBehaviour
{
    public float speed = 5;
    public int lives;
    public Color objectColor;
    public bool iFrames;
    public bool canRoll;
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        Spawn();
        iFrames = false;
        canRoll = true;
        objectColor = gameObject.GetComponent<SpriteRenderer>().color;  
    }

    // Update is called once per frame
    void Update()
    {
        if (iFrames == true)
        {
            StartInvincibilityFrames();
            Invoke("EndInvincibilityFrames", .5f);
        }
        gameObject.GetComponent<SpriteRenderer>().color = objectColor;

        ManualPlayerMovement();




    }

    //Allows for actual input by a player
    void ManualPlayerMovement()
    {
        if (Input.GetKey("w"))
        {
            float yMove = 1;

            Vector3 NewPos = transform.position;

            NewPos.y += yMove * Time.deltaTime * speed;

            transform.position = NewPos;
        }

        if (Input.GetKey("s"))
        {
            float yMove = -1;

            Vector3 NewPos = transform.position;

            NewPos.y += yMove * Time.deltaTime * speed;

            transform.position = NewPos;
        }
        if (Input.GetKey("d"))
        {
            float xMove = 1;

            Vector3 NewPos = transform.position;

            NewPos.x += xMove * Time.deltaTime * speed;

            transform.position = NewPos;
        }

        if (Input.GetKey("a"))
        {
            float xMove = -1;

            Vector3 NewPos = transform.position;

            NewPos.x += xMove * Time.deltaTime * speed;

            transform.position = NewPos;
        }

        if(Input.GetKeyDown("space") && canRoll == true)
        {
            Roll();
        }    
    }    
    void WanderMovement()
    {

    }
    void Spawn()
    {
        transform.position = new Vector2(0, 0);
        lives--;
    }
    void StartInvincibilityFrames()
    {
        gameObject.tag = "iFrames";
        gameObject.layer = 6;
        objectColor.a = .5f;
    }
    void EndInvincibilityFrames()
    {
        iFrames = false;
        gameObject.tag = "Player";
        gameObject.layer = 0;
        objectColor.a = 1;
    }
    void Roll()
    {
        iFrames = true;
        canRoll = false;
        Invoke("RollReset", 2.0f);
    }
    void RollReset()
    {
        canRoll = true;
    }
}
