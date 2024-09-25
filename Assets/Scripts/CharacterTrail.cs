using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrail : MonoBehaviour
{
    private static CharacterTrail instance;

    [SerializeField] Trails[] trails;
    [ColorUsage(true, true)]
    [SerializeField] public Color[] color;

    [SerializeField] Trails dagger;
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

    public static CharacterTrail Instance
    {

        get
        {

            return instance;
        }

    }

    public void OnDaggerTrail()
    {
        for (int i = 0; i < trails.Length; i++)
        {
            dagger.OnTrail();
        }
    }

    public void OffDaggerTrail()
    {
        for (int i = 0; i < trails.Length; i++)
        {
            dagger.OffTrail();
        }
    }

    public void OnTrail(int cnt)
    {
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].OnTrails(color[cnt]);
        }
    }
    public void OffTrail()
    {
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].OffTrail();
        }
    }
}
