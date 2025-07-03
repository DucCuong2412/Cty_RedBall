using System;
using UnityEngine;

public class AsansorDonen : MonoBehaviour
{
	private Vector3 merkez;

	private float x;

	private float y;

	public float DaireCapi = 3f;

	public float Hiz = 10f;

	private float theta;

	private bool gotur;

	private void Start()
	{
		merkez = base.transform.position;
		if (Hiz > 0f)
		{
			merkez.y = base.transform.position.y + DaireCapi * 0.6f;
		}
		else
		{
			merkez.y = base.transform.position.y - DaireCapi * 0.6f;
		}
	}

	private void FixedUpdate()
	{
		theta += Hiz * 0.001f;
		if (theta > (float)Math.PI * 2f)
		{
			theta = 0f;
		}
		x = DaireCapi * Mathf.Cos(theta) * 0.3f;
		y = DaireCapi * Mathf.Sin(theta) * 0.3f;
		GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		if (!Application.isPlaying)
		{
			Vector3 position = base.transform.position;
			if (Hiz > 0f)
			{
				position.y = base.transform.position.y + DaireCapi * 0.6f;
			}
			else
			{
				position.y = base.transform.position.y - DaireCapi * 0.6f;
			}
			Gizmos.DrawWireSphere(position, DaireCapi * 0.6f);
		}
		else
		{
			Gizmos.DrawWireSphere(merkez, DaireCapi * 0.6f);
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!gotur && kim.tag == "Player")
		{
			kim.gameObject.transform.parent = base.transform;
			kim.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			gotur = true;
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (gotur && kim.tag == "Player")
		{
			kim.transform.parent = null;
		}
	}
}
