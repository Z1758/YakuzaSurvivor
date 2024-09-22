using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    private static BGM_Manager instance;

    [SerializeField] private AudioClip[] bgm;
    [SerializeField] private AudioSource sources;



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

    public static BGM_Manager Instance
    {

        get
        {

            return instance;
        }
    
    }

    public void ChangeBGM(int cnt)
    {
        sources.clip = bgm[cnt];

        sources.Play();
    }

}
