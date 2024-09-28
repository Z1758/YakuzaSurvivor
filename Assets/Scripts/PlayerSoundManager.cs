using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource voiceSource;
    [SerializeField] AudioSource soundSource;

    [SerializeField] BattleStyleSound[] styles;


    [SerializeField] BattleStyleSound curStyle;

    void Awake()
    {
        SetSound(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSound(int index)
    {
        curStyle = styles[index];

    }

    public void PlayerChangeSound(int cnt)
    {
        if (styles[cnt].changeClip != null)
            voiceSource.PlayOneShot(styles[cnt].changeClip);
        if (styles[cnt].changeSwingClip != null)
            soundSource.PlayOneShot(styles[cnt].changeSwingClip);
    }

    public void PlayVoice(int cnt, bool type)
    {
        if (type)
        {
            if (curStyle.atkVoiceClips[cnt] == null)
                return;
            voiceSource.PlayOneShot(curStyle.atkVoiceClips[cnt]);
        }
        else
        {
            if (curStyle.fAtkVoiceClips[cnt] == null)
                return;
            voiceSource.PlayOneShot(curStyle.fAtkVoiceClips[cnt]);
        }

  
    }

    public void PlaySound(int cnt, bool type)
    {
        if (type)
        {
            if (curStyle.atkSwingClips[cnt] != null)
                soundSource.PlayOneShot(curStyle.atkSwingClips[cnt]);

        }
        else
        {
            if (curStyle.fAtkSwingClips[cnt] != null)
                soundSource.PlayOneShot(curStyle.fAtkSwingClips[cnt]);
        }


    }


    public void PlayLoopStartVoice(int cnt)
    {

        if (curStyle.loopStartVoiceClips[cnt] == null)
            return;
        voiceSource.PlayOneShot(curStyle.loopStartVoiceClips[cnt]);



       
    }

    public void PlayLoopVoice(int cnt)
    {

        if (curStyle.loopVoiceClips[cnt] == null)
            return;
        voiceSource.PlayOneShot(curStyle.loopVoiceClips[cnt]);



      
    }

    public void PlayLoopEndVoice(int cnt)
    {

        if (curStyle.loopEndVoiceClips[cnt] == null)
            return;
        voiceSource.PlayOneShot(curStyle.loopEndVoiceClips[cnt]);



       
    }

    public void PlayLoopStartSwing(int cnt)
    {

        if (curStyle.loopStartSwingClips[cnt] != null)
            soundSource.PlayOneShot(curStyle.loopStartSwingClips[cnt]);
    }
    public void PlayLoopSwing(int cnt)
    {

        if (curStyle.loopSwingClips[cnt] != null)
            soundSource.PlayOneShot(curStyle.loopSwingClips[cnt]);
    }
    public void PlayLoopEndSwing(int cnt)
    {

        if (curStyle.loopEndSwingClips[cnt] != null)
            soundSource.PlayOneShot(curStyle.loopEndSwingClips[cnt]);
    }
}
