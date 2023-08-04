using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float rightForce = 5;
    [SerializeField] float rightVelocity = 1;
    bool stopped = false;
    public bool Stop { set { stopped = value; } }

    bool outOfMoves = false;
    public bool MovesComplete { set { outOfMoves = value; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetCheckHeight();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if(rb.velocity.x < rightVelocity) rb.AddForce(Vector2.right * rightForce);

        if(!stopped && outOfMoves && rb.velocity.magnitude < 0.01f)
        {
            Debug.Log(rb.velocity.x);
            FindObjectOfType<LevelHandler>().BallStuck();
        }
    }
    float start;
    float maxHeight;
    float targetHeight;
    private void ResetCheckHeight()
    {
        start = transform.position.y;
        maxHeight = start;
    }

    [SerializeField] float jumpCoefficient = 1.05f;
    public void Jump(int height)
    {
        targetHeight = height;
        Debug.Log($"jump height: {height}");
        Vector2 velocity = Vector2.up * Mathf.Sqrt(2 * (-1)*Physics2D.gravity.y*height * jumpCoefficient);
        rb.velocity = velocity;

        ResetCheckHeight();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            FindObjectOfType<LevelHandler>().Pickup();
            Destroy(collision.gameObject);
        }
    }
}
