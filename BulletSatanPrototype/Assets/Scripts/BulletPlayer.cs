using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] GameObject basicBullet, circleShotBullets, snakingBullet, bulletShooter; // references to bullet object prefabs
    [SerializeField] GameObject playerReference; // reference to player object
    [SerializeField] BulletSpending bulletSpending;
    int bulletChoice = 0; // choices of ability for switch statement
    bool snakeActive; //for the snaking pattern bullet ability
    public static bool shiftDown = false;
    List<string> methods;
    List<Vector3> mousePositions;

    public GameObject StandardSquare;
    public GameObject SnakeSquare;
    public GameObject BurstSquare;
    public GameObject SpinSquare;
    public GameObject SplitSquare;
    public Color nonchosenColor;
    public Color chosenColor;
    // Start is called before the first frame update
    void Start()
    {
        nonchosenColor = StandardSquare.GetComponent<SpriteRenderer>().color;
        chosenColor = new Color(1.0f, .8f, .4f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (Vector2.Distance(mousePosition, playerReference.transform.position) < 5) // prevents placement of abilities being unfairly close to player
                return; // prevents further traversal of Update()
            switch (bulletChoice)
            {
                case 1: // single fire bullet aimed directly at player
                    if (bulletSpending.GetFunds() < 1f) // POSSIBLY ADD A DELAY SO THAT PLAYER CAN HOLD RATHER THAN SPAMMING
                        break;
                    bulletSpending.SpendFunds(1f);
                    StartCoroutine(BasicShot(mousePosition));
                    //if(shiftDown)
                    //{
                    //    methods.Add("BasicShot");
                    //}

                    //if (mousePosition.y > playerReference.transform.position.y)
                        //angle = angle * -1;

                    //Instantiate(basicBullet, mousePosition, Quaternion.Euler(0, 0, angle));
                    break;
                case 2: // snaking lines of bullets from the top of the screen all the way to the bottom
                    if (bulletSpending.GetFunds() < 15f)
                        break;
                    if (!snakeActive)
                    {
                        snakeActive = true;
                        bulletSpending.SpendFunds(15f);
                        StartCoroutine(SnakingPattern());
                    }
                    break;
                case 3: // shoots a number of bullets in a circle at mouse click
                    if (bulletSpending.GetFunds() < 5f)
                        break;
                    bulletSpending.SpendFunds(5f);
                    StartCoroutine(CircleShot(mousePosition));
                    break;
                case 4: // automatic bulllet shooter that spins and shoots for ten seconds
                    if (bulletSpending.GetFunds() < 10f)
                        break;
                    bulletSpending.SpendFunds(10f);
                    StartCoroutine(BulletShooter(mousePosition));
                    //Instantiate(bulletShooter, mousePosition, bulletShooter.transform.rotation);
                    break;
                case 5: // shoots two bullets at a 30 degree offset from the player
                    if (bulletSpending.GetFunds() < 3f)
                        break;
                    bulletSpending.SpendFunds(3f);
                    StartCoroutine(SplitShot(mousePosition));
                    //target = playerReference.transform.position - mousePosition;
                    //angle = Vector3.Angle(target, playerReference.transform.right);
                    //if (mousePosition.y > playerReference.transform.position.y)
                      //  angle = angle * -1;

                    //Instantiate(basicBullet, mousePosition, Quaternion.Euler(0, 0, angle + 30));
                    //Instantiate(basicBullet, mousePosition, Quaternion.Euler(0, 0, angle - 30));
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletChoice = 1;
            StandardSquare.GetComponent<SpriteRenderer>().color = chosenColor;
            SnakeSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            BurstSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SpinSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SplitSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
        }

        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        { 
            bulletChoice = 2;
            StandardSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SnakeSquare.GetComponent<SpriteRenderer>().color = chosenColor;
            BurstSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SpinSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SplitSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            bulletChoice = 3;
            StandardSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SnakeSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            BurstSquare.GetComponent<SpriteRenderer>().color = chosenColor;
            SpinSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SplitSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        { 
            bulletChoice = 4;
            StandardSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SnakeSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            BurstSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SpinSquare.GetComponent<SpriteRenderer>().color = chosenColor;
            SplitSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        { 
            bulletChoice = 5;
            StandardSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SnakeSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            BurstSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SpinSquare.GetComponent<SpriteRenderer>().color = nonchosenColor;
            SplitSquare.GetComponent<SpriteRenderer>().color = chosenColor;
        }
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            shiftDown = true;
        }
        else
        {
            shiftDown = false;
        }
        
    }

    IEnumerator BasicShot(Vector3 mousePosition)
    {
        while (shiftDown)
            yield return new WaitForEndOfFrame();
        Vector3 target = playerReference.transform.position - mousePosition;
        float angle = Vector3.Angle(target, playerReference.transform.right);
        if (mousePosition.y > playerReference.transform.position.y)
            angle = angle * -1;

        Instantiate(basicBullet, mousePosition, Quaternion.Euler(0, 0, angle));
    }

    IEnumerator SplitShot(Vector3 mousePosition)
    {
        while (shiftDown)
            yield return new WaitForEndOfFrame();
        Vector3 target = playerReference.transform.position - mousePosition;
        float angle = Vector3.Angle(target, playerReference.transform.right);
        if (mousePosition.y > playerReference.transform.position.y)
            angle = angle * -1;

        Instantiate(basicBullet, mousePosition, Quaternion.Euler(0, 0, angle + 30));
        Instantiate(basicBullet, mousePosition, Quaternion.Euler(0, 0, angle - 30));
    }

    IEnumerator BulletShooter(Vector3 mousePosition)
    {
        while (shiftDown)
            yield return new WaitForEndOfFrame();
        Instantiate(bulletShooter, mousePosition, bulletShooter.transform.rotation);
        yield break;
    }

    IEnumerator CircleShot(Vector3 mousePos)
    {
        while (shiftDown)
            yield return new WaitForEndOfFrame();
        float z = 0; //represents the z rotation when spawning the bullets
        for (int i = 0; i < 8; i++)
        {
            Instantiate(basicBullet, mousePos, Quaternion.Euler(0, 0, z));
            z += 45f;
        }
        yield break;
    }

    IEnumerator SnakingPattern()
    {
        while (shiftDown)
            yield return new WaitForEndOfFrame();
        Vector2 spawnPos;
        Vector2 spacing = new Vector2(5, 0);
        for (int j = 0; j < 20; j++)
        {
            spawnPos = new Vector2(-30, 20);
            for (int i = 0; i < 13; i++)
            {
                Instantiate(snakingBullet, spawnPos, Quaternion.Euler(0, 0, -90));
                spawnPos += spacing;
            }
            yield return new WaitForSeconds(0.5f);
        }
        snakeActive = false;
        yield break;
    }
}
