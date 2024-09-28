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

    WaitForSeconds disableDelay;
 
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

        disableDelay = new WaitForSeconds(2.0f);

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
            if (m.TryGetComponent<Enemy>(out Enemy me))
            {
                me.ActiveEnemy();
                me.dieEvent += ReturnPool;
            }

            
            activeMob.Add(m);

        }
        else
        {
            GameObject m = Instantiate(prefabs[type]);
            m.transform.position = spawnPoints[ran].position;
            if (m.TryGetComponent<Enemy>(out Enemy me))
            {
                me.ActiveEnemy();
                me.dieEvent += ReturnPool;
            }
          
    
            activeMob.Add(m);
        }
    }

    void ReturnPool(GameObject mob, int type)
    {


        if (mob.TryGetComponent<Enemy>(out Enemy me))
        {
           
            me.dieEvent -= ReturnPool;
        }

     

        queue[type].Enqueue(mob);
        activeMob.Remove(mob);

        StartCoroutine(DisableEnemy(mob));

       
    }

    IEnumerator DisableEnemy(GameObject mob )
    {
        yield return disableDelay;

        mob.SetActive(false);
    }
}
