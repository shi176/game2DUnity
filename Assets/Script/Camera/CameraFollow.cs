using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject players;
    public float positionZ;
    private Vector3 vectorVelocity = Vector3.zero;
    public float startX, endX,startY,endY;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (players != null){

            var player = players.transform.position;
            var camX = transform.position.x;
            var camY = transform.position.y;

            //x
            if (camX > startX && camX < endX)
            {
                camX = player.x;
            }
            if (camX < startX)
            {
                camX = startX;
            }
            if (camX > endX)
            {
                camX = endX;
            }
            //y
            if (camY > startY && camY < endY)
            {
                camY = player.y;
            }
            if (camY < startY)
            {
                camY = startY;
            }
            if (camY > endY)
            {
                camY = endY;
            }

            transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(camX, camY, positionZ), ref vectorVelocity, 0.5f);
        }
    }
}
