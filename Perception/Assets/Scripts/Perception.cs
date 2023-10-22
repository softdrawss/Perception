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
    public LayerMask mask;
    RaycastHit hit;

    // Zombies
    public NavMeshAgent agent;
    public GameObject zombiePrefab;

    // Target
    GameObject target;
    Vector3 targetVec;

    bool detected = false;

    int intervalTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewHeading());
    }

    IEnumerator NewHeading()
    {
        while (enabled)
        {
            while (!detected)
            {
                if (Search())
                {
                    transform.parent.BroadcastMessage("Detected", hit.collider.gameObject);
                }
                targetVec = wander.wander();
                agent.destination = targetVec;
                yield return new WaitForSeconds(intervalTime);
            }
     
            Seek();
            yield return null;
        }
        
    }

    void Detected(GameObject pos)
    {
        print("Found");
        detected = true;
        target = pos;
    }

    void Seek()
    {
        print("Search the human!");
        agent.destination = target.transform.position;
    }

    bool Search()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {

                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
