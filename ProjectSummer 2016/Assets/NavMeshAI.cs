﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAI : MonoBehaviour
{

    public string msg;
    public int distToTarget;
    public int distToUntarget;

    public float upAmnt;
    public float crsAmnt;

    public float speed;

    public Transform target;

    NavMeshAgent m_Agent;
    GameObject m_Player;
    ClickToMove m_PlayerMoveScript;
    float m_AttackTimer = 0;

    bool m_Active = false;
    bool m_Moving = false;

    float m_LerpTimer = 0;

    Vector3 startPos;
    Vector3 endPos;

    // Use this for initialization
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerMoveScript = m_Player.GetComponent<ClickToMove>();

    }

    // Update is called once per frame
    void Update()
    {
        if(m_Active)
        {
            if (Vector3.Distance(m_Agent.transform.position, m_Player.transform.position) < distToTarget)
            {
                if (Vector3.Distance(m_Agent.transform.position, m_Player.transform.position) < 2)
                {
                    m_AttackTimer += Time.deltaTime;
                    if (m_AttackTimer > 2)
                    {
                        m_Agent.speed = 0;
                        //m_PlayerMoveScript.TakeDamage(2);
                        msg = "Attacking the player with melee attack";
                        m_AttackTimer = 0;

                    }//end if attack timer
                }//end if agent is <2 meters from player
                else
                {
                    target = m_Player.transform;
                    m_Agent.speed = 3.5f;
                    msg = "Targeting Player, and Chasing";
                }
            }//end if agent is within target distance

            if (Vector3.Distance(m_Agent.transform.position, m_Player.transform.position) > distToUntarget)
            {
                target = null;
                m_Agent.speed = 0;
                msg = "Player ran away, back to idle state";
            }

            if (target != null)
            {
                m_Agent.SetDestination(target.position);
            }
        }
        

    }

    void FixedUpdate()
    {
        if (m_Moving)
        {
            float timeSinceStarted = Time.time - m_LerpTimer;
            float percentageComplete = timeSinceStarted / speed;

            m_Agent.transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);
            
            if(percentageComplete >= 1.0f)
            {
                m_Moving = false;
                m_Active = true;
            }

        }


    }

    public void Activate()
    {
        m_Moving = true;
        m_LerpTimer = Time.time;
        startPos = transform.position;
        endPos = transform.position;
        endPos.y += upAmnt;

    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 270, 50), "Enemy: " + msg);
    }
}