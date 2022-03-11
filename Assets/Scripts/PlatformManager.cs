using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager main;

    [Header("AreaSettings")]
    [SerializeField] private float coinSpawnTime;
    [Space]
    public List<GameObject> greenArea = new List<GameObject>();
    public List<GameObject> greyArea = new List<GameObject>();
    [SerializeField] private GameObject greenCoin;
    [SerializeField] private GameObject greyCoin;
    [Space]
    [SerializeField] private GameObject container;
    [NonSerialized]public int FPQuantityCoins;
    [NonSerialized]public int SPQuantityCoins;
    [NonSerialized]public int maxQuantityCoins;
    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(this);
            return;
        }
        main = this;
    }
    private void Start()
    {
        FPQuantityCoins = 0;
        SPQuantityCoins = 0;
        NewSpawnPositions(true);
        StartCoroutine(CoinSpawner());
        maxQuantityCoins = DataHolder.main.maxQuantityCoins;
    }
    public void RegionCapture(bool itsGreen)
    {
        if (itsGreen)
        {
            if (greyArea.Count > 1)
            {
                greenArea.Add(greyArea[0]);
                GameObject g = greenArea[0];
                for (int i = 0; i < greenArea.Count; i++)
                {
                    if (i == 0)
                    {
                        g = greenArea[i + 1];
                        greenArea[i + 1] = greenArea[i];
                    }
                    else if (i < greenArea.Count - 1)
                    {
                        GameObject gg = greenArea[i + 1];
                        greenArea[i + 1] = g;
                        g = gg;
                    }
                    else
                    {
                        greenArea[0] = g;
                    }
                }
                greyArea.RemoveAt(0);
            } else
            {
                DataHolder.main.Restart();
            }
        }
        else
        {
            if (greenArea.Count > 1)
            {
                greyArea.Add(greenArea[0]);
                GameObject g = greyArea[0];
                for (int i = 0; i< greyArea.Count; i++)
                {
                    if(i == 0)
                    {
                        g = greyArea[i + 1];
                        greyArea[i + 1] = greyArea[i];
                    }
                    else if(i < greyArea.Count-1)
                    {
                        GameObject gg = greyArea[i+1];
                        greyArea[i + 1] = g;
                        g = gg;
                    }
                    else
                    {
                        greyArea[0] = g;
                    }
                }
                greenArea.RemoveAt(0);
            }
            else
            {
                DataHolder.main.Restart();
            }
        }
        NewSpawnPositions();
    }

    public void NewSpawnPositions(bool flag = false)
    {
        DeathZone.main.EraseSpawns();
        foreach (GameObject i in greenArea)
        {
            DeathZone.main.SpawnsFirstPlayer.Add(i.GetComponent<RegionManager>().spawnPos);
            if (!i.CompareTag("GreenArea"))
            {
                if (flag)
                {
                    Debug.LogWarning("Some region have wrong tag. Check all regions");
                    flag = false;
                }
                i.tag = "GreenArea";
            }
            i.GetComponent<RegionManager>().SwitchMaterial(true);
           
        }
        foreach (GameObject i in greyArea)
        {
            DeathZone.main.SpawnsSecondPlayer.Add(i.GetComponent<RegionManager>().spawnPos);
            if (!i.CompareTag("GreyArea"))
            {
                if (flag)
                {
                    Debug.LogWarning("Some region have wrong tag. Check all regions");
                    flag = false;
                }
                i.tag = "GreyArea";
            }
            i.GetComponent<RegionManager>().SwitchMaterial(false);
            

        }
    }

    IEnumerator CoinSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(coinSpawnTime);
            if (FPQuantityCoins < maxQuantityCoins)
            {
                FPQuantityCoins++;
                MeshCollider col = greenArea[Random.Range(0, greenArea.Count)].GetComponent<MeshCollider>();
                Vector3 position =
                    new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
                    0.1f, Random.Range(col.bounds.min.z, col.bounds.max.z));
                GameObject g = Instantiate(greenCoin, position, Quaternion.identity);
                g.GetComponent<CoinScript>().parentCollider = col;
                g.GetComponent<CoinScript>().isGreen = true;
                g.transform.parent = container.transform;
            }
            if (SPQuantityCoins < maxQuantityCoins)
            {
                SPQuantityCoins++;
                MeshCollider col = greyArea[Random.Range(0, greyArea.Count)].GetComponent<MeshCollider>();
                Vector3 position =
                    new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
                    0.1f, Random.Range(col.bounds.min.z, col.bounds.max.z));
                GameObject g = Instantiate(greyCoin, position, Quaternion.identity);
                g.GetComponent<CoinScript>().parentCollider = col;
                g.GetComponent<CoinScript>().isGreen = false;
                g.transform.parent = container.transform;
            }
        }
    }
}
