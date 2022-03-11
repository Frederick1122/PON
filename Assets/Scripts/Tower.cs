using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("PrefabSettings")]
    [SerializeField] private bool isGreen; 
    [SerializeField] private int carma; 
    void Start()
    {
        StartCoroutine(TowerCoroutine());
    }
    IEnumerator TowerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            DataHolder.main.ChangeCarma(isGreen ? carma : -carma);
        }
    }

}
