using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//Aswad Mirza, 991445135 Exercise 4, edited for assignment 2
//Based On Code from example 4 in week 9, and maze example from week 11
public class CivillianAi : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public NavMeshAgent nmAgent;
    public float distanceFromTarget;
    public int patrolTargetIndex = 0;

    public float fleeRange = 8;
    public float attackRange = 4;
    public float fireTimer = 10;
    public float reloadTime = 3;

    public int ammoCap = 5;
    public GameObject bullet;

    //public Transform[] patrolTargets;
    //public Transform[] fleeTargets;

    public GameObject[] patrolTargets;
    public GameObject[] fleeTargets;

    //private GameObject wayPoints;

    public GameObject player;
    public float speed = 0.1f;

    public Vector3 bulletForce = new Vector3(0,100,100);

    public Transform PatrolTarget { get; set; }
    public bool OnTarget
    {
        get
        {
            if (animator) {
                return animator.GetBool("onTarget");
            } 
            animator = GetComponent<Animator>();
            return animator.GetBool("onTarget");
        }
        set
        {
            if (animator) {
                animator.SetBool("onTarget", value);
            } 
            animator = GetComponent<Animator>();
            animator.SetBool("onTarget", value);
        }
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();

        patrolTargets = GameObject.FindGameObjectsWithTag("AIPoint");
        fleeTargets = GameObject.FindGameObjectsWithTag("AIPoint");
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // if you want to make this have more options, make it pick a random point in the array and have it go there
    /*
    public void GetNextPatrolTarget()
    {
        switch (patrolTargetIndex)
        {
            case 0:
                patrolTargetIndex = 1;
                break;
            case 1:
                patrolTargetIndex = 0;
                break;
        }
        PatrolTarget = patrolTargets[patrolTargetIndex];
        nmAgent.SetDestination(PatrolTarget.position);
        OnTarget = false;
    }
    */

    public void GetNextPatrolTarget()
    {

        patrolTargetIndex = Random.Range(0, patrolTargets.Length);

        PatrolTarget = patrolTargets[patrolTargetIndex].transform;
        nmAgent.SetDestination(PatrolTarget.position);
        OnTarget = false;
    }
    public bool IsPlayerVisible()
    {
        RaycastHit hit;

        Vector3 vecToPlayer = (player.transform.position + Vector3.up) - transform.position;

        // int layerMask = 1 << 9;
        //makes this object only interested in the player mask
        int layerMask = LayerMask.GetMask("Player");
        layerMask = ~layerMask;
        Debug.DrawRay(transform.position, vecToPlayer, Color.blue);

        bool inSight = !Physics.Raycast(
            transform.position,
            vecToPlayer.normalized, out hit,
            Vector3.Distance(transform.position, player.transform.position + Vector3.up),
                             layerMask);

        if (inSight)
        {
            return true;
        }
        return false;
    }

    public void Chase()
    {
        nmAgent.SetDestination(player.transform.position);
    }

    public bool InAttackRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) < attackRange;
    }

    public float GetFireTimer()
    {
        return fireTimer;
    }

    /*
    public void SetColor(Color color)
    {
        transform.GetChild(0).GetComponent<Renderer>().materials[0].color = color;
    }
    */
    public void Fire()
    {
        GameObject bulletObject= Instantiate(bullet, transform.position + transform.forward, transform.rotation);
        bulletObject.GetComponent<Rigidbody>().AddRelativeForce(bulletForce);
    }

    public void Reload()
    {
        animator.SetInteger("ammo", ammoCap);
    }

    //returns if we are far enough from the player
    public bool Fled()
    {
        return Vector3.Distance(player.transform.position, transform.position) > fleeRange;
    }

    //Makes the object run away

    /*
    public void Flee()
    {
        //goes through the list of flee targets, and compares them, and picks the farthest point from its current position, and make it run to that
        nmAgent.SetDestination(
            fleeTargets.Aggregate((i, j) =>
                Vector3.Distance(transform.position, i.position) > Vector3.Distance(transform.position, j.position)
                    ? i : j).position);
    }
    */

    public void Flee()
    {
        //goes through the list of flee targets, and compares them, and picks the farthest point from its current position, and make it run to that
        nmAgent.SetDestination(
            fleeTargets.Aggregate((i, j) =>
                Vector3.Distance(transform.position, i.transform.position) > Vector3.Distance(transform.position, j.transform.position)
                    ? i : j).transform.position);
    }
    public void ClearNav()
    {
        nmAgent.ResetPath();
    }

    public void MoveToTarget()
    {
        nmAgent.SetDestination(PatrolTarget.position);

    }
}
