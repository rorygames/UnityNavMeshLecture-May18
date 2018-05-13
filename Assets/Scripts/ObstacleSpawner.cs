using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleSpawner : MonoBehaviour {

    public enum ObjectSpawnType
    {
        modifiers,
        obstacles,
        both
    }

    [SerializeField]
    ObjectSpawnType m_spawnType;

	[SerializeField]
	int m_maxObjects = 10;

    [SerializeField]
    Transform m_objectParent;

    [SerializeField]
    bool m_autoRebuild = true;

    DynamicNavMesh m_dnm;

    [SerializeField]
	List<GameObject> m_modifiers = new List<GameObject>();

    [SerializeField]
    List<GameObject> m_obstacles = new List<GameObject>();

    List<GameObject> m_spawnedObjects = new List<GameObject>();

    MouseRaycast m_mouseRayCast;

    private void Start()
    {
        m_mouseRayCast = GameObject.Find("Player").GetComponent<MouseRaycast>();
        m_mouseRayCast.OnSecondaryPositionChange += SpawnNewObject;
        m_dnm = GameObject.Find("NavMesh").GetComponent<DynamicNavMesh>();
    }

    public void GenerateObjects()
    {
        // 6.5 to -6.5
        for (int i = 0; i < m_maxObjects; i++)
        {
            GameObject go;
            switch (m_spawnType)
            {
                case ObjectSpawnType.modifiers:
                    go = Instantiate(m_modifiers[Random.Range(0, m_modifiers.Count)]);
                    break;
                case ObjectSpawnType.obstacles:
                    go = Instantiate(m_obstacles[Random.Range(0, m_obstacles.Count)]);
                    break;
                case ObjectSpawnType.both:
                default:
                    if (i % 2 == 0)
                    {
                        go = Instantiate(m_modifiers[Random.Range(0, m_modifiers.Count)]);
                    }
                    else
                    {
                        go = Instantiate(m_obstacles[Random.Range(0, m_obstacles.Count)]);
                    }
                    break;
            }

            go.transform.SetParent(m_objectParent);
            go.transform.position = GetNewPosition();
            go.transform.Rotate(Vector3.up, Random.Range(0f, 360f));

        }
    }

    void SpawnNewObject(Vector3 _pos)
    {
        bool modifier = false;
        GameObject go;
        switch (m_spawnType)
        {
            case ObjectSpawnType.modifiers:
                go = Instantiate(m_modifiers[Random.Range(0, m_modifiers.Count)]);
                modifier = true;
                break;
            case ObjectSpawnType.obstacles:
                go = Instantiate(m_obstacles[Random.Range(0, m_obstacles.Count)]);
                break;
            case ObjectSpawnType.both:
            default:
                if (Random.Range(0, 2) == 0)
                {
                    go = Instantiate(m_modifiers[Random.Range(0, m_modifiers.Count)]);
                    modifier = true;
                }
                else
                {
                    go = Instantiate(m_obstacles[Random.Range(0, m_obstacles.Count)]);
                }
                break;
        }
        go.transform.SetParent(m_objectParent);
        go.transform.position = _pos;
        go.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        if(m_autoRebuild && modifier)
        {
            m_dnm.BuildNavMesh();
        }
    }

    Vector3 GetNewPosition()
    {
        bool found = false;

        int aboveBridge = Random.Range(0, 10);

        if (aboveBridge == 0)
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
            float randX = Random.Range(-6.5f, 6.5f);
            float randZ = Random.Range(-6.5f, 6.5f);
            if (Physics.Raycast(new Vector3(randX, aboveBridge, randZ), Vector3.down, out hit, 3f))
            {
                if (NavMesh.SamplePosition(hit.point, out nmhit, 1.0f, NavMesh.AllAreas))
                {
                    result = nmhit.position;
                    found = true;
                }
            }
        }
        return result;
    }
}
