using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float minX, maxX;
  

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; //procura o objeto com a tag player, pego o transfrom e armazena na variavel player
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(player.position.x >= transform.position.x) // se a possição do player for maior q a da camera 
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); //pega a posição da camera e atribui um novo vetor de 3 posições, para o x e y a posição do player e z a da camera

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), 0, transform.position.z); // pega posição atual da camerea e atribui um vector de 3 posições, para o x usa o comando mathf.clamp para restringir o valor da camera entre o minimo em a maximo, 0 é a posição inicial da camera no eixo z 
        }
    }
}
