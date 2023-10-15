using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoviment : MonoBehaviour
{
    Rigidbody2D rbPLayer;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] bool isJump;
    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Animator animPLayer;

    [SerializeField] bool dead = false;
    CapsuleCollider2D playerCollider;

    public static bool isGrow;

    private void Awake()
    {
        animPLayer = GetComponent<Animator>();
        rbPLayer = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();

    }

    private void Start()
    {
        dead = false;
        isGrow = false;
    }

    private void Update()
    {
        if (dead) return;

        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer); // adicionar fisica 2d a variavel inFloor e gera uma linha invisivel na posição atual do mario, verifica a colisão com a camada layer
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue); // se for verdadeiro  desenha a linha na cena

        animPLayer.SetBool("Jump", inFloor); //acessa o metodo bool do Jump e usa inFloor para saber quando o player estiver ou não no chão para ativar a animação

        if (Input.GetButtonDown("Jump") && inFloor)
            isJump = true;
                         

        else if (Input.GetButtonUp("Jump") && rbPLayer.velocity.y > 0)
            rbPLayer.velocity = new Vector2(rbPLayer.velocity.x, rbPLayer.velocity.y * 0.5f);

        animPLayer.SetBool("Grow", isGrow); // variavel animplayer que é o mario, passa o nome do parameto grow para o valor isGrow;

    }
    private void FixedUpdate()
    {
        Move();
        JumpPlayer();
    }
    void Move()
    {

        if (dead) return;

        float xMove = Input.GetAxis("Horizontal"); //movimento do eixo horizontal
        rbPLayer.velocity = new Vector2(xMove * speed, rbPLayer.velocity.y); //atribui velocidade ao mario

        animPLayer.SetFloat("Speed", Mathf.Abs(xMove)); //gera a velocidade para a animação do mario andando, adiciona a mathf.abs para ter o valor exato do Move porque ja tinha valores adicionados na Move

        if (xMove > 0) //move a posição do personagem de acordo com a botão apertado
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (xMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void JumpPlayer()
    {
        if (dead) return;

        if (isJump)
        {
            rbPLayer.velocity = Vector2.up * jumpForce; //atribui a velocidade do player o valor 15f e torna  variavel is jump falsa para que o player não va para o ceu
            isJump = false;
        }
    }

    public void Death()
    {
        StartCoroutine(DeathCorotine());
    }

    IEnumerator DeathCorotine()
    {
        if (!dead)
        {
            dead = true;
            animPLayer.SetTrigger("Death");
            yield return new WaitForSeconds(0.5f);
            rbPLayer.velocity = Vector2.zero;
            rbPLayer.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            playerCollider.isTrigger = true;
            Invoke("Restart Game", 2.5f);
        }
    }   
    
    void RestartGame()
    {
        SceneManager.LoadScene("Fase1");
    }

}
