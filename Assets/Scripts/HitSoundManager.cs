using UnityEngine;

public class HitSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] HitSounds[] hs;

    private static HitSoundManager instance;


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

    public static HitSoundManager Instance
    {

        get
        {

            return instance;
        }

    }

    public void EnemyHitSound(Vector3 vec, int cnt, bool type)
    {
        transform.position = vec;

        if (type)
        {
            audioSource.PlayOneShot(hs[PlayerStat.Instance.StyleIndex].fAtkClips[cnt]);

        }
        else
        {
            audioSource.PlayOneShot(hs[PlayerStat.Instance.StyleIndex].atkClips[cnt]);
        }


    }

    public void SkillSound(Vector3 vec, AudioClip clip)
    {
      
        transform.position = vec;


        audioSource.PlayOneShot(clip);


    }


    public void PlayerHitSound(Vector3 vec)
    {
        transform.position = vec;
        audioSource.PlayOneShot(hs[0].atkClips[0]);



    }
}
