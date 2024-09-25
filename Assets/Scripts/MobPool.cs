using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : MonoBehaviour
{
    [SerializeField] CSV_Parser csv;
    [SerializeField] Transform[] spawnPoints;
    public static MobPool Instance { get; private set; }
    [SerializeField] GameObject[] prefabs;
    [SerializeField] int[] poolSize;
    Queue<GameObject>[] queue;

    public List<GameObject> activeMob;

    Coroutine spawnCoroutine;

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

        csv = GetComponent<CSV_Parser>();

        activeMob = new List<GameObject>();

      
        queue = new Queue<GameObject>[prefabs.Length];
        for(int i = 0; i < queue.Length; i++)
        {
            queue[i] = new Queue<GameObject>();
        }
        

       for (int i = 0; i < prefabs.Length; i++)
        {
          
            for (int y = 0; y < poolSize[i]; y++)
            {
                GameObject m = Instantiate(prefabs[i]);
                m.SetActive(false);

                if (m.TryGetComponent<Enemy>(out Enemy me))
                {
                    me.dieEvent += ReturnPool;
                }

                queue[i].Enqueue(m);
            }
        }
       

    

    }


    void Start()
    {
        StartSpawn();

    }

    public void StartSpawn()
    {
        if (spawnCoroutine != null) { 
            StopCoroutine(spawnCoroutine);
        }
   

        spawnCoroutine = StartCoroutine(SpawnMobCorouinte());
    }

    IEnumerator SpawnMobCorouinte()
    {
        StageData stageData = csv.stageDatas.Dequeue();

        WaitForSeconds delay = new WaitForSeconds(stageData.delay);
        int count = stageData.count;


        while (count > 0 )
        {
           


            yield return delay;
            Spawn(stageData.type);
            count--;
        }

        if(csv.stageDatas.Count > 0)
        StartSpawn();
    }

    void Spawn(int type)
    {

        int ran = Random.Range(0, spawnPoints.Length);
        if (queue[type].Count != 0)
        {
            

            GameObject m = queue[type].Dequeue();
            m.transform.position = spawnPoints[ran].position;
            m.SetActive(true);
            activeMob.Add(m);

        }
        else
        {
            GameObject m = Instantiate(prefabs[type]);
            if (m.TryGetComponent<Enemy>(out Enemy me))
            {
                me.dieEvent += ReturnPool;
            }
            m.transform.position = spawnPoints[ran].position;
    
            activeMob.Add(m);
        }
    }

    void ReturnPool(GameObject mob, int type)
    {
       
        mob.SetActive(false);
        

        queue[type].Enqueue(mob);
        activeMob.Remove(mob);
    }

 
}
