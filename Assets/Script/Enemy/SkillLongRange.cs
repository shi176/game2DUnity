using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLongRange : MonoBehaviour
{
    private Vector3 playerPosition;
    private Animator animator;
    public float speedMove;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Player>().transform.position;
        if (playerPosition != null)
        {
            Vector2 direction = playerPosition - transform.position;
            Vector3 normalizedDirection = direction.normalized;
            transform.Translate(normalizedDirection * speedMove * Time.deltaTime);
        }

    }

    //sử lý sự kiên tấn công tại đây
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player"))
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject, 0.4f);
            GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(20);
        }
    }
}
