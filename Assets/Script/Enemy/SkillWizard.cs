using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWizard : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D collider2D){
        if(collider2D.CompareTag("player")){
            GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(20);
        }
    }
}
