using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{


    [Header("For debug")]
    [SerializeField] private bool debuging;
    [SerializeField] private GameObject debugSphere;
    [SerializeField] private Vector3 attackPos;
    [SerializeField] private bool isGreen;
    public int quantityColliders;

    private bool flag;
    private void Start()
    {
        flag = true;
        quantityColliders = 0;
        isGreen = gameObject.GetComponentInParent<PlayerScript>().isGreen;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name} view");
        if ((isGreen && collision.gameObject.CompareTag("GreyArea"))
            || (!isGreen && collision.gameObject.CompareTag("GreenArea")))
        {
            Vector3 collisionPoint = collision.contacts[0].normal;
            attackPos = collisionPoint;
            quantityColliders++;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name} not view");
        if ((isGreen && collision.gameObject.CompareTag("GreyArea"))
            || (!isGreen && collision.gameObject.CompareTag("GreenArea")))
        {
            if (quantityColliders == 1)
            {
                attackPos = Vector3.zero;
            }
            quantityColliders--;
        }
    }

    private void Update()
    {
        if (!debuging) return;

        if (quantityColliders > 0)
        {
            if (!debugSphere.activeSelf) debugSphere.SetActive(true);
            if (flag)
            {
                flag = false;
                GameObject g = Instantiate(debugSphere, attackPos, Quaternion.identity);
                debugSphere = g;
            }
            else
            {
                debugSphere.transform.position = attackPos;
            }
        }
        else
        {
            if (debugSphere.activeSelf) debugSphere.SetActive(false);
        }
    }
}
