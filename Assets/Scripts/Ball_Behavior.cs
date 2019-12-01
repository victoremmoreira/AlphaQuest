using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Behavior : MonoBehaviour
{
    [SerializeField]
    public float speed;

    public float radius;
    public Vector2 direction;

    //Caracteristicas
    public float hp;
    public float damage;

    //Animações
    public Animator animator;
    public GameObject fxExplosion;

    //Status
    UI_FollowObject uiBar;
    public Controller controller;

    void Start()
    {
        //direction = Vector2.one.normalized;
        direction = new Vector2(Random.value, Random.value);

        radius = transform.localScale.x / 2;
        print("direction: " + direction + " radius: " + radius);

        //Animações
        animator = GetComponent<Animator>();

        //Status
        uiBar = GetComponent<UI_FollowObject>();
        controller = GameObject.Find("Controller").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        //Função bate rebate nas paredes responsivo a tela
        if (transform.position.y < Controller.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
            Damage();
        }
        if (transform.position.y > Controller.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
            Damage();
        }
        if (transform.position.x < Controller.bottomLeft.x + radius && direction.x < 0)
        {
            direction.x = -direction.x;
            Damage();
        }
        if (transform.position.x > Controller.topRight.x - radius && direction.x > 0)
        {
            direction.x = -direction.x;
            Damage();
        }

    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Ball")
        {
            direction.y = -direction.y;
            direction.x = -direction.x;
            
        }
    }
    public void Damage()
    {
        hp = hp - damage;
        if (hp > 0.1)
        {
            animator.SetTrigger("Collider");
            uiBar.BarLife(hp);
        }
        if (hp <= 0.0f)
        {

            StartCoroutine(Exploding());
            uiBar.DestroyUIbar();
        }
    }
    IEnumerator Exploding()
    {
        animator.SetTrigger("Desappear");
        fxExplosion.SetActive(true);
        yield return new WaitForSeconds(2f);
        controller.DecreaseBall();
        Destroy(gameObject);
    }

}
