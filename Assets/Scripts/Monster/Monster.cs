using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Transform target;

    [SerializeField]
    LayerMask rayLayers;

    public float patrolSpeed { get; set; }
    public float huntSpeed { get; set; }

    [SerializeField, Range(0, 5)]
    float huntForPlayer = 3f;
    public void TeleportTo(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;
    }

    public void Hunt(Transform target)
    {
        this.target = target;
    }

    float lastSeen = 0f;
    bool CanSeePlayer()
    {        
        for (float a=-45f; a<=45f; a+=5f)
        {
            Quaternion q = Quaternion.AngleAxis(a, transform.up);
            Vector3 dir =  q * transform.forward;
            Ray r = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 10f, rayLayers))
            {
                if (hit.collider.tag == "Player")
                {
                    lastSeen = Time.timeSinceLevelLoad;
                    return true;
                }
            }
        }
        return Time.timeSinceLevelLoad - lastSeen < huntForPlayer;
    }

    private void Update()
    {
        NavMeshAgent agent = GetComponentInChildren<NavMeshAgent>();
        if (agent != null)
        {
            if (CanSeePlayer())
            {
                agent.destination = player.transform.position;
                agent.speed = huntSpeed;
            } else if (target == null)
            {
                agent.destination = transform.position;
                agent.speed = patrolSpeed;
            } else
            {
                agent.destination = target.transform.position;
                agent.speed = patrolSpeed;
            }
        }
    }
}
