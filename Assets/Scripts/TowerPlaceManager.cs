using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlaceManager : MonoBehaviour
{
    public bool isGreen;

    [Space]
    [Header("PrefabSettings")]
    [SerializeField] private GameObject[] shops;
    public MeshRenderer meshRenderer;
    public Material gray;
    public Material green;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.gameObject.GetComponent<PlayerScript>().checkerTower = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.gameObject.GetComponent<PlayerScript>().checkerTower = null;
            CloseShop();
        }
    }


    public void OpenShop()
    {
        if (isGreen)
        {
            shops[0].SetActive(true);
            shops[0].GetComponent<ShopScript>().position = gameObject.transform;
        }
        else
        {
            shops[1].SetActive(true);
            shops[1].GetComponent<ShopScript>().position = gameObject.transform;
        }
    }

    public void CloseShop(bool flag = true)
    {
        if (flag)
        {
            if (isGreen)
            {
                shops[0].SetActive(false);
                shops[0].GetComponent<ShopScript>().position = null;
            }
            else
            {
                shops[1].SetActive(false);
                shops[1].GetComponent<ShopScript>().position = null;
            }
        } else
        {
            if (!isGreen)
            {
                shops[0].SetActive(false);
                shops[0].GetComponent<ShopScript>().position = null;
            }
            else
            {
                shops[1].SetActive(false);
                shops[1].GetComponent<ShopScript>().position = null;
            }
        }
    }


}
