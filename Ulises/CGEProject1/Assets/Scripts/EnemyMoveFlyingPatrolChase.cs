
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{

    public GameObject[] patrolPoints;

    public float speed = 2f;
    public float chaseRange = 3f;


    //enemy state enum-new type
    public enum EnemyState { PATROLLING, CHASING}


    public EnemyState currentState = EnemyState.PATROLLING;

    public GameObject target;

    private GameObject player;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    private int currentPatrolPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        //check if patrol points are assigned
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned!");
        }

        target = patrolPoints[currentPatrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //update state based on player and target distance
        UpdateState();

        //move and face based on current state
        switch (currentState)  //switch work good with enum
        {
            case EnemyState.PATROLLING:
                Patrol();
                break;
            case EnemyState.CHASING:
                ChasePlayer();
                break;
        }

        //can use debug.DrawLinge to draw line between two points in scene view
        Debug.DrawLine(transform.position, target.transform.position, Color.red);


    }


    void UpdateState()
    {
        if (IsPlayerInChaseRange() && currentState ==EnemyState.PATROLLING)
        {
            currentState = EnemyState.CHASING;
        }
        else if (!IsPlayerInChaseRange() && currentState == EnemyState.CHASING)
        {
            currentState = EnemyState.PATROLLING;
        }
    }

    bool IsPlayerInChaseRange()
    {
        if(player == null)
        {
            Debug.LogError("Player not found");
            return false;   
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= chaseRange;
    }


    void Patrol()
    {
        //check if reached current target
        if(Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            //update target to next control point (wrap around?)
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        target = patrolPoints[currentPatrolPointIndex];

        MoveTowardsTarget();

    }

    void ChasePlayer()
    {
        target = player;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        //calculate direction towards target
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        //move towards target with rb
        rb.velocity = direction * speed;

        //face forward
        FaceForward(direction);
    }

    private void FaceForward(Vector2 direction)
    {
        if (direction.x < 0)
        {
            sr.flipX = false;
        }
        else if (direction.x > 0)
        {
            sr.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints !=null)
        {
            Gizmos.color = Color.green;
            foreach (GameObject point in patrolPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}
