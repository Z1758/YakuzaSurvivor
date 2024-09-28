using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.XR.TrackedPoseDriver;

public class BGM_Manager : MonoBehaviour
{
    private static BGM_Manager instance;

    [SerializeField] PlayerStat model;

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
    private void OnEnable()
    {

        model.OnStyleChanged += ChangeBGM;

    }
    private void OnDisable()
    {

        model.OnStyleChanged -= ChangeBGM;

    }
    public void ChangeBGM(int num)
    {
        sources.clip =bgm[num];

        sources.Play();
    }

    public void ChangeBgmOnce(AudioClip clip) 
    {
        sources.clip = clip;

        sources.Play();
    }
 
}
