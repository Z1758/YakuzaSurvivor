using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BakeTrail : MonoBehaviour
{
    [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] public MeshFilter meshFilter;
    [SerializeField] public MeshRenderer meshRenderer;


    public Mesh bakedMesh;

    [SerializeField] public float fade;
    [SerializeField] public float curFade;
    [SerializeField] public float minusFade;

    [SerializeField] public float inten;

    private void Awake()
    {
        bakedMesh = new Mesh();
       
    }

    void Start()
    {
        curFade = fade;
 
      
    }

    private void Update()
    {
       
    }


    public void SetColor(Color color)
    {
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {

            meshRenderer.materials[i].SetColor("_Fren", color);
        }

    }

    public void SetSMR(SkinnedMeshRenderer smr)
    {
       
        skinnedMeshRenderer = smr;
        
    }

    public void Bake()
    {


        skinnedMeshRenderer.BakeMesh(bakedMesh);
       
        meshFilter.mesh = bakedMesh;
    
    }

    public void SetMinus(int cnt)
    {
        minusFade = fade / cnt;
       
    }

    public void FadeOut()
    {
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            meshRenderer.materials[i].SetFloat("_Fade", curFade);
        }
        curFade -= minusFade;
        if(curFade < 0)
        {
            curFade = 0;
        }
    }

    public void SetFade()
    {
        curFade = fade;
    }
}
