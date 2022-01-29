using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] GameObject basicBullet, circleShotBullets, snakingBullets, bulletShooter; // references to bullet object prefabs
    [SerializeField] GameObject playerReference; // reference to player object
    int bulletChoice = 0; // choices of ability for switch statement
    bool snakeActive; //for the snaking pattern bullet ability
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            switch (bulletChoice)
            {
                case 1:
                    Vector3 target = playerReference.transform.position - mousePosition;
                    float angle = Vector3.Angle(target, playerReference.transform.right);
                    if (mousePosition.y > playerReference.transform.position.y)
                        angle = angle * -1;

                    Instantiate(basicBullet, mousePosition, Quaternion.Euler(0, 0, angle));
                    break;
                case 2:
                    if (!snakeActive)
                        StartCoroutine(SnakingPattern());
                    break;
                case 3:
                    CircleShot(mousePosition);
                    break;
                case 4:
                    Instantiate(bulletShooter, mousePosition, bulletShooter.transform.rotation);
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
            bulletChoice = 1;
        else if (Input.GetKeyDown(KeyCode.Keypad2))
            bulletChoice = 2;
        else if (Input.GetKeyDown(KeyCode.Keypad3))
            bulletChoice = 3;
        else if (Input.GetKeyDown(KeyCode.Keypad4))
            bulletChoice = 4;
        else if (Input.GetKeyDown(KeyCode.Keypad5))
            bulletChoice = 5;
        
    }

    void CircleShot(Vector2 mousePos)
    {
        float z = 0; //represents the z rotation when spawning the bullets
        for (int i = 0; i < 8; i++)
        {
            Instantiate(basicBullet, mousePos, Quaternion.Euler(0, 0, z));
            z += 45f;
        }

    }

    IEnumerator SnakingPattern()
    {
        snakeActive = true;
        Vector2 spawnPos;
        Vector2 spacing = new Vector2(4, 0);
        for (int j = 0; j < 20; j++)
        {
            spawnPos = new Vector2(-30, 20);
            for (int i = 0; i < 16; i++)
            {
                Instantiate(basicBullet, spawnPos, Quaternion.Euler(0, 0, -90));
                spawnPos += spacing;
            }
            yield return new WaitForSeconds(0.4f);
        }
        snakeActive = false;
        yield break;
    }
}
