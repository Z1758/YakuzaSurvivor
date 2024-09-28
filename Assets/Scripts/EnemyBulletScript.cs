using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] public int dmg;
    [SerializeField] public PlayController player;
    void Start()
    {
        rb.velocity = transform.forward * speed  ;

        Destroy(gameObject, 10f);
    }

   

    private void OnTriggerEnter(Collider other)
    {

        player.PlayerTakeDamage(dmg, transform.position);

        Destroy(gameObject);
    }
}
