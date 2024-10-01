using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;

    [SerializeField] GameObject[] prefabs;


    private void Awake()
    {
        if (null == instance)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

     
    }

    public static ItemManager Instance
    {

        get
        {

            return instance;
        }

    }

    public void CreateItem(Vector3 vec)
    {
        int ran = Random.Range(0, 101);
        if(0 < ran & ran <= 2 )
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], vec, Quaternion.identity);
    }
}
