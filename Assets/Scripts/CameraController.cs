using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public GameObject follow;

	private Vector3 offset;

	void Start()
	{
		offset = transform.position - follow.transform.position;
	}

	void LateUpdate()
	{
		transform.position = follow.transform.position + offset;
	}
}

