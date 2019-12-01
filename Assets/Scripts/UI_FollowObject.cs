using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_FollowObject : MonoBehaviour
{
    public GameObject barLife;
    public Image bar;
    public Image barfilled;

    public TextMeshProUGUI txtHP;
    //public TextMesh txtHP;
    public bool barON;

    void Start()
    {
        bar = Instantiate(barLife, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        barfilled = new List <Image>(bar.GetComponentsInChildren<Image>()).
                    Find(img => img != bar);
        txtHP = new List<TextMeshProUGUI>(bar.GetComponentsInChildren<TextMeshProUGUI>()).
                    Find(Text => Text != bar);
        barON = true;
    }
    private void Update()
    {
        if (barON == true)
        {
            bar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
        
    }
    public void BarLife(float lifeStatus)
    {
        barfilled.fillAmount = lifeStatus;
        txtHP.text = "HP: " + lifeStatus.ToString();
        if (lifeStatus <= 0)
        {
            DestroyUIbar();
        }
    }
    public void DestroyUIbar()
    {
        StartCoroutine(SendDestroy());
    }
    IEnumerator SendDestroy()
    {
        yield return new WaitForSeconds(1f);

        barON = false;
        bar.GetComponent<BarLifeDestroy>().DestroyUIbarA();
    }

}
