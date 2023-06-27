using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController m_Singleton;

    private static readonly float SPEED = 1f;
    private static readonly float MIN_X = -100f;
    private static readonly float MIN_Y = -100f;

    [SerializeField]
    private Camera m_Camera;

    [SerializeField]
    private Transform m_Follower;

    public static CameraController Singleton
    {
        get
        {
            return m_Singleton;
        }
    }

    void Awake()
    {
        if (m_Singleton)
        {
            Destroy(gameObject);
            return;
        }

        m_Singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        float speed = SPEED;

        Vector3 cameraPosition = transform.position;
        Vector3 targetPosition = m_Follower.position;
        if (targetPosition.x - m_Camera.orthographicSize * m_Camera.aspect > MIN_X)
        {
            cameraPosition.x = targetPosition.x;
        }
        else
        {
            cameraPosition.x = MIN_X + m_Camera.orthographicSize * m_Camera.aspect;
        }
        if (targetPosition.y - m_Camera.orthographicSize > MIN_Y)
        {
            cameraPosition.y = targetPosition.y;
        }
        else
        {
            cameraPosition.y = MIN_Y + m_Camera.orthographicSize;
        }

        transform.position = Vector3.MoveTowards(transform.position, cameraPosition, speed);
    }
}
