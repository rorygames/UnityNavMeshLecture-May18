using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseRaycast : MonoBehaviour {

	public Action<Vector3> OnPositionChange;
	public Action<Vector3> OnSecondaryPositionChange;

	Vector3 m_currentLocation = Vector3.zero;
	Vector3 m_secondLocation = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
		{
			if(GetMouseWorldRaycast(ref m_currentLocation))
			{
				if(OnPositionChange != null)
				{
					OnPositionChange(m_currentLocation);
				}
			}
		}
		if(Input.GetMouseButtonUp(1))
		{
			if(GetMouseWorldRaycast(ref m_secondLocation))
			{
				if(OnSecondaryPositionChange != null)
				{
					if(OnSecondaryPositionChange != null)
					{
						OnSecondaryPositionChange(m_secondLocation);
					}
				}
			}
		}
	}

	bool GetMouseWorldRaycast(ref Vector3 _position)
	{
		if(EventSystem.current.IsPointerOverGameObject())
		{
			Debug.Log("UI Clicked. World ignored.");
			return false;
		}
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100f))
		{
			NavMeshHit navMeshHit;
			if(NavMesh.SamplePosition(hit.point, out navMeshHit, 1.0f, NavMesh.AllAreas))
			{
				_position = navMeshHit.position;
				return true;
				
			}
			else
			{
				ErrorLog();
			}
		}
		else
		{
			ErrorLog();
		}
		return false;
	}

	void ErrorLog()
	{
		Debug.LogWarning("Unable to place mouse position in world.");
	}
}
