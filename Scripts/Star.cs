using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // sempre que o script foir anexado a um objeto vai ser o rigidbody2d

public class Star : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] bool moveRight = true;
    [SerializeField] float jumpForce = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // armazena o rigidbody2d na variavel RB
    }

    void Update()
    {
         if(moveRight)  // verifica se moverRight é verdadeiroe  faz a translação do movimento da estrela, se moveRight for falso a estrela vai movimentar para a esquerda
         {
            transform.Translate(7 * Time.deltaTime, 0, 0);
         }
         else 
         {
            transform.Translate(-7 * Time.deltaTime, 0, 0);
         }        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        switch (collision.gameObject.layer)  //verifica a colisão da estrela
        {
            case 3: // caso a layer seja 3

                if (moveRight) // se moveRight for verdadeira vai ser torna falso e fazendo a estrela movimentar para esquerda
                {
                    moveRight = false;
                }
                else //se a moveRight for falsa a estrela vai ir para direita
                {
                    moveRight = true;
                }
                break;

            case 9: // caso encoste no nosso player vai destroir a estrela
                Destroy(gameObject);
                break;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.layer == 6) //se a colisão é com objeto da layer 6 que é o chão
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // se encostar no chão vai adicionar uma força a estrela
        }
    }

}
