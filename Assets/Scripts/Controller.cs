using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject[] balls;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public int numBall;

    //Canvas
    public GameObject uiEnd;

    void Start()
    {
        uiEnd.SetActive(false);

        //Converter pixels para game coordinate
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        print("Screen Size: bottomLeft: " + bottomLeft + " topRight: " + topRight);

        numBall = Random.Range(3, 9);

        for (int i = 0; i < numBall; i++)
        {
            Instantiate(balls[i]);
        }
    }

    public void DecreaseBall()
    {
        print("Ball out -1");
        numBall = numBall - 1;
        if (numBall <= 0)
        {
            print("Fim da Aplcação");
            uiEnd.SetActive(true);
        }
    }
}

