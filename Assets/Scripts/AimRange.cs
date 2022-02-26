using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRange : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool direction; // if up - false, down - true

    [SerializeField] private float y;

    private void Start()
    {
        y = transform.rotation.eulerAngles.y;
    }
    private void Update()
    {
        Rotating();
    }

    void Rotating()
    {

        if (!direction)
        {
            float yy = (y + 90 > 360) ? y + 90 - 360 : y + 90;  
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, y + 90.0f, 0.0f), Time.deltaTime * speed);
            Debug.Log($"{transform.rotation.eulerAngles.y} {yy}");
            if (transform.rotation.eulerAngles.y < yy + 5f && transform.rotation.eulerAngles.y > yy - 5.0f)
            {
                direction = true;
            }
        }
        else
        {
            float yy = (y - 90 < 0) ? y - 90 + 360 : y - 90;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, y - 90.0f, 0.0f), Time.deltaTime * speed);
            Debug.Log($"{transform.rotation.eulerAngles.y} {yy}");
            if (transform.rotation.eulerAngles.y < yy + 5f && transform.rotation.eulerAngles.y > yy - 5.0f)
            {
                direction = false;
            }
        }
    }
}
