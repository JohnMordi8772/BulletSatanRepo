using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSpending : MonoBehaviour
{
    float funds, maxFunds;
    [SerializeField] Text fundText;
    bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        maxFunds = 50;
        funds = 25;
        pressed = false;
        UpdateFundText();
        StartCoroutine(EarningFunds());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void UpdateFundText()
    {
        fundText.text = "Money: " + funds + "/" + maxFunds;
    }

    public float GetFunds()
    {
        return funds;
    }

    public void SpendFunds(float cost)
    {
        funds -= cost;
        UpdateFundText();
    }

    IEnumerator EarningFunds()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (!BulletPlayer.shiftDown)
            {
                if (funds < maxFunds - 1)
                    funds += 2;
                else if (funds != maxFunds)
                    funds++;
                UpdateFundText();
            }
        }
    }
}
