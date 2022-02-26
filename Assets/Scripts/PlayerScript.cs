using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private bool WASDOrNot;
    [SerializeField] private float speed;
    [SerializeField] private Animator characterAnimator;

    private Vector3 moveVector;
    private Vector3 moveVector2;
    [SerializeField] private float gravityForce;
    private CharacterController characterController;
    private bool action;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveVector = Vector3.right;
        moveVector2 = Vector3.forward;
    }
    private void FixedUpdate()
    {
        Action();
        if (!action)
        {
            Gravity();
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        var forward = moveVector; var right = moveVector2;
        forward = WASDOrNot ? forward * Input.GetAxis("HorizontalWASD") * speed : forward * Input.GetAxis("HorizontalArrows") * speed;
        right = WASDOrNot ? right * Input.GetAxis("VerticalWASD") * speed : right * Input.GetAxis("VerticalArrows") * speed;
        /*if ((moveVector + moveVector2) != Vector3.zero)
        {
            characterAnimator.SetBool("Walking", true);
        }
        else
        {
            characterAnimator.SetBool("Walking", false);
        }*/
        forward.y = gravityForce;
        characterController.Move((forward + right) * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            string tag = hit.collider.tag;
            if (tag == "GreyArea" && WASDOrNot || tag == "GreenArea" && !WASDOrNot)
            {
                var DD = DeathZone.main;
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

    void Action()
    {

    }
}
