﻿using UnityEngine;
using System.Collections;


public class SpawnGroup : MonoBehaviour
{

    public GameObject[] m_Enemies;
    public float m_DetectRadius;
    public float m_SpawnSpeed;

    GameObject m_Player;
    NavMeshAI m_EnemieAIScript;
    float m_Timer = 0;
    float m_DistToPlayer = 0;
    int m_EnemyMax = 0;
    int m_EnemyCount = 0;

    bool m_InsideRadius = false;
    bool m_HasSpawned = false;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Enemies != null)
        {
            m_EnemieAIScript = m_Enemies[0].GetComponent<NavMeshAI>();
            m_EnemyMax = m_Enemies.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Enemies != null)
        {
            Vector3 temppos = m_Player.transform.position - this.transform.position;
            m_DistToPlayer = temppos.magnitude;

            if (m_DistToPlayer <= m_DetectRadius)// if magnitude is shorter than radius spawn 1 enemy at a time
            {
                m_InsideRadius = true;
            }


            if(m_InsideRadius)
            {
                if (m_HasSpawned == false)
                {
                    if (m_Timer == 0)
                    {
                        m_EnemieAIScript = m_Enemies[m_EnemyCount].GetComponent<NavMeshAI>();
                        m_EnemieAIScript.Activate();
                        m_EnemyCount++;
                        if (m_EnemyCount >= m_EnemyMax)//reached all enemies spawned
                        {
                            m_HasSpawned = true;
                        }
                    }
                    m_Timer += Time.deltaTime;

                    if(m_Timer >= m_SpawnSpeed)
                    {
                        m_Timer = 0;
                    }
                }
            }
        
        }

    }
}
