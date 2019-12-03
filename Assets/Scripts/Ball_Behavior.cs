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

    //Boys
    public GameObject boysPrefab;
    public string parent;

    void Start()
    {
        direction = new Vector2(Random.value, Random.value);
        radius = transform.localScale.x / 2;
        print("direction: " + direction + " radius: " + radius);

        //Animações
        animator = GetComponent<Animator>();

        //Status
        uiBar = GetComponent<UI_FollowObject>();
        controller = GameObject.Find("Controller").GetComponent<Controller>();
    }

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
            animator.SetTrigger("Collider"); //Animação de Colisão
            uiBar.BarLife(hp); // Atualização do Status da Barra de HP
        }
        if (hp <= 0.0f)
        {
            StartCoroutine(Exploding()); //Delay para Explosão
            uiBar.BarLife(0); // Atualização do Status da Barra de HP
        }
    }
    IEnumerator Exploding()
    {
        //Animações de Explosão
        animator.SetTrigger("Desappear");
        fxExplosion.SetActive(true);

        yield return new WaitForSeconds(2f);

        //Função Boys
        int boys = Random.Range(1, 3);
        if (boys == 2 && parent == "Father")
        {
            print("Swpam boys");
            Instantiate(boysPrefab, transform.position, transform.rotation);
            Instantiate(boysPrefab, transform.position, transform.rotation);
            controller.numBall += 2;
        }
        controller.DecreaseBall(); //Diminuindo a quantidade de Bolas no Controller
        Destroy(gameObject);
    }

}
