using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    //Prefab Bolas
    public GameObject[] balls;
    //Tamanho da Tela 
    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    //Quantidade de Bolas na Tela
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
    //Contagem da quantidade de Bolas na Tela
    public void DecreaseBall()
    {
        numBall = numBall - 1;
        if (numBall <= 0)
        {
            print("Fim da Aplcação");
            uiEnd.SetActive(true);
        }
    }
    //Botão para Reiniciar
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

