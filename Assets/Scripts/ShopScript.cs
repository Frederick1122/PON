using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private bool isGreen;
    public Transform position;
    [SerializeField] private GameObject[] greenObjects;
    [SerializeField] private GameObject[] greyObjects;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) && isGreen) || (Input.GetKeyDown(KeyCode.Keypad1) && !isGreen))
        {
            CreateGameobject(0);
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha2) && isGreen) || (Input.GetKeyDown(KeyCode.Keypad2) && !isGreen))
        {
            CreateGameobject(1);
        }
    }

    private void CreateGameobject(int index)
    {

        if (isGreen)
        {
            //Debug.Log($"YES {gameObject.name}");
            if (DataHolder.main.coins[0] < 20 * (index + 1)) return;
            else
            {
                DataHolder.main.coins[0] -= 20 * (index + 1);
            }
            foreach (Transform child in position)
            {
                Destroy(child.gameObject);
            }
            GameObject g = Instantiate(greenObjects[index], position.position + Vector3.up * 2, Quaternion.identity);
            g.transform.parent = position;
            g.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f)));
            
        }
        else
        {
            //Debug.Log($"NO {gameObject.name}");
            if (DataHolder.main.coins[1] < 20 * (index + 1)) return;
            else
            {
                DataHolder.main.coins[1] -= 20 * (index + 1);

            }
            foreach (Transform child in position)
            {
                Destroy(child.gameObject);
            }
            GameObject g = Instantiate(greyObjects[index], position.position + Vector3.up * 2, Quaternion.identity);
            g.transform.parent = position;
            g.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f)));
            
        }
        DataHolder.main.RefreshScore();
        gameObject.SetActive(false);
    }
}
