using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdapter : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;

    public void TargetInteract()
    {
        OnInteract?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        TargetInteract();
    }
}
