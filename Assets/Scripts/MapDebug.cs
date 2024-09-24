using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDebug : MonoBehaviour
{
    [SerializeField] Transform playerDebugSpawn;
    [SerializeField] Transform EnemyDebugSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if(other.tag == "Enemy")
        {
            
            other.transform.position = EnemyDebugSpawn.position;
        }
        else if (other.tag == "Player")
        {
            other.transform.position = playerDebugSpawn.position;
        }
    }

}
