using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDebug : MonoBehaviour
{
    [SerializeField] Transform playerDebugSpawn;
    [SerializeField] Transform EnemyDebugSpawn;

  

    private void OnTriggerEnter(Collider other)
    {
     
        if(other.tag == "Enemy")
        {
            
            other.gameObject.transform.position = EnemyDebugSpawn.position;
        }
        else if (other.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
           
            other.gameObject.transform.position = playerDebugSpawn.position;

            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }

}
