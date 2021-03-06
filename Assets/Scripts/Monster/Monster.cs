﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{    
    PlayerWalking player;

    Transform target;

    [SerializeField]
    LayerMask rayLayers;

    public float patrolSpeed { get; set; }
    public float huntSpeed { get; set; }

    [SerializeField, Range(0, 5)]
    float huntForPlayer = 3f;
    public void TeleportTo(Transform location)
    {
        NavMeshAgent agent = GetComponentInChildren<NavMeshAgent>();
        agent.enabled = false;
        transform.position = location.position;
        transform.rotation = location.rotation;
        agent.enabled = true;
    }

    public void Hunt(Transform target)
    {
        if (target == null)
        {
            Debug.Log("Monster hunts no one");
        }
        else
        {
            Debug.Log(string.Format("Monster hunts {0}", target.name));
        }
        this.target = target;
    }

    public bool IsTargeting(Transform target)
    {
        return this.target == target;
    }

    float lastSeen = 0f;

    [SerializeField, Range(0, 90)]
    float viewAngle = 45f;

    [SerializeField, Range(0, 10)]
    float angleStep = 2.5f;

    [SerializeField, Range(0, 20)]
    float sightDistance = 10f;

    Animator anim;


    bool CanSeePlayer()
    {
        if (Time.timeSinceLevelLoad - lastSeen < huntForPlayer)
        {
            return true;
        }
        for (float a=-viewAngle; a<=viewAngle; a+=angleStep)
        {
            Quaternion q = Quaternion.AngleAxis(a, transform.up);
            Vector3 dir =  q * transform.forward;
            Ray r = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, sightDistance, rayLayers))
            {
                if (hit.collider.tag == "Player")
                {
                    lastSeen = Time.timeSinceLevelLoad;
                    PlaySpeak();
                    return true;
                }
            }
        }
        return false;
    }

    public void ForgetPlayer()
    {
        lastSeen = Time.timeSinceLevelLoad - (huntForPlayer + 1);
    }

    float lastSpeak = 0f;
    float speakThreshold = 5f;

    void PlaySpeak()
    {

        if (captures.Length > 0 && Time.timeSinceLevelLoad - lastSpeak > speakThreshold)
        {
            AudioClip capture = captures[Random.Range(0, captures.Length)];
            speakers.PlayOneShot(capture);
            lastSpeak = Time.timeSinceLevelLoad;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color gColor = Gizmos.color;
        for (float a = -viewAngle; a <= viewAngle; a += angleStep)
        {
            Quaternion q = Quaternion.AngleAxis(a, transform.up);
            Vector3 dir = q * transform.forward;
            Ray r = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, sightDistance, rayLayers))
            {
                Gizmos.color = hit.collider.tag == "Player" ? Color.red : Color.gray;
                Gizmos.DrawLine(r.origin, hit.point);
            }
            else
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(r.origin, r.GetPoint(sightDistance));
            }
        }
        if (target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
        Gizmos.color = gColor;
    }

    AudioSource speakers;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerWalking>();
        speakers = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        NavMeshAgent agent = GetComponentInChildren<NavMeshAgent>();
        if (agent != null  && agent.enabled)
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

        anim.SetFloat("Speed", agent.speed);
    }

    [SerializeField]
    AudioClip[] captures;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NavMeshAgent agent = GetComponentInChildren<NavMeshAgent>();
            agent.enabled = false;
            player.SetKilled();
        }
    }
}
