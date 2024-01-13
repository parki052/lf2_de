using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform player;
	public float lerpSpeed = 3f;

	void Start()
	{
		if (player == null)
		{
			player = GameObject.Find("Player").transform;
		}
	}

	void Update()
	{

	}

    private void FixedUpdate()
    {

		Vector3 newPos = transform.position;

		if(player.position.x > 0)
        {
			newPos.x = player.position.x;
        }

		if(player.position.x > 2)
        {
			transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);
        }
    }
}
