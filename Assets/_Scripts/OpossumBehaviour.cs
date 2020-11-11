using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumBehaviour : MonoBehaviour
{
    public float runSpeed;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public bool isGroundedAhead;
    public LayerMask collisionGroundLayer;
    public LayerMask collisionWallLayer;
    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isGroundedAhead = false;
        direction = 1.0f;
        //rigidbody2D.velocity = Vector2.left * runSpeed;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void FixedUpdate()
    {
        _LookInFront();
        _LookAhead();
        _Move(); 
    }

    private void _LookInFront()
    {
        if(Physics2D.Linecast(transform.position, lookInFrontPoint.position, collisionWallLayer))
        {
            _FlipX();
            direction *= -1.0f;
        }

        Debug.DrawLine(transform.position, lookInFrontPoint.position, Color.red);
    }

    private void _LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, collisionGroundLayer);

        if(hit.collider != null)
        {
            isGroundedAhead = true;
        }
        else
        {
            isGroundedAhead = false;
        }
            
        //if(hit.collider.CompareTag("Platforms"))
        //{
        //    isGroundedAhead = true;
        //}
        //else
        //{
        //    isGroundedAhead = false;
        //}

        Debug.DrawLine(transform.position, lookAheadPoint.position, Color.green);
    }

    private void _Move()
    {
        if (isGroundedAhead)
        {
            rigidbody2D.AddForce(Vector2.left * runSpeed * Time.deltaTime * direction);
        }
        else
        {
            _FlipX();
            direction *= -1.0f;
        }

        
        rigidbody2D.velocity *= 0.90f;

        //rigidbody2D.velocity = Vector2.left * runSpeed;
        //transform.position += new Vector3(rigidbody2D.velocity.x * runSpeed, 0.0f, 0.0f);
        //rigidbody2D
    }

    private void _FlipX()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    isGrounded = true;
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    isGrounded = false;
    //}
}
