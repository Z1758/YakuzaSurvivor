using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] Transform target;

    private Mesh bakedMesh;

    [SerializeField] float delay;
    WaitForSeconds delayWFS;

    Coroutine BCorouine;

    Vector3 prevVector;
    Quaternion prevRotation;

    void Start()
    {
        bakedMesh = new Mesh();

        delayWFS = new WaitForSeconds(delay);

        prevRotation = Quaternion.identity;
        prevVector = transform.localPosition;

        BCorouine = StartCoroutine("BakeCoroutine");
    }


    IEnumerator BakeCoroutine()
    {

        while (true)
        {
           
            skinnedMeshRenderer.BakeMesh(bakedMesh);
            meshFilter.mesh = bakedMesh;
            
            transform.position = prevVector;
            transform.rotation = prevRotation;

            prevVector = target.transform.position;
            prevRotation = target.transform.rotation;
            yield return delayWFS;
        }

    }
}
