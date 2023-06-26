using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
	public const string GROUND_TAG = "Ground";
	public const string GROUND_LAYER_NAME = "Ground";

	private bool m_IsGrounded;

	[SerializeField]
    private Collider2D m_Collider2D;

	[SerializeField]
	private float m_GroundCheckDistance = 0.1f;


	// Start is called before the first frame update
	void Start()
    {
		m_IsGrounded = false;
	}

    // Update is called once per frame
    void Update()
	{
		Vector2 left = new Vector2(m_Collider2D.bounds.max.x, m_Collider2D.bounds.center.y);
		Vector2 center = new Vector2(m_Collider2D.bounds.center.x, m_Collider2D.bounds.center.y);
		Vector2 right = new Vector2(m_Collider2D.bounds.min.x, m_Collider2D.bounds.center.y);

		RaycastHit2D hit1 = Physics2D.Raycast(left, new Vector2(0f, -1f), m_GroundCheckDistance, LayerMask.GetMask(GROUND_LAYER_NAME));
		Debug.DrawRay(left, new Vector2(0f, -m_GroundCheckDistance));
		bool grounded1 = hit1 && hit1.collider != null && hit1.collider.CompareTag(GROUND_TAG);

		RaycastHit2D hit2 = Physics2D.Raycast(center, new Vector2(0f, -1f), m_GroundCheckDistance, LayerMask.GetMask(GROUND_LAYER_NAME));
		Debug.DrawRay(center, new Vector2(0f, -m_GroundCheckDistance));
		bool grounded2 = hit2 && hit2.collider != null && hit2.collider.CompareTag(GROUND_TAG);

		RaycastHit2D hit3 = Physics2D.Raycast(right, new Vector2(0f, -1f), m_GroundCheckDistance, LayerMask.GetMask(GROUND_LAYER_NAME));
		Debug.DrawRay(right, new Vector2(0f, -m_GroundCheckDistance));
		bool grounded3 = hit3 && hit3.collider != null && hit3.collider.CompareTag(GROUND_TAG);

		bool grounded = grounded1 || grounded2 || grounded3;
		m_IsGrounded = grounded;
	}

	public bool IsGrounded()
    {
		return m_IsGrounded;
    }

}
