using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnife : Skill
{
    [SerializeField] GameObject knifePrefabs;
   
  
    
    Queue<Knife> knives;

    Coroutine throwCoroutine;

    public WaitForSeconds knifeWFS;

    int count;

    private void Awake()
    {
        count = info.maxLevel+1;
        knifeWFS = new WaitForSeconds(0.2f);
  

        knives = new Queue<Knife>();

        for (int i = 0; i < 8; i++)
        {
            GameObject obj = Instantiate(knifePrefabs);

            Knife knife = obj.GetComponent<Knife>();
            knife.knifeDurationWFS = new WaitForSeconds(1.0f);
            knife.gameObject.SetActive(false);
            knife.returnEvent += ReturnKnife;
            knives.Enqueue(knife);
        }
    }

    private void Start()
    {
        UseSkill();
    }

    public override void UseSkill()
    {
        if(level == 1)
        {
            throwCoroutine = StartCoroutine(ThrowingCoroutine());
        }
    }
    public override void SkillLevelUp()
    {
        level++;
        if (level == 1)
        {
            gameObject.SetActive(true);
        }
        count--;

        if(level == info.maxLevel)
        {
            count--;
        }
    }
    IEnumerator ThrowingCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                yield return knifeWFS;
            }
            Knife k =  knives.Dequeue();
            k.transform.position = transform.position;
            k.transform.rotation = transform.rotation;
            k.gameObject.SetActive(true);
            k.Throwing();
        }
    }

    

    void ReturnKnife(Knife k)
    {
        k.gameObject.SetActive(false);
        knives.Enqueue(k);
    }

}
