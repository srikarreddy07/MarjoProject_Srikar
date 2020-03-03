using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSlide : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speedMultipler = 5f;

    [SerializeField] InputState state;
    [SerializeField] LongJump jump;
    [SerializeField] PlayerMovement p;

    private void Start()
    {
        state = GetComponent<InputState>();
        jump = GetComponent<LongJump>();
        rb = GetComponent<Rigidbody2D>();
        p = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PowerWire")
        {
            state.enabled = false;
            jump.enabled = false;
            p.enabled = false;
            rb.velocity = new Vector2(rb.velocity.x * speedMultipler, rb.velocity.y);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PowerWire")
        {
            state.enabled = false;
            jump.enabled = false;
            p.enabled = false;
            rb.velocity = new Vector2(rb.velocity.x * speedMultipler, rb.velocity.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PowerWire")
        {
            state.enabled = true;
            jump.enabled = true;
            p.enabled = true;

            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
