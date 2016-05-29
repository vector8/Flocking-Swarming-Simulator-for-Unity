using UnityEngine;
using System.Collections.Generic;

public class Flock : MonoBehaviour
{
    private class FlockMember
    {
        public Vector3 velocity;
        public GameObject instance;
        public int id;
    }

    public int numberOfMembers;
    public GameObject memberPrefab;
    public Transform followTarget;

    [Header("Parameters")]
    public float pullFactor;
    public float separationFactor;
    public float swarmFactor;
    public float inertiaFactor;
    public float neighbourDistance;
    public float maxVelocity;

    private List<FlockMember> flock;

    const float CORRECTION_ACCELERATION = 2f;

    // Use this for initialization
    void Start()
    {
        flock = new List<FlockMember>();
        for (int i = 0; i < numberOfMembers; i++)
        {
            GameObject go = Instantiate(memberPrefab);
            go.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            go.transform.SetParent(transform);
            FlockMember m = new FlockMember();
            m.instance = go;
            m.velocity = new Vector3();
            m.id = i;
            flock.Add(m);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(flock.Count != numberOfMembers)
        {
            // add new members if less than max
            for (int i = flock.Count; i < numberOfMembers; i++)
            {
                GameObject go = Instantiate(memberPrefab);
                go.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                go.transform.SetParent(transform);
                FlockMember m = new FlockMember();
                m.instance = go;
                m.velocity = new Vector3();
                m.id = i;
                flock.Add(m);
            }

            // delete members if greater than max
            while(flock.Count > numberOfMembers)
            {
                Destroy(flock[flock.Count - 1].instance);
                flock.RemoveAt(flock.Count - 1);
            }
        }

        Vector3 centre = new Vector3(), inertia = new Vector3();
        Vector3 alignment = new Vector3();
        foreach(FlockMember m in flock)
        {
            centre += m.instance.transform.position;
            inertia += m.velocity;
        }

        centre /= numberOfMembers;
        inertia /= numberOfMembers;
        alignment = inertia * inertiaFactor;

        foreach(FlockMember m in flock)
        {
            Vector3 pull = new Vector3(), separation = new Vector3(), swarmForce = new Vector3();

            Vector3 localCentre = new Vector3();
            localCentre += m.instance.transform.position;
            int numNeighbours = 1;

            foreach (FlockMember m2 in flock)
            {
                if(m2.id != m.id && (Vector3.Distance(m2.instance.transform.position, m.instance.transform.position) < neighbourDistance))
                {
                    localCentre += m2.instance.transform.position;
                    numNeighbours++;
                }
            }

            localCentre /= numNeighbours;

            if(followTarget != null)
            {
                swarmForce = (followTarget.transform.position - m.instance.transform.position) * swarmFactor;
            }
            pull = (centre - m.instance.transform.position) * pullFactor;
            separation = (m.instance.transform.position - localCentre) * separationFactor;
            Vector3 acceleration = alignment + pull + separation + swarmForce;

            //if(followTarget != null)
            //{
            //    Vector3 targetCentrePosition = followTarget.position;

            //    // Wander limits
            //    if (m.instance.transform.position.x < targetCentrePosition.x - wanderDistance)
            //    {
            //        acceleration.x = CORRECTION_ACCELERATION;
            //    }
            //    else if (m.instance.transform.position.x > targetCentrePosition.x + wanderDistance)
            //    {
            //        acceleration.x = -CORRECTION_ACCELERATION;
            //    }
            //    if (m.instance.transform.position.y < targetCentrePosition.y - wanderDistance)
            //    {
            //        acceleration.y = CORRECTION_ACCELERATION;
            //    }
            //    else if (m.instance.transform.position.y > targetCentrePosition.y + wanderDistance)
            //    {
            //        acceleration.y = -CORRECTION_ACCELERATION;
            //    }
            //    if (m.instance.transform.position.z < targetCentrePosition.z - wanderDistance)
            //    {
            //        acceleration.z = CORRECTION_ACCELERATION;
            //    }
            //    else if (m.instance.transform.position.z > targetCentrePosition.z + wanderDistance)
            //    {
            //        acceleration.z = -CORRECTION_ACCELERATION;
            //    }
            //}

            m.velocity += acceleration * Time.deltaTime;

            m.velocity = Vector3.ClampMagnitude(m.velocity, maxVelocity);

            m.instance.transform.position += m.velocity * Time.deltaTime;

            // turn the boid toward their velocity
            m.instance.transform.LookAt(m.instance.transform.position + Vector3.Normalize(m.velocity));
        }
    }
}
