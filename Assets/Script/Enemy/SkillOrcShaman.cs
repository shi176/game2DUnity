using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOrcShaman : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        StartCoroutine(DelayHide(5));
    }

    //sử lý sự kiên tấn công tại đây
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("player")){
           GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(30);
        }
    }

    IEnumerator DelayHide(float delayTime)
    {
        // Đợi trong khoảng thời gian delayTime
        yield return new WaitForSeconds(delayTime);

        animator.SetBool("isDead",true);
        Destroy(gameObject,1);
        // Code bạn muốn thực hiện sau khi delay
    }
}
