using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
	[SerializeField]
	private float speed = 1f;

	[SerializeField]
	private float delay = 0f;

	public float GetSpeed()
    {
		return speed;
    }

	public float GetDelay()
    {
		return delay;
	}
}
