using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnife : Skill
{
    [SerializeField] GameObject knifePrefabs;
   
  
    
    Queue<Knife> knives;

    Coroutine throwCoroutine;

   
    int count;

    private void Awake()
    {

        count = info.maxLevel+1;
        onDelay = new WaitForSeconds(0.2f);
        offDelay = new WaitForSeconds(1.5f);

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
        SkillManager.Instance.StopSkills += StopSkill;
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
        count-=2;

        if(level == info.maxLevel)
        {
            GetUlt();



            onDelay = new WaitForSeconds(1.0f);

            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            
            throwCoroutine = StartCoroutine(ThrowingUlt());
            return;
        }
    }



    IEnumerator ThrowingCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                yield return onDelay;
            }
            Knife k =  knives.Dequeue();
            k.transform.position = transform.position;
            k.transform.rotation = transform.rotation;
            k.gameObject.SetActive(true);
            k.Throwing();
        }
    }

    IEnumerator ThrowingUlt()
    {
        while (true)
        {
            ult.transform.position = transform.position;
            ult.transform.rotation = transform.rotation;
            ult.gameObject.SetActive(true);
            yield return offDelay;

            ult.gameObject.SetActive(false);
            yield return onDelay;
        }
    }

    void ReturnKnife(Knife k)
    {
        k.gameObject.SetActive(false);
        knives.Enqueue(k);
    }

}
