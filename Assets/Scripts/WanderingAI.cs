using UnityEngine;
using UnityEngine.AI;
using System.Collections;
 
public class WanderingAI : MonoBehaviour {
 
    public float wanderRadius;
    public float wanderTimer;
    public GameObject lightTarget;

    private LightUp lightUp;
    private Vector3 lightPosition;
    private NavMeshAgent agent;
    private Animator animator;
    private float timer;
 
    // Use this for initialization
    void OnEnable () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = wanderTimer;
        lightUp = lightTarget.GetComponent<LightUp>();
        lightPosition = lightTarget.transform.position;
        lightPosition.y = transform.position.y;
    }
 
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
 
        if (lightUp.intensity > 0.5f) {
            animator.SetBool("moving", true);
            // NavMeshHit navHit;
            // NavMesh.SamplePosition(lightPosition, out navHit, wanderRadius, -1);
            agent.SetDestination(lightPosition);
        } else if (timer >= wanderTimer) {
            animator.SetBool("moving", true);
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        } else if (agent.remainingDistance < 0.01f) {
            animator.SetBool("moving", false);
        }
    }
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}