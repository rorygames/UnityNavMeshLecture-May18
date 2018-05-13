using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMAMove : MonoBehaviour {

	MouseRaycast m_mraycast;

	NavMeshAgent m_agent;

	void Awake()
	{
		m_agent = GetComponent<NavMeshAgent>();
	}

	void OnEnable()
	{
		m_mraycast = GetComponent<MouseRaycast>();
		m_mraycast.OnPositionChange += ChangePosition;
	}

	void OnDisable()
	{
		m_mraycast.OnPositionChange -= ChangePosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangePosition(Vector3 _pos)
	{
		m_agent.SetDestination(_pos);
		
	}
}
