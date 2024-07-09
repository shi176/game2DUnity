using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.CompareTag("player")){
            GameObject.Find("GamePlay").GetComponent<GamePlay>().UpdateQuantityKey();
            Destroy(gameObject);
        }
    }
}
