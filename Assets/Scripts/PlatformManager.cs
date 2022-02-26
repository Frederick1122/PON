using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager main;
    [SerializeField] List<GameObject> greenArea = new List<GameObject>();
    [SerializeField] List<GameObject> greyArea = new List<GameObject>();

    [SerializeField] private GameObject greenCoin;
    [SerializeField] private GameObject greyCoin;
    [SerializeField] private float coinSpawnTime;


    public int FPQuantityCoins;
    public int SPQuantityCoins;
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
    }
    public void RegionCapture(bool itsGreen)
    {
        if (itsGreen)
        {
            if (greyArea.Count > 1)
            {
                greenArea.Add(greyArea[0]);
                
                greyArea.RemoveAt(0);
            }
        }
        else
        {
            if (greenArea.Count > 1)
            {
                greyArea.Add(greenArea[0]);
                
                greenArea.RemoveAt(0);
            }
        }
        NewSpawnPositions();
    }

    public void NewSpawnPositions(bool flag = false)
    {
        DeathZone.main.EraseSpawns();
        foreach (GameObject i in greenArea)
        {
            DeathZone.main.SpawnsFirstPlayer.Add(i.GetComponent<RegionManager>().spawn);
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
            DeathZone.main.SpawnsSecondPlayer.Add(i.GetComponent<RegionManager>().spawn);
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
            if (FPQuantityCoins < 15)
            {
                FPQuantityCoins++;
                MeshCollider col = greenArea[Random.Range(0, greenArea.Count)].GetComponent<MeshCollider>();
                Vector3 position =
                    new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
                    0.1f, Random.Range(col.bounds.min.z, col.bounds.max.z));
                GameObject g = Instantiate(greenCoin, position, Quaternion.identity);
                g.GetComponent<CoinScript>().parentCollider = col;
            }
            if (SPQuantityCoins < 15)
            {
                SPQuantityCoins++;
                MeshCollider col = greyArea[Random.Range(0, greyArea.Count)].GetComponent<MeshCollider>();
                Vector3 position =
                    new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
                    0.1f, Random.Range(col.bounds.min.z, col.bounds.max.z));
                GameObject g = Instantiate(greyCoin, position, Quaternion.identity);
                g.GetComponent<CoinScript>().parentCollider = col;
            }
        }
    }
}