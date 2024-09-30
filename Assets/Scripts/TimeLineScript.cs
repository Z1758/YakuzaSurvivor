using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TimeLineScript : MonoBehaviour
{
    [SerializeField] PlayerInput pi;
    [SerializeField] PlayController pc;
    [SerializeField] PlayableDirector pd;

    [SerializeField] PlayableAsset clearPB;
    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void StartGame()
    {
        pc.SetChangeStyle();
        pi.enabled = true;
    }

    public void StartClear()
    {
        pd.playableAsset = clearPB;
        pd.Play();
    }

   
}
