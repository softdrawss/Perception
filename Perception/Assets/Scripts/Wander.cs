using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float radius =  8;
    public float offset = 10;
    public NavMeshAgent agent;
    public int intervalTime = 1;
    Vector3 target;

    void Start()
    {
        StartCoroutine(NewHeading());
    }
    // Update is called once per frame
    void Update()
    {
        agent.destination = target;
    }

    Vector3 wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
        return worldTarget;
        
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            target = wander();
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
