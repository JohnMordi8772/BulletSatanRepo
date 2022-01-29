using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] GameObject basicBullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutomatedShooting());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,-3f, Space.Self);
    }

    IEnumerator AutomatedShooting()
    {
        for(int i = 0; i < 40; i++)
        {
            Instantiate(basicBullet, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
            Instantiate(basicBullet, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180));
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(gameObject);
    }
}
