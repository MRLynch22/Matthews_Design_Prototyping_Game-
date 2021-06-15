using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running")]
    [SerializeField] private float _wallRunUpForce, _wallRunPushForce;
    //<<Summary>> Boolean that is used for adding forces when jumping off the walls, used to determine which direction.
    private bool isRightWall, isLeftWall, isOffWall;

    //Used for effects etc.
    public static bool isWallRunning;
    //<<Summary>> Checks the distance from walls and takes the wall that is the closest to the player
    private float distanceFromLeftWall, distanceFromRightWall, distanceFromFloor;

    //Used to add forces.
    private Rigidbody rb;
    public Transform cameraPosition, head;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void wallChecker()
    {
        RaycastHit rightRaycast;
        RaycastHit leftRaycast;
        RaycastHit downRaycast;

        if (Physics.Raycast(head.transform.position, head.transform.right, out downRaycast))
        {
            distanceFromRightWall = Vector3.Distance(head.transform.position, downRaycast.point);
            if (distanceFromFloor <= 3f)
            {
                isRightWall = false;
                isLeftWall = false;
                isOffWall = true;
            }

        }

            if (Physics.Raycast(head.transform.position, head.transform.right, out rightRaycast))
            {
                distanceFromRightWall = Vector3.Distance(head.transform.position, rightRaycast.point);
                if (distanceFromRightWall <= 3f)
                {
                    isRightWall = true;
                    isLeftWall = false;
                    isOffWall = false;
                }
            }
            if (Physics.Raycast(head.transform.position, -head.transform.right, out leftRaycast))
            {
                distanceFromLeftWall = Vector3.Distance(head.transform.position, leftRaycast.point);
                if (distanceFromLeftWall <= 3f)
                {
                    isRightWall = false;
                    isLeftWall = true;
                    isOffWall = false;
                }
            }
    }

    private void Update()
    {
        wallChecker();
    }

    private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("RunnableWall"))
            {
                isWallRunning = true;
                rb.useGravity = false;

                if (isLeftWall)
                {
                    cameraPosition.transform.localEulerAngles = new Vector3(0f, 0f, -10f);
                }

                if (isRightWall)
                {
                    cameraPosition.transform.localEulerAngles = new Vector3(0f, 0f, 10f);
                }

                if (isOffWall)
                {
                    cameraPosition.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {

            if (collision.transform.CompareTag("RunnableWall"))
            {
                if (Input.GetKey(KeyCode.Space) && isLeftWall)
                {
                    rb.AddForce(Vector3.up * _wallRunUpForce, ForceMode.Impulse);
                    rb.AddForce(head.transform.right * _wallRunUpForce, ForceMode.Impulse);
                }

                if (Input.GetKey(KeyCode.Space) && isRightWall)
                {
                    rb.AddForce(Vector3.up * _wallRunUpForce, ForceMode.Impulse);
                    rb.AddForce(-head.transform.right * _wallRunUpForce, ForceMode.Impulse);
                }
                
                if (Input.GetKey(KeyCode.Space) && isOffWall)
                {
                    rb.AddForce(Vector3.up * _wallRunUpForce, ForceMode.Force);
                    rb.AddForce(-head.transform.right * _wallRunUpForce, ForceMode.Force);
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {

            if (collision.transform.CompareTag("RunnableWall"))
            {
                cameraPosition.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                isWallRunning = false;
                rb.useGravity = true;
            }
        }
    }