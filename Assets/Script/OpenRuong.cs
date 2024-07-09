using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRuong : MonoBehaviour
{
    public GameObject itemsToDrop;
    public AudioSource soundOpen; 

   Animator animator;
    private bool checkOpen = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpenRuong", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && !checkOpen)

        {
            DropItems();
            animator.SetBool("isOpenRuong", true);
            soundOpen.Play();
        }
       
    }


    void DropItems()
    {  
        Instantiate(itemsToDrop, new Vector3(transform.position.x,transform.position.y - 0.3f, 0), Quaternion.identity);
        checkOpen = true;
           
    }
}
