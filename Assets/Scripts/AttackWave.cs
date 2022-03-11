using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWave : MonoBehaviour
{
    [SerializeField] private GameObject attackItemPefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private Transform forwardTransform;
    public bool isGreen;
    [SerializeField] private Material grey;
    [SerializeField] private Material green;
    [Header("For debug")]
    [SerializeField] private bool flag;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GreenArea") || other.CompareTag("GreyArea"))
        {
            flag = true;
        }
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            DeathZone.main.Respawn(other.gameObject);
        }
        else if(other.CompareTag("Buildings"))
        {
            Destroy(other.gameObject);
        }
        
    }

    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        flag = false;
        if(isGreen)
        {
           mr.material = green;
        } else
        {
            mr.material = grey;
        }
        mr.enabled = false;
    }

    public void InstantiateNewObj()
    {
        if (!flag) return;
       
        GameObject g = Instantiate(attackItemPefab, transform.position + transform.right, transform.rotation);
        g.GetComponentInChildren<AttackWave>().isGreen = isGreen;
    }

    public void Delete()
    {
        Destroy(parent);
    }
}
