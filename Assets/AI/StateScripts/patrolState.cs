using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrolState : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    float chaseRange = 5;
    Transform playerTransform;
    PlayerController pc;

    float patrollSpeed = 1f;
    float PatrollAccelaration = 0.25f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        pc = player.GetComponent<PlayerController>();
        agent = animator.GetComponent<NavMeshAgent>();

        timer = 0;

        agent.speed = patrollSpeed;
        agent.acceleration = PatrollAccelaration;

        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
        foreach(Transform t in go.transform)
        {
            wayPoints.Add(t);
        }

        Vector3 wayPoint = wayPoints[Random.Range(0, wayPoints.Count)].position;
        animator.transform.LookAt(wayPoint);
        agent.SetDestination(wayPoint);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 wayPoint = wayPoints[Random.Range(0, wayPoints.Count)].position;
            animator.transform.LookAt(wayPoint);
            agent.SetDestination(wayPoint);
        }

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("IsPatrolling", false);
        }

        float cR;

        if (pc.isSneaking)
        {
            cR = chaseRange * 0.3f;
        }
        else cR = chaseRange;

        float distance = Vector3.Distance(playerTransform.position, animator.transform.position);
        if (distance < cR)
        {
            animator.SetBool("IsChasing", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
