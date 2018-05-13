using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	Transform m_targetToFollow;

	[SerializeField]
	float m_followSpeed = 7f;

	[SerializeField]
	float m_rotationSpeed = 90f;

	Transform m_cameraParent,m_cameraRotator;

	[SerializeField]
	float m_rotationAngle = 45f;

	void Awake()
	{
		if(m_targetToFollow == null)
			Debug.LogWarning("No camera target set, camera will not move. Make sure to set one in the editor");

		m_cameraParent = transform;
		m_cameraRotator = transform.GetChild(0);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		if(Input.GetAxisRaw("Horizontal") != 0)
		{
			m_rotationAngle += (Time.deltaTime * (Input.GetAxisRaw("Horizontal") * -1)) * m_rotationSpeed;
			if(m_rotationAngle > 360)
			{
				m_rotationAngle -= 360;
			}
			if(m_rotationAngle < 0)
			{
				m_rotationAngle += 360;
			}
		}
		
	}

	// Update is called once per frame
	void LateUpdate () {
		float interp = m_followSpeed * (Time.deltaTime);
		Vector3 nPos = m_cameraParent.position;
		nPos = Vector3.Lerp(nPos, m_targetToFollow.position, interp);
		m_cameraParent.position = nPos;

        Quaternion nRot = m_cameraRotator.localRotation;
        Quaternion tRot = Quaternion.Euler(0,m_rotationAngle,0);
        nRot = Quaternion.Lerp(nRot, tRot, interp);
        m_cameraRotator.localRotation = nRot;
	}
}
