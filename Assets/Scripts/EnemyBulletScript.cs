using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    void Start()
    {
        rb.velocity = transform.forward * speed  ;

        Destroy(gameObject, 10f);
    }

   

    private void OnTriggerEnter(Collider other)
    {
  
        Destroy(gameObject);
    }
}
