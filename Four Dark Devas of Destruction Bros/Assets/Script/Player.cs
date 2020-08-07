using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    public int currentJumps;
    public int maxJumps = 1;
    public float speed = 3f;
    public float height = 0.65f;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private Animator animationPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animationPlayer = GetComponent<Animator>();
        currentJumps = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        rigidBody.velocity = new Vector3(xMovement, rigidBody.velocity.y);
        
        if (rigidBody.velocity.x != 0)
        {
            animationPlayer.Play("Gundham_Walk");
            
        }
        else
        {
            animationPlayer.Play("Gundham_Idle");
            
        }
        sprite.flipX = rigidBody.velocity.x < 0;

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                currentJumps = maxJumps;
            }
            if (currentJumps > 0)
            {
                Jump();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Game_Over");
        }
        if (otherObject.gameObject.CompareTag("Doorway"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Victory");
        }
    }
    void Jump()
    {
        currentJumps--;
        rigidBody.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        
    }
    public bool IsGrounded()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, (height /2f) + 0.1f );
        return (hitinfo.collider != null);
    } 
}
