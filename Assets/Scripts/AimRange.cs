using System;
using System.Collections;
using UnityEngine;

public class AimRange : MonoBehaviour
{

    [Header("AimSettings")]
    [SerializeField] private float speed;
    [Space]
    [Header("PrefabSettings")]
    [SerializeField] GameObject attackItemPrefab;
    [SerializeField] private Transform checkerTransform;
    [SerializeField] private MeshRenderer aimRenderer;

    [Space]
    [Header("For debug")]
    [SerializeField] private bool direction; // if up - false, down - true
    [SerializeField] private bool waitCoroutineWorked; 
    [SerializeField] private float rotationY;

    // private settings
    [NonSerialized] public PlayerScript player;
    [SerializeField]private bool isGreen;

    void Rotating()
    {
        
        Ray ray = new Ray(checkerTransform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        //Debug.Log($"{hit.collider.name} {hit.collider.GetComponent<RegionManager>().isGreen != isGreen}");
        if (!direction)
        {
            float yy = (rotationY + 90 > 360) ? rotationY + 90 - 360 : rotationY + 90;
            if(yy == transform.rotation.eulerAngles.y)
            {
                rotationY = yy;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, rotationY + 90.0f, 0.0f), Time.deltaTime * speed);
                    }
        else
        {
            float yy = (rotationY - 90 < 0) ? rotationY - 90 + 360 : rotationY - 90;
            if (yy == transform.rotation.eulerAngles.y)
            {
                rotationY = yy;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, rotationY - 90.0f, 0.0f), Time.deltaTime * speed);
            
        }
        if (waitCoroutineWorked) return;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<RegionManager>().isGreen != isGreen)
            {
                StartCoroutine(WaitCoroutine());
            }
        }
    }
    private void Start()
    {
        waitCoroutineWorked = false;
        isGreen = !player.isGreen;
        rotationY = transform.rotation.eulerAngles.y;
        aimRenderer.enabled = false;
        StartRotating();
    }
    private void Update()
    {
        Rotating();
        if ((Input.GetKeyDown(KeyCode.E) && !isGreen) || (Input.GetKeyDown(KeyCode.M) && isGreen))
        {
            Attack();
            player.playerCondition = PlayerScript.PlayerCondition.moving;
            Destroy(gameObject);
        }
        Debug.DrawRay(checkerTransform.position, Vector3.down);
    }

    void StartRotating()
    {
        RaycastHit hit;
        for (int i = 0; i < 360; i += 10)
        {
            gameObject.transform.eulerAngles = new Vector3(0, i, 0);
            if (Physics.Raycast(checkerTransform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                string tag = hit.collider.tag;
                Debug.Log($"{hit.collider.name}");
                if (tag == "GreyArea" && !isGreen || tag == "GreenArea" && isGreen)
                {
                    Debug.Log($"You're position - {gameObject.transform.eulerAngles}");
                    aimRenderer.enabled = true;
                    break;
                }
            }
            if (i == 350)
            {
                Debug.LogError("Start position not finding");
            }
        }

    }
    IEnumerator WaitCoroutine()
    {
        waitCoroutineWorked = true;
        direction = !direction;
        Debug.Log(direction);
        yield return new WaitForSeconds(0.5f);
        waitCoroutineWorked = false;
    }
    void Attack()
    {
        GameObject g = Instantiate(attackItemPrefab, checkerTransform.position, transform.rotation);
        g.GetComponentInChildren<AttackWave>().isGreen = !isGreen;
    }
}
