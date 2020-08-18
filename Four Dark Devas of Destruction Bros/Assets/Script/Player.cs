using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Has the jump force set as a float.
    public float jumpForce = 5f;
    //Current Jumps is set as an integer.
    public int currentJumps;
    //Max Jumps is set as an integer.
    public int maxJumps = 1;
    //Speed is set as a float.
    public float speed = 3f;
    // Height is set as a float which is the height for the player sprite.
    public float height = 0.65f;
    //This would have it get the rigid body of the player
    private Rigidbody2D rigidBody;
    //This would get the sprite renderer of the object.
    private SpriteRenderer sprite;
    //This would get the animator connected to the object.
    private Animator animationPlayer;
    //this has the location of the checkpoint
    public Vector3 checkpoint;
    public AudioClip enemyLaugh;
    // Start is called before the first frame update
    void Start()
    {
        //Sets rigidbody to the component of the game object.
        rigidBody = GetComponent<Rigidbody2D>();
        //Sets the sprite to the sprite renderer of the game object.
        sprite = GetComponent<SpriteRenderer>();
        //Sets the animation player to the animator componenet of the object
        animationPlayer = GetComponent<Animator>();
        //sets the cureent amount of jumps to the max amount of jumps.
        currentJumps = maxJumps;        

        //Set this object as the current player pawn in the scene
        GameManager.instance.currentPlayerPawn = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //This float is getting the x axis movement 
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //
        rigidBody.velocity = new Vector3(xMovement, rigidBody.velocity.y);
        //When the player is moving it will start to play the walking animation.
        if (rigidBody.velocity.x != 0)
        {
            animationPlayer.Play("Gundham_Walk");
            
        }
        //When the player is not moving it will activate the Idle animation of the sprite.
        else
        {
            animationPlayer.Play("Gundham_Idle");
            
        }
        //Whenever the player goes to the negative side of the x axis it will flip the image.
        sprite.flipX = rigidBody.velocity.x < 0;
        //When the player presses the jump button it checks to see if the player is grounded. Then activates the Jump function.
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                currentJumps = maxJumps;
                Debug.Log("hello");
            }
            if (currentJumps > 0)
            {
                Jump();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        // When the game object collides into an object with the tag Enemy they activate the function from the game manager.
        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.PlayerDeath();
            AudioSource.PlayClipAtPoint(enemyLaugh, transform.position);
        }
        //Once the player collides with what is labeled as doorway. It will load the scene Victory.
               
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //When the player hit's what is called the checkpoint it will begin to set the checkpoint to it's position.
        if (otherObject.gameObject.CompareTag("Checkpoint"))
        {
            GameManager.instance.checkpoint = transform.position;
        }
    }
    //Whenever it is activated both force activates during jumping and it subtracts the amount of jumps you can do.
    void Jump()
    {
        currentJumps--;
        rigidBody.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        
    }
    //This boolean is to check whether the raycast does hit the gorund.
    public bool IsGrounded()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, (height /2f) + 0.1f );
        return (hitinfo.collider != null);
    }

}
