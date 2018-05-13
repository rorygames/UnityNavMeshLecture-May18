using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMAAnimationController : MonoBehaviour {

	Animator m_animator;

	NavMeshAgent m_agent;

	bool m_grounded = true;

	void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
		m_animator.SetBool("Grounded", m_grounded);
	}
	
	// Update is called once per frame
	void Update () {
		m_animator.SetFloat("MoveSpeed", m_agent.velocity.magnitude);
		if(m_agent.isOnOffMeshLink)
		{
			// this works for the current scene, however others may have offlink meshes of jumping or climing.
			// you would need to validate exactly what the offmeshlink properties are
			m_grounded = false;
		}
		else
		{
			m_grounded = true;
		}
		m_animator.SetBool("Grounded", m_grounded);
	}
}
