using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private const int DAMAGE_TO_PLAYER = 1;

    private Transform targetRotation;
    private float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform;
        m_Speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = targetRotation.rotation.eulerAngles;
        rotation.z += m_Speed;

        targetRotation.rotation = Quaternion.Euler(rotation);
    }

	void OnCollisionEnter2D(Collision2D collision2D)
	{
		//TODO: invece di PlayerController implementare una abstract class/interface
		PlayerController player = collision2D.collider.GetComponent<PlayerController>();
		if (player)
            player.EnemyHit(GetComponent<Saw>().GetDamage());
    }

    public int GetDamage()
    {
        return DAMAGE_TO_PLAYER;
    }
}
