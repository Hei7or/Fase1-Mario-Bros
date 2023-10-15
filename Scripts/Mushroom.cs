using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool moveLeft;
    void Start()
    {
        moveLeft = false;
    }

    
    void Update()
    {
        if (moveLeft) // se for verdadeiro pega o eixo x e multiplica por speed para normalizar os quadors usa deltatime, se não faz o mesmo para direita
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // se p obeto colidir com o cano que é a layer 3, se não estiver para a esquerda vai para direita e o contrario vice e versa
    {
        if (collision.gameObject.layer == 3)
        {
            if (!moveLeft)
            {
                moveLeft = true;
            }
            else

            {
                moveLeft = false;
            }

        }

        if (collision.CompareTag("Player")) //verifica se  a colisão é com o mario, o mario vai crescer
        {
           PlayerMoviment.isGrow = true;
            Destroy(gameObject);
        }
    }    
}
