using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakingBullet : BulletMovement
{
    float x;
    float horiSpeed;
    bool rightOrLeft; // true is right and false is left
    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        horiSpeed = 3f;
        x = transform.position.x;
        rightOrLeft = true;
    }

    protected override void Update()
    {
        base.Update();
        if (rightOrLeft)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.x > x + 3)
            rightOrLeft = false;
        else if (transform.position.x < x - 3)
            rightOrLeft = true;
    }
}
