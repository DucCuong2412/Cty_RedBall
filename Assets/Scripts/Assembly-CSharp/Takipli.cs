using System;
using UnityEngine;

public class Takipli : MonoBehaviour
{
	private bool Takipte;

	private bool HedefVar;

	private bool EveDonuyor;

	private Transform player;

	private float DaireCapi = 2f;

	private Vector3 nereden;

	private Vector3 nereye;

	private float ilkTime;

	private float TakipSuresi = 0.5f;

	private float angle;

	private float speed = (float)Math.PI;

	private float radius = 1f;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void ParaAlindi()
	{
		ilkTime = Time.time;
		nereden = base.transform.position;
		nereden.z = -0.5f;
		if (Vector2.Distance(base.transform.position, player.position) > DaireCapi * 1.3f)
		{
			EveDonuyor = true;
			nereye = player.position;
			nereye.z = -0.5f;
			return;
		}
		Collider2D collider2D = Physics2D.OverlapCircle(base.transform.position, DaireCapi, 1 << LayerMask.NameToLayer("Para"));
		if (collider2D != null)
		{
			HedefVar = true;
			Takipte = true;
			nereye = collider2D.gameObject.transform.position;
			nereye.z = -0.5f;
		}
		else
		{
			HedefVar = false;
			EveDonuyor = true;
		}
	}

	private void FixedUpdate()
	{
		if (Takipte)
		{
			if (EveDonuyor)
			{
				Vector3 position = player.position;
				position.x += 1f;
				position.z = -0.5f;
				if (Vector2.Distance(base.transform.position, position) < 0.1f)
				{
					Takipte = (HedefVar = (EveDonuyor = false));
					angle = 0f;
				}
				else
				{
					float t = (Time.time - ilkTime) / TakipSuresi;
					base.transform.position = Vector3.Lerp(nereden, position, t);
				}
				return;
			}
			float t2 = (Time.time - ilkTime) / TakipSuresi;
			base.transform.position = Vector3.Lerp(nereden, nereye, t2);
			if (!(base.transform.position == nereye))
			{
				return;
			}
			if (Vector2.Distance(base.transform.position, player.position) > DaireCapi * 2f)
			{
				Takipte = (HedefVar = false);
				nereden = base.transform.position;
				nereye = player.transform.position;
				nereye.z = -0.5f;
				return;
			}
			Collider2D collider2D = Physics2D.OverlapCircle(base.transform.position, DaireCapi, 1 << LayerMask.NameToLayer("Para"));
			if (collider2D != null)
			{
				Takipte = (HedefVar = true);
				nereden = base.transform.position;
				nereye = collider2D.gameObject.transform.position;
				nereye.z = -0.5f;
				ilkTime = Time.time;
			}
			else
			{
				EveDonuyor = true;
			}
			return;
		}
		angle -= speed * Time.deltaTime;
		Vector3 position2 = player.position;
		position2.z = -0.5f;
		position2.x += Mathf.Cos(angle) * radius;
		position2.y += Mathf.Sin(angle) * radius;
		base.transform.position = position2;
		if (!HedefVar)
		{
			Collider2D collider2D = Physics2D.OverlapCircle(player.transform.position, DaireCapi, 1 << LayerMask.NameToLayer("Para"));
			if (collider2D != null)
			{
				Takipte = (HedefVar = true);
				nereden = base.transform.position;
				nereye = collider2D.gameObject.transform.position;
				nereden.z = -0.5f;
				nereye.z = -0.5f;
				ilkTime = Time.time;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, DaireCapi);
	}
}
