using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Perception : MonoBehaviour
{
    public Wander wander;

    // Camera
    public Camera frustum;
    LayerMask mask;

    // Zombies
    public NavMeshAgent agent;
    public GameObject zombiePrefab;
    public GameObject target;
    
    float detected;

    int intervalTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewHeading());
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                    if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        Seek();
                    }
            }
        }
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            target.transform.position = wander.wander();
            yield return new WaitForSeconds(intervalTime);
        }
    }

    void Seek()
    {
        agent.nextPosition = transform.position;
    }

}
