using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_FollowObject : MonoBehaviour
{
    public GameObject barLife;//Barra Prefab
    public Image bar;
    public Image barfilled;
    public TextMeshProUGUI txtHP;
    public bool barON;

    void Start()
    {
        //Instanciando a Barra de HP
        bar = Instantiate(barLife, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        barfilled = new List <Image>(bar.GetComponentsInChildren<Image>()).
                    Find(img => img != bar);
        txtHP = new List<TextMeshProUGUI>(bar.GetComponentsInChildren<TextMeshProUGUI>()).
                    Find(Text => Text != bar);
        barON = true;
    }
    private void Update()
    {
        //Atualizando posição da Barra de HP
        if (barON == true)
        {
            bar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
        
    }
    public void BarLife(float lifeStatus)
    {
        //Atualizando Status da Barra de HP
        barfilled.fillAmount = lifeStatus;
        txtHP.text = "HP: " + lifeStatus.ToString();
        if (lifeStatus <= 0)
        {
            DestroyUIbar();
        }
    }
    //Quando o Bola for Destruida, em um segundo a Bara Some
    public void DestroyUIbar()
    {
        StartCoroutine(SendDestroy());
    }
    IEnumerator SendDestroy()
    {
        yield return new WaitForSeconds(2f);
        bar.GetComponent<BarLifeDestroy>().DestroyUIbarA();
        barON = false; 
    }

}
