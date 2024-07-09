using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;

    private Rigidbody2D enemyRigi;
    private Vector3 oldPosition;
    public GameObject item;

    public float jumpForce = 10f;
    public float distanceAttack = 1f;

    private Animator animator;
    public float speedEnemy = 1f;
    private bool isDetectPlayer = false;
    private bool isAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {

            var distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
            // Debug.Log(distance);
            //hướng
            Vector2 direction = player.transform.position - transform.position;

            ControlDirectionFace(direction.x);


            if (distance < distanceAttack) //enemy sẽ tấn công
            {
                if (!isAttacked)
                {

                    EnemyAttack();
                    StartCoroutine(DelayAttack(2f));

                }
            }
            else if (isDetectPlayer)
            {
                Vector3 normalizedDirection = direction.normalized;
                transform.Translate(normalizedDirection * 1f * Time.deltaTime);
                oldPosition = transform.position;
                animator.SetBool("isWalk", true);
            }
            else if ((distance > distanceAttack) && (distance < 5))
            {
                Vector3 normalizedDirection = direction.normalized;
                transform.Translate(normalizedDirection * 1f * Time.deltaTime);
                oldPosition = transform.position;
                animator.SetBool("isWalk", true);
                isDetectPlayer = true;

            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("skill"))
        {
            gameObject.GetComponent<EnemyHealth>().TakeDamage(100);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("skill"))
        {
            gameObject.GetComponent<EnemyHealth>().TakeDamage(100);

        }
    }

    void ControlDirectionFace(float x)
    {
        if (x > 0)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
        }
        else if (x < 0)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
        }
    }

    void EnemyAttack()
    {
        isAttacked = true;
        animator.Play("attack");
        animator.SetBool("isWalk", false);
        GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(2);
    }

    IEnumerator DelayAttack(float delayTime)
    {
        // Đợi trong khoảng thời gian delayTime
        yield return new WaitForSeconds(delayTime);
        isAttacked = false;
        // Code bạn muốn thực hiện sau khi delay
        Debug.Log("Delayed function executed!");
    }

    public void EnemyDieHandle()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            Destroy(gameObject);
        }
        if (random == 1)
        {

            Destroy(gameObject);

            //xử lí rớt vật phẩm máu ở đây
            if (item)
            {
                GameObject itemBlood=Instantiate(item, transform.position, Quaternion.identity);
                itemBlood.SetActive(true);
                itemBlood.GetComponent<PolygonCollider2D>().enabled=true;
                GameObject.Find("GamePlay").GetComponent<GamePlay>().UpPoint();
            }

        }
    }
}
