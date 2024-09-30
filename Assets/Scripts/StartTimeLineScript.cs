using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartTimeLineScript : MonoBehaviour
{
    [SerializeField] PlayerInput pi;
    [SerializeField] PlayController pc;


    public void StartGame()
    {
        pc.SetChangeStyle();
        pi.enabled = true;
    }
}
