using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : MonoBehaviour
{
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
    }


    void Start()
    {
        activeMob = new List<GameObject>();
        queue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject m = Instantiate(prefab);
            m.SetActive(false);
            
            if(m.TryGetComponent<DisMob>(out DisMob me)){
                me.dEvent += ReturnPool;
            }
               
            queue.Enqueue(m);
        }

        StartCoroutine(SpawnMob());
    }


    IEnumerator SpawnMob()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (queue.Count != 0)
            {
                float ran1 = Random.Range(-15, 15);
                float ran2 = Random.Range(-15, 15);
                GameObject m = queue.Dequeue();
                m.transform.position = new Vector3(ran1, 0, ran2);
                m.SetActive(true);
                activeMob.Add(m);

            }
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
