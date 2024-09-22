using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEvent : MonoBehaviour
{
    [SerializeField] PlayerSoundManager soundManager;
    [SerializeField] PlayController controller;

    public void AtkSound(int cnt)
    {

        soundManager.PlaySound(cnt, true);
    }

  

    public void AtkVoice(int cnt)
    {
        soundManager.PlayVoice(cnt, true);
    }
    public void FAtkSound(int cnt)
    {
        soundManager.PlaySound(cnt, false);
    }

    public void FAtkVoice(int cnt)
    {
        soundManager.PlayVoice(cnt, false);
    }

    public void LoopStartVoice(int cnt)
    {

        soundManager.PlayLoopStartVoice(cnt);
    }

    public void LoopVoice(int cnt)
    {

        soundManager.PlayLoopVoice(cnt);
    }

    public void LoopEndVoice(int cnt)
    {

        soundManager.PlayLoopEndVoice(cnt);
    }


    public void LoopStartSound(int cnt)
    {

        soundManager.PlayLoopStartSwing(cnt);
    }

    public void LoopSound(int cnt)
    {

        soundManager.PlayLoopSwing(cnt);
    }

    public void LoopEndSound(int cnt)
    {

        soundManager.PlayLoopEndSwing(cnt);
    }


    public void ChangeSound(int cnt)
    {
        soundManager.PlayerChangeSound(cnt);
    }

}