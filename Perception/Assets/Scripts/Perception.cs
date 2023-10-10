using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Perception : MonoBehaviour
{
    // Camera
    public Camera frustum;
    LayerMask mask;

    // Zombies
    public GameObject zombiePrefab;
    public int numZombies;
    GameObject[] zombies;
    float detected;

    Vector3 target;
    float radius = 8;
    float offset = 10;
    int intervalTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewHeading());
    }

    // Update is called once per frame
    void Update()
    {
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
                        
                    }
            }
        }
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