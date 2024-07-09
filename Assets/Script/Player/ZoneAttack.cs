using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttack : MonoBehaviour
{
    public GameObject player;
    private float heightPlayer;
    private Vector3 oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        heightPlayer = player.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        oldPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            var positionPlayer = player.transform.position;
            var navigationPlayer = GameObject.Find("Player").GetComponent<Player>().getNavigationPlayer();
            if (navigationPlayer == "up")
            {
                transform.position = new Vector3(positionPlayer.x,positionPlayer.y + heightPlayer/6,0);
            }
            else if (navigationPlayer == "down")
            {
                transform.position = new Vector3(positionPlayer.x,positionPlayer.y - heightPlayer/6,0);
            }
            else{
                transform.position=positionPlayer;
            }
        }


    }

    void OnTriggerEnter2D(Collider2D collider2D){
        // if(collider2D.CompareTag("enemy")){
        //     collider2D.gameObject.GetComponentInChildren<EnemyHealth>().TakeDamage(10);
        // }
    }


}
