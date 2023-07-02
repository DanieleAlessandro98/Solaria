using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private const float MIN_DISTANCE_TO_NEXT_POINT = 0.1f;

    private IEnumerator<Point> m_CurrentPoint;
    private bool m_IsMovingNext;

    [SerializeField]
    private Following m_Following;

    // Start is called before the first frame update
    void Start()
    {
        m_IsMovingNext = false;

        m_CurrentPoint = m_Following.GetPathEnumerator();
        m_CurrentPoint.MoveNext();

        if (m_CurrentPoint.Current == null)
            return;

        transform.position = m_CurrentPoint.Current.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentPoint == null || m_CurrentPoint.Current == null)
            return;

        float pointSpeed = m_Following.IsUseGlobalSpeed() ? m_Following.GetGlobalSpeed() : m_CurrentPoint.Current.GetSpeed();

        float speed = Time.deltaTime * pointSpeed;
        transform.position = Vector3.MoveTowards(transform.position, m_CurrentPoint.Current.transform.position, speed);

        var distanceSquared = (transform.position - m_CurrentPoint.Current.transform.position).sqrMagnitude;
        if (distanceSquared < MIN_DISTANCE_TO_NEXT_POINT * MIN_DISTANCE_TO_NEXT_POINT)
        {
            if (!m_IsMovingNext)
                StartCoroutine(MoveNext());
        }
    }

    IEnumerator MoveNext()
    {
        m_IsMovingNext = true;

        float delay = m_CurrentPoint.Current.GetDelay();
        yield return new WaitForSeconds(delay);

        m_CurrentPoint.MoveNext();
        m_IsMovingNext = false;
    }
}
