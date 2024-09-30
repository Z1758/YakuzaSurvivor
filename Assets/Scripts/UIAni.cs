using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAni : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(UltUI());
    }

    IEnumerator UltUI()
    {

        yield return new WaitForSeconds(1.32f);
        gameObject.SetActive(false);
    }
    
}
