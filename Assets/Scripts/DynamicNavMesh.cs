using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class DynamicNavMesh : MonoBehaviour {

    public Action OnNavMeshBuild;

	NavMeshSurface m_navMeshSurface;

    ObstacleSpawner m_obstacleSpawner;

    [SerializeField]
    bool m_spawnObjects = true;    

	void Awake()
	{
		m_navMeshSurface = GetComponent<NavMeshSurface>();
        m_obstacleSpawner = GetComponent<ObstacleSpawner>();
	}

	// Use this for initialization
	void Start () {
		BuildNavMesh();
        if(m_spawnObjects)
        {
            m_obstacleSpawner.GenerateObjects();
            BuildNavMesh();
        }
	}

	public void BuildNavMesh()
	{
		m_navMeshSurface.BuildNavMesh();
        if(OnNavMeshBuild != null)
        {
            OnNavMeshBuild();
        }
	}
}
