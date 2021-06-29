using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    private bool isPlayerInRange;
    
    public Transform player;
    public EndGame gameEnding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform==player)
        {
            isPlayerInRange = true;
        }    
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            //le sumo vector up porque el origen esta en los pies
            Vector3 direction =  player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            //tambien se podria sobreescribir on draw gizmos para ver donde estan mis gargolas, solo salen en modo edicion en la view
            Debug.DrawRay(transform.position, direction, Color.green, Time.deltaTime,true);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray,out raycastHit))
            {
                if (raycastHit.collider.transform==player)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawSphere(transform.position,0.01f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,player.position+Vector3.up);
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.transform==player)
        {
            isPlayerInRange = false;
        }
    }
}
