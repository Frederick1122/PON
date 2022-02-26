using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    public Transform spawn;
    private MeshRenderer meshRenderer;
    [SerializeField] private Material gray;
    [SerializeField] private Material green;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void SwitchMaterial(bool flag) // if true - green, false - grey
    {

        if(flag)
        {
            meshRenderer.material = green;
        } else
        {
            meshRenderer.material = gray;
        }
    }
}
