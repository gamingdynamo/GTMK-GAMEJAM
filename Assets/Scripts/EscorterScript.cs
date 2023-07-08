using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class EscorterScript : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    int waypointIndex = 0;
    public Transform player;
    [SerializeField] float minFollowDist = 5f;
    NavMeshAgent agent;

    [SerializeField]
    Animator animator;
    public float turnSpeed = 10.0f; 
    Rigidbody rb;
    Vector3 targetDirection;
    private Quaternion freeRotation;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (waypoints[waypointIndex] && (player.position - transform.position).magnitude < minFollowDist)
        {
            agent.SetDestination(waypoints[waypointIndex].transform.position);
            agent.speed = 15f;

        }

        else
        {
            agent.speed = 0f;
        }
        if ((waypoints[waypointIndex].position - transform.position).magnitude < agent.stoppingDistance)
        {
            agent.speed = 0f;
            waypointIndex++;
        }
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("Speed", agent.speed);
    }

    private void LateUpdate()
    {
        FaceDirection();

    }

    void FaceDirection()
    {
        
        targetDirection = transform.forward;

        if (rb.velocity != Vector3.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
           var euler = new Vector3(0, eulerY, 0);

           rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * Time.deltaTime);
        }
    }
}
