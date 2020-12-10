using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    // public variables
    public float chaseDistance;
    public float attackDistance;
    public float visionAngle;
    public int totalBullets;
    public float chargingSeconds;
    public float minPointDistance;
    public float shootSeconds;
    public GameObject bulletGO;
    public Material[] materials;
    public NavMeshAgent navMeshAgent;


    //private array of all the game objects it patrols and flees to
    private GameObject[] FleePoints;
    private GameObject[] patrollingPoints;
    private int bullets;

    //index of the point you want to start patrolling
    private int searchingPoint = 0;

    //meshrenderer, basically in charge of changing material or colour
    private MeshRenderer mr;
    private Animator animator;
    private GameObject player;

    private void Awake()
    {
        // will know the player
        player = GameObject.Find("Player");
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        //fills its ammunition
        ChargeBullets();
        //fills the array with points it uses as patrol points and flee points
        patrollingPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        FleePoints = GameObject.FindGameObjectsWithTag("FleePoint");
    }

    // Update is called once per frame
    void FixedUpdate () {

        //every fixedupdate we set the boolean values of the animator based on the following
        animator.SetBool("IsPlayerVisible", IsPlayerVisible());
        animator.SetBool("IsPlayerClose", IsPlayerClose());
        animator.SetBool("IsPlayerDetectable", IsPlayerDetectable());
        animator.SetBool("IsPlayerAttackable", IsPlayerAttackable());
        animator.SetBool("IsOnPoint", IsOnPoint());
        animator.SetBool("IsBulletsEmpty", isEmpty());
    }

    public bool IsPlayerDetectable() {
        if(IsPlayerVisible()) {
            if(IsPlayerInAngle()) {
                return IsPlayerClose();
            }
        }
        return false;
    }

    // if you can see the player, and the player is close enough, it returns true otherwise it returns false;
    public bool IsPlayerAttackable()
    {
        if (IsPlayerVisible())
        {
            return PlayerDistance() < attackDistance; 
        }
        return false;
    }

    //returns the distance of the player from this object
    public float PlayerDistance() {
		return Vector3.Magnitude (player.transform.position - transform.position);
	}

    //sets the current material of the object to the specified index
	public void SetMaterial(int index) {
		if (mr == null) {
			mr = GetComponent<MeshRenderer> ();
		}
		mr.material = materials [index];
	}

	public void SetNextPoint() {
        //picks a random number between the 0th index and the last index of the patrolling point array
		searchingPoint = Random.Range(0, patrollingPoints.GetLength(0));
        if(navMeshAgent == null) {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }
        //set the next patrol point to this value
        navMeshAgent.SetDestination(patrollingPoints[searchingPoint].transform.position);
        navMeshAgent.stoppingDistance = 0f;
	}

    public void SetPlayerPosition() {
        navMeshAgent.SetDestination(player.transform.position);
    }

    //returns the difference between this agents destination and its transform, and say how far apart it is
	public bool	IsOnPoint() {
		Vector3 diff = navMeshAgent.destination - transform.position;
		diff.y = 0f;
		return diff.magnitude <= minPointDistance;
	}

	public bool IsPlayerVisible() {
		RaycastHit hit;
        //gets the direction from this object to the player
		Vector3 direction = player.transform.position - transform.position;
        direction.y = 0; // make it horizontal
        direction = Vector3.Normalize(direction);

        // send a raycast out from this 
        if (Physics.Raycast (transform.position, direction, out hit, 100f)) {
            if (hit.transform.tag == "Player") {
                return true;
			}
		}
		return false;
	}


    //Debug to draw the lines
    public void ShowAngleLines() {
        Vector3 limit1 = Vector3.Slerp(transform.forward, transform.right, visionAngle / 180);
        Vector3 limit2 = Vector3.Slerp(transform.forward, -transform.right, visionAngle / 180);
        Debug.DrawRay(transform.position, limit1 * chaseDistance, Color.blue);
        Debug.DrawRay(transform.position, limit2 * chaseDistance, Color.blue);
    }

    public void ShowDestinationLine() {
        Debug.DrawRay(transform.position, navMeshAgent.destination - transform.position, Color.green);
    }
    //checks if the player is in the vision cone
    public bool IsPlayerInAngle(){
        return Vector3.Angle(transform.forward, player.transform.position - transform.position) <= visionAngle / 2f;
    }
    //checks if the player is close enough to chase
    public bool IsPlayerClose() {
        return PlayerDistance() < chaseDistance;
    }

    //loads the bullets
    public void ChargeBullets () {
        bullets = totalBullets;
    }

    //fires a bullet at the player
    public void Shoot() {
        transform.LookAt(player.transform);
        Instantiate(bulletGO, transform.position + transform.forward, Quaternion.identity);
        if (bullets > 0) {
            bullets--;
        }
    }

    //makes the navmesh stop at its current position
    public void Stop() {
        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.SetDestination(this.transform.position);
    }
    //checks if its out of bullets
    public bool isEmpty() {
        return bullets <= 0;
    }

    //picks the farthest point as its flee destination
    public void SetFarDestination() {
        NavMeshPath pathE = new NavMeshPath();
        NavMeshPath pathP = new NavMeshPath();
        float diff = 0;
        int iPath = -1;
        for (int i = 0; i < FleePoints.Length; i++) {
            NavMesh.CalculatePath(transform.position, FleePoints[i].transform.position, NavMesh.AllAreas, pathE);
            NavMesh.CalculatePath(player.transform.position, FleePoints[i].transform.position, NavMesh.AllAreas, pathP);
            float newDiff = PathLength(pathP) - PathLength(pathE);
            if (newDiff > diff) {
                diff = newDiff;
                iPath = i;
            }
        }
        if(iPath != -1) {
            NavMesh.CalculatePath(transform.position, FleePoints[iPath].transform.position, NavMesh.AllAreas, pathE);
            for (int i = 0; i < pathE.corners.Length - 1; i++) {
                Debug.DrawLine(pathE.corners[i], pathE.corners[i + 1], Color.red);
            }
            navMeshAgent.SetDestination(FleePoints[iPath].transform.position);
        }

        //Vector3 direction = Vector3.Normalize(transform.position - player.transform.position) * 5f;
        //navMeshAgent.SetDestination(transform.position + direction);
    }

    //rotates the object
    public void TurnAround() {
        transform.Rotate(new Vector3(0, 2f, 0));
    }
    //figure out what this does
    public float PathLength(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return 0;

        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length)
        {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;
    }

}
