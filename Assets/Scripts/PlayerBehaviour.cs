using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    // forca para ser aplicada quando o Player pula
    public Vector2 jumpForce = new Vector2(0, 450);

    //velocidade do eixo x
    public float maxSpeed = 3.0f;

    //um modificador para a forca aplicada
    public float speed = 50.0f;

    //forca aplicada para movimentar o player
    private float xMove;

    //flag para indicar pulo
    private bool shouldJump;

    // agiliza os saltos
    private bool onGround;
    private float yPrevious;

    //verifica se ira colidir com uma parede
    private bool collidingWall;

	// Use this for initialization
	void Start () {
        shouldJump = false;
        xMove = 0.0f;
        onGround = false;
        yPrevious = Mathf.Floor(transform.position.y);

        collidingWall = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        //verifica se esta no chao
        CheckGrounded();
        //Pula quando pressionar o botão
        Jumping();
	}

    void FixedUpdate()
    {
        //movimenta o player para esquerda ou direita
        Movement();
        //coloca a camera centrada no player com a profundidade adequada
        Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y,
            Camera.main.transform.position.z);
    }

    void Movement()
    {
        //pega o movimento: -1 para esquerda, +1 para direita, 0 nada
        xMove = Input.GetAxis("Horizontal");

        if (collidingWall && !onGround)
        {
            xMove = 0;
        }

        if (xMove != 0)
        {
            //movimento horizontal
            float xSpeed = Mathf.Abs(xMove * rigidbody.velocity.x);

            if (xSpeed < maxSpeed)
            {
                Vector3 movementForce = new Vector3(1, 0, 0);
                movementForce *= xMove * speed;
                
                //melhora colisão com a parede
                RaycastHit hit;
                if (!rigidbody.SweepTest(movementForce, out hit, 0.05f))
                {
                    rigidbody.AddForce(movementForce);
                }

            }
            if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
            {
                Vector2 newVelocity;
                newVelocity.x = Mathf.Sign(rigidbody.velocity.x) * maxSpeed;
                newVelocity.y = rigidbody.velocity.y;
                rigidbody.velocity = newVelocity;

            }
        }
        else
        {
            //se nao se altera, diminui a velocidade suavemente
            Vector2 newVelocity = rigidbody.velocity;
            //reduz a velocidade por 10%
            newVelocity.x *= 0.9f;
            rigidbody.velocity = newVelocity;
        }
        
    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            shouldJump = true;
        }
        //se o player deve pular
        if (shouldJump && onGround)
        {
            rigidbody.AddForce(jumpForce);
            shouldJump = false;
        }
    }
    
    void OnDrawGizmos(){
        Debug.DrawLine(transform.position, transform.position + rigidbody.velocity, Color.red);

    }

    void CheckGrounded()
    {
        //verifica se o player esta acertando para o centro do objeto(origem)
        //para proximo da parte de baixo (distancia)
        float distance = GetComponent<CapsuleCollider>().height / 2 * this.transform.localScale.y + 0.01f;
        Vector3 floorDirection = transform.TransformDirection(-Vector3.up);
        Vector3 origin = transform.position;
        if (!onGround)
        {
            if (Physics.Raycast(origin, floorDirection, distance))
            {
                onGround = true;
            }
        }
        else if (Mathf.Floor(transform.position.y) != yPrevious)
        {
            onGround = false;
        }
        //nossa posição corrente sera nosssa proxima, no outro frame
        yPrevious = Mathf.Floor(transform.position.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!onGround)
        {
            collidingWall = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        collidingWall = false;
    }

}
