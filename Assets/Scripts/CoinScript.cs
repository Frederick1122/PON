using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public MeshCollider parentCollider;
    public bool move;

    private MeshRenderer meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (!move)
        {
            switch (other.tag)
            {
                case "Border":
                    move = true;
                    meshRenderer.enabled = false;
                    StartCoroutine(StartingCoroutine());
                    break;
                case "Player1":
                    PlatformManager.main.FPQuantityCoins--;
                    DataHolder.main.TakeCoin(other.tag);
                    Destroy(gameObject);
                    break;
                case "Player2":
                    PlatformManager.main.SPQuantityCoins--;
                    DataHolder.main.TakeCoin(other.tag);
                    Destroy(gameObject);
                    break;
            }
        }
        else if (other == parentCollider)
        {
            move = false;
        }
    }
    void Start()
    {
        move = true;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        StartCoroutine(StartingCoroutine());

    }
    IEnumerator StartingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (move)
            {
                Vector3 position = new Vector3(
                    Random.Range(parentCollider.bounds.min.x, parentCollider.bounds.max.x),
                0.1f, Random.Range(parentCollider.bounds.min.z, parentCollider.bounds.max.z));
                transform.position = position;
            }
            else
            {
                meshRenderer.enabled = true;
                break;
            }
        }
    }
}
