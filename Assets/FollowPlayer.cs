using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform m_Target;
	public float lerpSpeed = 3f;

	void Start()
	{
		if (m_Target == null)
		{
			m_Target = GameObject.Find("Player").transform;
		}
	}

	void Update()
	{

	}

    private void FixedUpdate()
    {
		Vector3 newPos = transform.position;
		newPos.x = m_Target.position.x;

		transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);
    }
}
