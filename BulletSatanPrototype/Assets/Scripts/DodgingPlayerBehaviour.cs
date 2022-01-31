using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DodgingPlayerBehaviour : MonoBehaviour
{
    public float speed = 5;
    public int lives;
    public Color objectColor;
    public bool iFrames;
    public bool canRoll;
    public Color hitColor;
    public Text LivesText;
    public bool isHit;
    public GameObject loseText;
    // Start is called before the first frame update
    void Start()
    {

        lives = 3;
        iFrames = false;
        canRoll = true;
        objectColor = gameObject.GetComponent<SpriteRenderer>().color;
        hitColor = new Color(1, 0, 0, .5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iFrames == true)
        {
            
            StartInvincibilityFrames();
            Invoke("EndInvincibilityFrames", 1.0f);
        }
        //gameObject.GetComponent<SpriteRenderer>().color = objectColor;

        ManualPlayerMovement();


        LivesText.text = "Lives: " + lives;

        if(lives == 0)
        {
            loseText.SetActive(true);
            Time.timeScale = 0.2f;
            Invoke("Restart", .6f);
            
        }

    }

    //Allows for actual input by a player
    void ManualPlayerMovement()
    {
        if (Input.GetKey("w"))
        {
            float yMove = 1;

            Vector3 NewPos = transform.position;

            NewPos.y += yMove * Time.deltaTime * speed;

            NewPos.y = Mathf.Clamp(NewPos.y, -9.2f, 9.2f);

            transform.position = NewPos;
        }

        if (Input.GetKey("s"))
        {
            float yMove = -1;

            Vector3 NewPos = transform.position;

            NewPos.y += yMove * Time.deltaTime * speed;

            NewPos.y = Mathf.Clamp(NewPos.y, -9.2f, 9.2f);

            transform.position = NewPos;
        }
        if (Input.GetKey("d"))
        {
            float xMove = 1;

            Vector3 NewPos = transform.position;

            NewPos.x += xMove * Time.deltaTime * speed;

            NewPos.x = Mathf.Clamp(NewPos.x, -9.2f, 9.2f);

            transform.position = NewPos;
        }

        if (Input.GetKey("a"))
        {
            float xMove = -1;

            Vector3 NewPos = transform.position;

            NewPos.x += xMove * Time.deltaTime * speed;

            NewPos.x = Mathf.Clamp(NewPos.x, -9.2f, 9.2f);

            transform.position = NewPos;
        }

        if(Input.GetKeyDown("space") && canRoll == true)
        {
            Roll();
        }    
    }    

    void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    

    //changes layer to the bullets' non-interactable layer
    void StartInvincibilityFrames()
    {
        gameObject.tag = "iFrames";
        gameObject.layer = 6;
        if(isHit == true)
        {
            GetComponent<SpriteRenderer>().color = hitColor;
        }
        else
        {
            objectColor.a = .5f;
        
            gameObject.GetComponent<SpriteRenderer>().color = objectColor;
        }
        
    }
    //changes layer to one that can interact with the bullets
    void EndInvincibilityFrames()
    {
        iFrames = false;
        gameObject.tag = "Player";
        gameObject.layer = 0;
        objectColor.a = 1;
        isHit = false;
        gameObject.GetComponent<SpriteRenderer>().color = objectColor;
    }
    //sets bools to true so the player gets some invincibility frames
    void Roll()
    {
        iFrames = true;
        canRoll = false;
        Invoke("RollReset", 3.0f);
    }
    //cooldown for roll
    void RollReset()
    {
        canRoll = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            isHit = true;
            Invoke("EndInvincibilityFrames", 1);
            StartInvincibilityFrames();
            lives--;
        }
        
    }

}
