using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscorterMovementScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float minFollowDistance = 5.5f;
    private Rigidbody rigidbody_e;
    public float followSpeed = 600f;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody_e = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(player.transform.position, rigidbody_e.position);
        if (dist > minFollowDistance)
        {
            rigidbody_e.velocity = (player.transform.position - rigidbody_e.position).normalized * followSpeed * Time.deltaTime;
        }
        else
        {
            rigidbody_e.velocity = Vector3.zero;
        }
    }

}
