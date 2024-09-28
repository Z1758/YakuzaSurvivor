using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoiceManager : MonoBehaviour
{
    private static EnemyVoiceManager instance;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] int maxVoiceCount;
    [SerializeField] int voiceCount;

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

        audioSource = GetComponent<AudioSource>();  
    }

    public static EnemyVoiceManager Instance
    {

        get
        {

            return instance;
        }

    }

    public void PlayerVoice(AudioClip clip, Vector3 vec)
    {

       

        if (voiceCount >= maxVoiceCount)
        {


            transform.position = vec;
            audioSource.PlayOneShot(clip);

        }
        else
        {
            voiceCount++;
            return;
        }

        maxVoiceCount = Random.Range(0, 8);
        voiceCount = 0;

    }
}
