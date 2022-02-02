using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(7, 6);

        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

    
}
