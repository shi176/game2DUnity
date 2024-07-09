using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyKitsune : MonoBehaviour
{
    public GameObject player;
    public GameObject skillKitsune;

    private Rigidbody2D enemyRigi;
    private Vector3 oldPosition;
    private Animator animator;
    public AudioSource soundSkill, soundVoice;

    private bool isAttacked = false;

    public float distanceAttack = 1f;

    public float speedEnemy = 1f;
    private bool isDetectPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {

            var distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
            //hướng
            Vector2 direction = player.transform.position - transform.position;

            ControlDirectionFace(direction.x);


            if (distance < distanceAttack) //enemy sẽ tấn công
            {
                if (!isAttacked)
                {

                    EnemyAttack();
                    StartCoroutine(DelayAttack(5f));

                }
            }
            else
            {
                if (isDetectPlayer)  //trường hợp quái đã gập nhân vật
                {
                    Vector3 normalizedDirection = direction.normalized;
                    transform.Translate(normalizedDirection * 1f * Time.deltaTime);
                    animator.SetBool("isWalk", true);
                }
                else //trường hợp quái gập nhân vật lần đầu
                {
                    Vector3 normalizedDirection = direction.normalized;
                    transform.Translate(normalizedDirection * 1f * Time.deltaTime);
                    animator.SetBool("isWalk", true);
                    isDetectPlayer = true;

                    soundVoice.Play();

                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("skill"))
        {
            gameObject.GetComponent<EnemyHealth>().TakeDamage(30);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("skill"))
        {
            gameObject.GetComponent<EnemyHealth>().TakeDamage(30);

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
        Instantiate(skillKitsune, transform.position, Quaternion.identity);
        isAttacked = true;
        animator.SetBool("isWalk", false);
        animator.Play("kitsune-attack");
        soundSkill.Play();
    }

    IEnumerator DelayAttack(float delayTime)
    {
        // Đợi trong khoảng thời gian delayTime
        yield return new WaitForSeconds(delayTime);
        isAttacked = false;
        // Code bạn muốn thực hiện sau khi delay
        Debug.Log("Delayed function executed!");
    }
}
