using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero; // don't know where to be looking on startup
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction that we want to go by getting input from the axis
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moved the character with speed
        characterController.SimpleMove(moveDirection * moveSpeed);

        //check if we're moving or not
        if (moveDirection == Vector3.zero)
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("DoubleChop");
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.Play("SpinAttack");
        }
    }

    //update for physics update, raycast is a physics object
    void FixedUpdate()
    {
        //create a hit point variable, where the raycast is actually hitting the layer mask
        RaycastHit hit;
        //creating a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //visualize the raycast
        Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

        //don't set any of the other collision triggers off
        //sets up the raycast, pass in the ray we created, out is to use it, layermask we're looking to intersect with, ignore any other trigger interactions
        if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {
                //set it to hit point if we're not already looking in that direction
                currentLookTarget = hit.point;
            }

            //perform the rotation here
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

        }
    }
}
