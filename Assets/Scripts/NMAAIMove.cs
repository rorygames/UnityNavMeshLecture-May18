using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMAAIMove : MonoBehaviour {

	NavMeshAgent m_agent;

    bool m_pathComplete = true;

    float m_waitTime = 0f;

    float m_zeroTime = 0f;

    [SerializeField]
    float m_mag = 0f;

	void Awake()
	{
		m_agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
        transform.position = GetNewPosition();
        StartCoroutine(PathCheck());
	}

    IEnumerator PathCheck()
    {
        yield return null;
        for(; ;)
        {
            if (m_pathComplete)
            {
                m_waitTime -= Time.deltaTime;
                if (m_waitTime <= 0)
                {
                    m_pathComplete = false;
                    m_agent.SetDestination(GetNewPosition());
                }
            }
            if (!m_agent.pathPending && !m_pathComplete)
            {
                m_mag = m_agent.velocity.magnitude;
                if (m_agent.remainingDistance <= m_agent.stoppingDistance)
                {
                    if (!m_agent.hasPath)
                    {
                        m_pathComplete = true;
                        m_waitTime = Random.Range(1f, 5f);
                    }
                }
            }
            yield return null;
        }
        
    }

    Vector3 GetNewPosition()
    {
        bool found = false;

        int aboveBridge = Random.Range(0, 10);

        if(aboveBridge == 0)
        {
            aboveBridge = 4;
        }
        else
        {
            aboveBridge = 1;
        }

        RaycastHit hit;
        NavMeshHit nmhit;
        Vector3 result = Vector3.zero;
        while (!found)
        {
            float randX = Random.Range(-7f, 7f);
            float randZ = Random.Range(-7f, 7f);
            if (Physics.Raycast(new Vector3(randX, aboveBridge, randZ), Vector3.down, out hit, 3f))
            {
                if(NavMesh.SamplePosition(hit.point, out nmhit, 1.0f,NavMesh.AllAreas))
                {
                    result = nmhit.position;
                    found = true;
                }
            }
        }
        return result;
    }
}
