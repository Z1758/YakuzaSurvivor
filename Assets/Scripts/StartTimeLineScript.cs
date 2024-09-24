using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartTimeLineScript : MonoBehaviour
{
    [SerializeField] PlayerInput pi;
    [SerializeField] PlayController pc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        pc.ChangeStyle();
        pi.enabled = true;
    }
}
