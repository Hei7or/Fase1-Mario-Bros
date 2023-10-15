using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{

    Rigidbody2D rbGoomba;

    [SerializeField] float speed = 2; // variavel para ver no inspetor, valor de speed 2
    [SerializeField] Transform point1, point2;
    [SerializeField] LayerMask layer;
    [SerializeField] bool isColliding;

    Animator animGoomba;
    BoxCollider2D colliderGoomba;

    private void Awake()
    {
        rbGoomba = GetComponent<Rigidbody2D>();

        animGoomba = GetComponent<Animator>();
        colliderGoomba = GetComponent<BoxCollider2D>();

    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        rbGoomba.velocity = new Vector2(speed, rbGoomba.velocity.y); // velocidade do inimigo, eixo X usa a velocidade da variavel float e eixo y a velociade atual

        isColliding = Physics2D.Linecast(point1.position, point2.position, layer); //criar uma linha imaginaria para verificar a colisão do goomba

        Debug.DrawLine(point1.position, point2.position, Color.blue);

        if (isColliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1;
        }
    }

    void Update()
    {

    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (transform.position.y + 0.5f < collision.transform.position.y)
            {
                if (collision.GetComponent<CapsuleCollider2D>().isTrigger) return;
                collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                animGoomba.SetTrigger("Death");
                speed = 0;
                Destroy(gameObject, 0.3f);
                colliderGoomba.enabled = false;
            }
            else
            {
                if (PlayerMoviment.isGrow)
                {
                    PlayerMoviment.isGrow = false;
                }
                else
                {
                    FindAnyObjectByType<PlayerMoviment>().Death();

                    Goomba[] goomba = FindObjectsOfType<Goomba>();

                    for     (int i = 0; i < goomba.Length; i++)
                    {
                        goomba[i].speed = 0;
                        goomba[i].animGoomba.speed = 0;
                    }


                }
                
            }

        }

    }

}   