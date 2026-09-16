using System.Numerics;
using System.Reflection;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rd;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool isGrounded;
    private Animator animator;
    private bool facinRigth = true;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float speedAnimation = Mathf.Abs(move);
        animator.SetFloat("Speed", speedAnimation);
  

        rd.linearVelocity = new Vector2(move * moveSpeed, rd.linearVelocity.y);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isGrounded = false;
            animator.SetBool("isJump",true);
        }

        if(move > 0 && !facinRigth){
            Flip();
        }else if (move < 0 && facinRigth){
            Flip(); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJump", false);
        }
    }

     void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

        }
    }

    void Flip(){
        facinRigth = !facinRigth;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
