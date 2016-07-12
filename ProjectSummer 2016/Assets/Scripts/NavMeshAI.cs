using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAI : MonoBehaviour
{

    public string msg;
    public int distToTarget;
    public int distToUntarget;

    public float upAmnt;
    public float crsAmnt;

    public float speedIntro;
    public float speedAtk;
    public float speedIdl;
    public float timeToUntarget;
    public float timeToIdlMove;
    public float moveRadius;

    public Transform target;

    NavMeshAgent m_Agent;
    CapsuleCollider m_Capsule;

    GameObject m_Player;
    ClickToMove m_PlayerMoveScript;

    GameObject highlightCircle;
    Hover hoverScript;


    float m_AttackTimer = 0;
    float m_WaitTimer = 0;

    bool m_Active = false;
    bool m_Moving1 = false;
    bool m_Moving2 = false;
    bool m_Attacking = false;
    bool m_Idle = false;
    bool m_MouseOver = false;

    float m_LerpTimer = 0;

    Vector3 startPos;
    Vector3 endPos;

    // Use this for initialization
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>(); // nav mesh agent
        m_Capsule = GetComponent<CapsuleCollider>(); //collision

        m_Player = GameObject.FindGameObjectWithTag("Player"); // player
        m_PlayerMoveScript = m_Player.GetComponent<ClickToMove>(); // player move script

        highlightCircle = GameObject.FindGameObjectWithTag("Highlight");//highlight game object
        hoverScript = highlightCircle.GetComponent<Hover>();//highlight script

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Moving1)// first intro movement
        {
            float timeSinceStarted = Time.time - m_LerpTimer;//timer (time of game minus time when started)
            float percentageComplete = timeSinceStarted / speedIntro;//percent decimal

            this.transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);//lerping

            if (percentageComplete >= 1.0f)//when finished lerping
            {
                m_LerpTimer = Time.time;//reset timer start
                startPos = m_Agent.transform.position;//reset position
                endPos = m_Agent.transform.position;//reset position
                endPos.z += crsAmnt;//add finished position
                m_Moving1 = false;//done first movement
                m_Moving2 = true;//start second movement
                //m_Active = true;
            }

        }
        if(m_Moving2)
        {
            //msg = "next move";
            float timeSinceStarted = Time.time - m_LerpTimer;
            float percentageComplete = timeSinceStarted / speedIntro;

            this.transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                m_Moving2 = false;
                m_Capsule.enabled = true;
                m_Agent.enabled = true;
                m_Active = true;
                m_Idle = true;
                startPos = m_Agent.transform.position;
                endPos = startPos;
            }
        }

        if (m_Active)//once done intro movements start enemy AI 
        {

            if (m_Attacking)
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
                        m_Agent.speed = speedAtk;
                        msg = "Targeting Player, and Chasing";
                    }

               

                if (Vector3.Distance(m_Agent.transform.position, m_Player.transform.position) > distToUntarget) // if player is past untarget distance
                {
                    m_WaitTimer += Time.deltaTime;
                    target = null;
                    m_Agent.speed = 0;
                    if (m_WaitTimer >= timeToUntarget)//after wait timer go back to idle state
                    {
                        msg = "Player ran away, back to idle state";
                        m_Idle = true;
                        m_Attacking = false;
                        m_WaitTimer = 0;
                        startPos = m_Agent.transform.position;
                        endPos = startPos;
                    }
                    
                }

                if (target != null)
                {
                    m_Agent.SetDestination(target.position);
                }


            }//end if attacking

            if (m_Idle)
            {
                m_WaitTimer += Time.deltaTime;//start wait timer
                if (Vector3.Distance(m_Agent.transform.position, m_Player.transform.position) < distToTarget)//within target distance
                {
                    m_Attacking = true;
                    m_Idle = false;
                    m_WaitTimer = 0;
                }//end if player is within target distance    

                if (m_WaitTimer > timeToIdlMove)//if waited for amount of time
                {
                    m_WaitTimer = 0;
                    m_Agent.speed = speedIdl;
                    startPos = m_Agent.transform.position;
                    endPos = startPos;
                    endPos.x += Random.Range(0, 6) - 2;
                    endPos.z += Random.Range(0, 6) - 2;
                }

                m_Agent.SetDestination(endPos);
            }//end if idle
        }//end if active
        

    }//end update

    public void Activate()//will start the movement to activate enemy
    {
        m_Moving1 = true;
        m_LerpTimer = Time.time;
        startPos = transform.position;
        endPos = transform.position;
        endPos.y += upAmnt;

    }
    void OnMouseEnter()//mouse over
    {
        m_MouseOver = true;
        hoverScript.OnHover(this.gameObject);//hover script
    }
    void OnMouseExit()//mouse exit
    {
        m_MouseOver = false;
        hoverScript.OffHover();//hover script
    }

    void OnGUI()
    {
        if (m_MouseOver)
        {
            GUI.Box(new Rect(300, 10, 270, 50), "Enemy: " + name);
        }
    }
}
