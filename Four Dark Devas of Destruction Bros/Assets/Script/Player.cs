using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    public int maxJumps = 1;
    public float speed = 3f;
    public float height = 0.65f;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;
        
        rigidBody.velocity = new Vector3(xMovement, rigidBody.velocity.y);
        
        sprite.flipX = rigidBody.velocity.x < 0;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce);

    }
    bool IsGrounded()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, (height /2f) + 0.1f );
        return (hitinfo.collider != null);
    }
}
