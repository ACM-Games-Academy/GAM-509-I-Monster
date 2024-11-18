using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : Enemy
{
    public GameObject missilePrefab;
    public Transform launchPoint;
    public Transform aimingDummy;
    public Transform turretSwivel;
    public Transform cannonSwivel;
    public float cooldown;
    public Vector3 targetPosition;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        ChangeDestination();
    }

    // Update is called once per frame
    void Update()
    {
        aimingDummy.LookAt(Camera.main.transform);

        turretSwivel.transform.rotation = Quaternion.Euler(0, aimingDummy.transform.eulerAngles.y, 0);
        cannonSwivel.transform.localRotation = Quaternion.Euler(aimingDummy.transform.eulerAngles.x, 0, 0);
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 5;
            GameObject newMissile = Instantiate(missilePrefab, (launchPoint ? launchPoint: transform).position, (launchPoint ? launchPoint : transform).rotation);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChangeDestination();
        }
    }

    public void ChangeDestination()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), out hit, Mathf.Infinity, NavMesh.AllAreas);

        targetPosition = hit.position;
        NavMeshPath newPath = new NavMeshPath();
        agent.CalculatePath(hit.position, newPath);

        if (newPath.status == NavMeshPathStatus.PathComplete)
        {
            agent.path = newPath;
            agent.destination = hit.position;
            agent.SetDestination(hit.position);
        }
    }
}
