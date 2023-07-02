using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
	[SerializeField]
	private List<Point> m_Points;

	[SerializeField]
	private bool m_UseGlobalSpeed = false;
	[SerializeField]
	private float m_GlobalSpeed = 1f;

	public IEnumerator<Point> GetPathEnumerator()
	{
		if (m_Points == null || m_Points.Count < 1)
			yield break;

		var direction = 1;
		var index = 0;

		while (true)
		{
			yield return m_Points[index];

			if (m_Points.Count == 1)
				continue;

			if (index <= 0)
				direction = 1;
			else if (index >= m_Points.Count - 1)
				direction = -1;

			if (index == m_Points.Count - 1)
				index = 0;
			else
				index = index + direction;
		}
	}

	public bool IsUseGlobalSpeed()
    {
		return m_UseGlobalSpeed;
    }

	public float GetGlobalSpeed()
    {
		return m_GlobalSpeed;
    }
}
