using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;


    private static UISoundManager instance;


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

        audioSource= GetComponent<AudioSource>();
    }

    public static UISoundManager Instance
    {

        get
        {

            return instance;
        }

    }

    public void PlayerUISound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
