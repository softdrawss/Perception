using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float radius = 8;
    public float offset = 10;
    public NavMeshAgent agent;
    public int intervalTime = 1;
    Vector3 target;

    void Start()
    {
        StartCoroutine(NewHeading());
    }
    // Update is called once per frame
    public Vector3 wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitSphere * radius;
        localTarget += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(localTarget, out navHit, radius, -1);

        return navHit.position;
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            agent.SetDestination(wander());
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
