using UnityEngine;

public class UzayliYercek : MonoBehaviour
{
	public bool Dead;

	private Animator anim;

	private bool Aktif;

	private Transform PlayrTrans;

	private float ActiveMesafesi = 2f;

	private int Yon;

	public void HayatSon()
	{
		Dead = true;
		float y = -9.81f;
		Physics2D.gravity = new Vector2(0f, y);
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
		InvokeRepeating("YonDegis", 3f, 4f);
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			PlayrTrans = gameObject.transform;
		}
		Yon = 1;
	}

	private void YonDegis()
	{
		if (!Dead)
		{
			if (Yon == 1)
			{
				SolaGonder();
				Yon = 2;
			}
			else if (Yon == 2)
			{
				YukariGonder();
				Yon = 3;
			}
			else if (Yon == 3)
			{
				SagaGonder();
				Yon = 4;
			}
			else if (Yon == 4)
			{
				AsagiGonder();
				Yon = 1;
			}
			if ((bool)anim)
			{
				anim.SetTrigger("Yukari");
			}
		}
	}

	private void YukariGonder()
	{
		if ((bool)anim)
		{
			anim.SetTrigger("Yukari");
		}
		float y = 4.905f;
		Physics2D.gravity = new Vector2(0f, y);
	}

	private void SagaGonder()
	{
		if ((bool)anim)
		{
			anim.SetTrigger("Yukari");
		}
		float y = -9.81f;
		Physics2D.gravity = new Vector2(4f, y);
	}

	private void SolaGonder()
	{
		if ((bool)anim)
		{
			anim.SetTrigger("Asagi");
		}
		float y = -9.81f;
		Physics2D.gravity = new Vector2(-4f, y);
	}

	private void AsagiGonder()
	{
		if ((bool)anim)
		{
			anim.SetTrigger("Asagi");
		}
		float y = -9.81f;
		Physics2D.gravity = new Vector2(0f, y);
	}

	private void FixedUpdate()
	{
		if (!Dead && !Aktif)
		{
			float num = Vector3.Distance(PlayrTrans.position, base.transform.position);
			if (num < ActiveMesafesi)
			{
				Aktif = true;
				YukariGonder();
			}
		}
	}
}
