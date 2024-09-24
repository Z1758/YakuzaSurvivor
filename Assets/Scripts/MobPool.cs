using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    public static MobPool Instance { get; private set; }
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize;
    Queue<GameObject> queue;

    public List<GameObject> activeMob;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        activeMob = new List<GameObject>();
        queue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject m = Instantiate(prefab);
            m.SetActive(false);

            if (m.TryGetComponent<DisMob>(out DisMob me))
            {
                me.dEvent += ReturnPool;
            }

            queue.Enqueue(m);
        }

    }


    void Start()
    {
      

    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnMobCorouinte());
    }

    IEnumerator SpawnMobCorouinte()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Spawn();
        }
    }

    void Spawn()
    {
        if (queue.Count != 0)
        {
            int ran = Random.Range(0, spawnPoints.Length);

            GameObject m = queue.Dequeue();
            m.transform.position = spawnPoints[ran].position;
            m.SetActive(true);
            activeMob.Add(m);

        }
    }

    void ReturnPool(GameObject mob)
    {
       
        mob.SetActive(false);
        queue.Enqueue(mob);
        activeMob.Remove(mob);
    }

    void Update()
    {

    }
}
