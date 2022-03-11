using System;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    [Header("PrefabSettings")]
    public Transform spawnPos;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material gray;
    [SerializeField] private Material green;
    [SerializeField] private GameObject tower;

    [NonSerialized] public bool isGreen;
    private int flag = 0;

    public void SwitchMaterial(bool _flag) // if true - green, false - grey
    {
        var towerScript = tower.GetComponent<TowerPlaceManager>();
        isGreen = _flag;
        if (_flag && (flag == 0 || flag == 2))
        {
            flag = 1;
            //Debug.Log($"YES {gameObject.name}");
            meshRenderer.material = green;
            towerScript.meshRenderer.material = towerScript.green;
            towerScript.isGreen = true;
            foreach (Transform child in tower.transform)
            {
                Destroy(child.gameObject);
            }

        } else if (!_flag && (flag == 0 || flag == 1))
        {
            flag = 2;
            //Debug.Log($"NO {gameObject.name}");
            meshRenderer.material = gray;
            towerScript.meshRenderer.material = towerScript.gray;
            towerScript.isGreen = false;
            foreach (Transform child in tower.transform)
            {
                Destroy(child.gameObject);
            }
        }
       
    }
}
