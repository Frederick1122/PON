using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("PlayerSettings")]
    public bool isGreen;
    [SerializeField] private float speed;
    [SerializeField] private float gravityForce;

    [Space]
    [Header("AttackPreset")]
    [SerializeField] private GameObject aim;
    public AttackEnergy attackEnergy;
    

    // private settings
    [NonSerialized]public TowerPlaceManager checkerTower;
    private Vector3 moveVector;
    private Vector3 moveVector2;
    private CharacterController characterController;
    private List<Ray> rays = new List<Ray>();
    Hashtable hashtableRays = new Hashtable();

    public enum PlayerCondition
    {
        moving,
        stay
    };
    [NonSerialized]public PlayerCondition playerCondition;


    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCondition = PlayerCondition.moving;
        moveVector = Vector3.right;
        moveVector2 = Vector3.forward;
        StartingRays();

    }
    private void FixedUpdate()
    {
        FindEnemyBorder();

        if (playerCondition == PlayerCondition.moving)
        {
            Gravity();
            MovePlayer();
        }
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && isGreen) || (Input.GetKeyDown(KeyCode.M) && !isGreen))
        {
            if (checkerTower != null)
            {
                checkerTower.OpenShop();
            }
            else
            {
                Action();
            }
        }

    }

    void AddNewRays(Vector3 direction)
    {
        rays.Add(new Ray(transform.position, direction));
    }
    private void MovePlayer()
    {
        var forward = moveVector; var right = moveVector2;
        forward = isGreen ? forward * Input.GetAxis("HorizontalWASD") * speed : forward * Input.GetAxis("HorizontalArrows") * speed;
        right = isGreen ? right * Input.GetAxis("VerticalWASD") * speed : right * Input.GetAxis("VerticalArrows") * speed;
        forward.y = gravityForce;
        characterController.Move((forward + right) * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            string tag = hit.collider.tag;
            if (tag == "GreyArea" && isGreen || tag == "GreenArea" && !isGreen)
            {
                var DD = DeathZone.main;
                if (checkerTower != null)
                {
                    checkerTower.GetComponent<TowerPlaceManager>().CloseShop(false);
                    GetComponent<PlayerScript>().checkerTower = null;
                }
                DD.Respawn(gameObject);
            }
        }
    }

    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            gravityForce -= 20f * Time.deltaTime;
        }
        else
        {
            gravityForce = -1f;
        }
    }
    public void ResetGravityForce()
    {
        gravityForce = -1f;
        Gravity();
    }

    void FindEnemyBorder()
    {
        ResetRays();
        for (int i = 0; i < rays.Count; i++)
        {
            Ray r = new Ray(transform.position, rays[i].direction);
            rays[i] = r;
            Debug.DrawRay(rays[i].origin, rays[i].direction);
            RaycastHit hit;
            if (Physics.Raycast(rays[i], out hit))
            {
                Vector3 v = hit.point;
                if ((isGreen && hit.collider.gameObject.CompareTag("GreyArea"))
            || (!isGreen && hit.collider.gameObject.CompareTag("GreenArea")))
                {
                    if (hashtableRays.ContainsKey(i))
                    {
                        hashtableRays.Remove(i);
                    }
                    hashtableRays.Add(i, v);
                }
                else if (((isGreen && hit.collider.gameObject.CompareTag("GreenArea"))
            || (!isGreen && hit.collider.gameObject.CompareTag("GreyArea"))))
                {
                    hashtableRays.Remove(i);
                }
            }
            else
            {
                hashtableRays.Remove(i);
            }

        }
        ICollection keys = hashtableRays.Keys;
        foreach (int i in keys)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction, color: Color.red);
        }
    }


    void Action()
    {
        if (playerCondition == PlayerCondition.stay 
            || hashtableRays.Count == 0 
            || attackEnergy.energy < 100) return;
        attackEnergy.energy = 0;
        ICollection keys = hashtableRays.Keys;
        foreach (int i in keys)
        {
            Ray r = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            Physics.Raycast(r, out hit);
            GameObject g = Instantiate(aim, hit.point, Quaternion.identity);
            g.GetComponent<AimRange>().player = this;

            playerCondition = PlayerCondition.stay;
            break;
        }
    }
    public void ResetRays()
    {
        hashtableRays.Clear();
    }
    public void StartingRays()
    {
        AddNewRays(new Vector3(0, -1, 2));
        AddNewRays(new Vector3(0, -1, -2));
        AddNewRays(new Vector3(-2, -1, 0));
        AddNewRays(new Vector3(2, -1, 0));
        AddNewRays(new Vector3(2, -1, 2));
        AddNewRays(new Vector3(-2, -1, 2));
        AddNewRays(new Vector3(2, -1, -2));
        AddNewRays(new Vector3(-2, -1, -2));
    }
}
