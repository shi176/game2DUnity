using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class haha : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool checkIsRight = true;
    private int maxJump = 2;
    private int jump;
    private bool checkBrick = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 vector3 = new Vector3(1, 0, 0);
            transform.Translate(vector3 * Time.deltaTime * 5f);
            checkIsRight = true;
            Vector2 scale = transform.localScale;
            scale.x = checkIsRight ? 1 : -1;
            transform.localScale = scale;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 vector3 = new Vector3(-1, 0, 0);
            transform.Translate(vector3 * Time.deltaTime * 5f);
          
            checkIsRight = true;
            Vector2 scale = transform.localScale;
            scale.x = checkIsRight ? -1 : 1;
            transform.localScale = scale;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (checkBrick && jump < maxJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 5f);

            }
            jump++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "brick")
        {
            checkBrick = true;
            jump = 0;
        }
        /*   if (collision.gameObject.tag == "enemy" && collision.contacts[0].normal.x > 0.5f)
           {
               Destroy(gameObject);
           }
           if (collision.gameObject.tag == "enemy" && collision.contacts[0].normal.y > 0.5f)
           {
               Destroy(collision.transform.parent.gameObject);
           }*/

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "top")
        {
            Destroy(collision.transform.parent.gameObject);
            Debug.Log("sds");
        }
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }
    }
}
