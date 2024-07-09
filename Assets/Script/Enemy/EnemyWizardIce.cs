using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWizardIce : MonoBehaviour
{
    public GameObject player;
    public GameObject skillWizard;
    public AudioSource soundSkill, soundVoice;

    private Rigidbody2D enemyRigi;

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


            if (distance < distanceAttack) //enemy sẽ tấn công //khoảng cách từ nhân vật tới quái nhỏ hơn khoảng cách cho phép tấn công
            {
                if (!isAttacked)
                {
                    EnemyAttack();
                    StartCoroutine(DelayAttack(3f));
                }
            }
            else //nếu khoảng cách từ rương tới quái trong khoảng 1 thì cho quá chạy nhân vật
            {

                if (isDetectPlayer) //trường hợp quái đã gập nhân vật
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

                    soundVoice.Play();

                    isDetectPlayer = true;

                }
            }

        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("skill"))
        {
            gameObject.GetComponent<EnemyHealth>().TakeDamage(15);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("skill"))
        {
            gameObject.GetComponent<EnemyHealth>().TakeDamage(15);

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
        animator.SetBool("isWalk", false);
        animator.Play("wizard-ice-attack");

        soundSkill.Play();
        Instantiate(skillWizard, new Vector3(player.transform.position.x, player.transform.position.y + 1, 0), Quaternion.identity);
        isAttacked = true;
    }

    void PlaySoundSkill()
    {

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
