using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRigi;
    private Animator animator;

    public float spaceMove;
    private Vector3 moveInput;

    private string navigationPlayer = "down";
    public AudioSource soundTurn;
    public GameObject zoneAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerRigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");


        if ((moveInput.x != 0) || (moveInput.y != 0))
        {
            transform.position += moveInput * spaceMove * Time.deltaTime;

            if (moveInput.x > 0) //di chuyển phải
            {
                navigationPlayer = "right";
            }
            else
            { //di chuyển trái
                navigationPlayer = "left";

            }

            if (moveInput.y > 0) //di chuyển lên
            {
                navigationPlayer = "up";

            }
            else if (moveInput.y < 0)
            { //di chuyển xuống
                navigationPlayer = "down";

            }

            Move();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

    }
    public void Move()
    {
        switch (navigationPlayer)
        {
            case "right": MoveRight(); break;
            case "left": MoveLeft(); break;
            case "up": MoveUp(); break;
            case "down": MoveDown(); break;
        }
    }

    private void MoveRight()
    {
        Vector2 scale = transform.localScale;
        scale.x *= scale.x > 0 ? 1 : -1;
        transform.localScale = scale;
        animator.SetBool("isMoveHorizontal", true);
        animator.SetBool("isMoveUp", false);
        animator.SetBool("isMoveDown", false);
    }
    private void MoveLeft()
    {
        Vector2 scale = transform.localScale;
        scale.x *= scale.x > 0 ? -1 : 1;
        transform.localScale = scale;
        animator.SetBool("isMoveHorizontal", true);
        animator.SetBool("isMoveUp", false);
        animator.SetBool("isMoveDown", false);
    }
    private void MoveUp()
    {
        //animation
        animator.SetBool("isMoveUp", true);
        animator.SetBool("isMoveDown", false);
        animator.SetBool("isMoveHorizontal", false);
        //animation

    }
    private void MoveDown()
    {
        //animation
        animator.SetBool("isMoveDown", true);
        animator.SetBool("isMoveUp", false);
        animator.SetBool("isMoveHorizontal", false);
        //animation

    }

    private void Attack()
    {

        switch (navigationPlayer)
        {
            case "right":
                animator.Play("PlayerAttackHorizontal");
                soundTurn.Play();
                break;
            case "left":
                animator.Play("PlayerAttackHorizontal");
                soundTurn.Play();
                break;
            case "up":
                animator.Play("PlayerAttackUp");
                soundTurn.Play();
                break;
            case "down":
                animator.Play("PlayerAttackDown");
                soundTurn.Play();
                break;
        }
        AttackKill();
    }

    public string getNavigationPlayer()
    {
        return navigationPlayer;
    }

    public void AttackKill()
    {

        Collider2D[] objects = Physics2D.OverlapBoxAll(zoneAttack.transform.position, new Vector3(0.2f,0.2f,0.2f),0);
        foreach (Collider2D collider in objects)
        {
            if(collider.CompareTag("enemy")){
                collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(50);
            }
            if(collider.CompareTag("boss")){
                collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(5);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.CompareTag("blood-item")){
            gameObject.GetComponent<PlayerHealth>().IncreasedBlood(15);
            Destroy(collider2D.gameObject);
        }
    }
}
