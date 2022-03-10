using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRange : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private bool direction; // if up - false, down - true

    [SerializeField] private Transform checkerTransform;
    [SerializeField] private MeshRenderer aimRenderer;

    [SerializeField] private float y;
    public bool isGreen;
    public PlayerScript player;
    void Rotating()
    {

        if (!direction)
        {
            float yy = (y + 90 > 360) ? y + 90 - 360 : y + 90;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, y + 90.0f, 0.0f), Time.deltaTime * speed);
            // Debug.Log($"{transform.rotation.eulerAngles.y} {yy}");
            if (transform.rotation.eulerAngles.y < yy + 5f && transform.rotation.eulerAngles.y > yy - 5.0f)
            {
                direction = true;
            }
        }
        else
        {
            float yy = (y - 90 < 0) ? y - 90 + 360 : y - 90;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, y - 90.0f, 0.0f), Time.deltaTime * speed);
            //Debug.Log($"{transform.rotation.eulerAngles.y} {yy}");
            if (transform.rotation.eulerAngles.y < yy + 5f && transform.rotation.eulerAngles.y > yy - 5.0f)
            {
                direction = false;
            }
        }
    }
    private void Start()
    {
        y = transform.rotation.eulerAngles.y;
        StartRotating();
    }
    private void Update()
    {
        Rotating();
        if ((Input.GetKeyDown(KeyCode.E) && isGreen) || (Input.GetKeyDown(KeyCode.M) && !isGreen))
        {
            Attack();
            player.action = false;
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
                if (tag == "GreyArea" && isGreen || tag == "GreenArea" && !isGreen)
                {
                    Debug.Log($"You're position - {gameObject.transform.eulerAngles}");
                    break;
                }
            }
            if (i == 350)
            {
                Debug.LogError("Start position not finding");
            }
        }

    }


    void Attack()
    {

    }
}
