using System.Collections.Generic;
using UnityEngine;

public class Crush : MonoBehaviour
{
	private bool Tetikbitti;

	private List<GameObject> Bulunanlar = new List<GameObject>();

	public Color kendiRengi;

	public bool tetik;

	private Vector3 TeenScaleilk;

	private Vector3 TeenScaleSon;

	private float TweenilkTime;

	public float TweenSuresi = 0.1f;

	public int islemEase;

	private void Start()
	{
		TeenScaleilk = base.transform.localScale;
		TeenScaleSon = Vector3.one * 1.3f;
	}

	private void FixedUpdate()
	{
		if (tetik && tetik && !Tetikbitti)
		{
			if (TweenilkTime == 0f)
			{
				TweenilkTime = Time.time;
			}
			if ((double)base.transform.localScale.y < (double)TeenScaleSon.y * 1.1 && islemEase == 0)
			{
				TeenScaleilk = Vector3.one * 1f;
				TeenScaleSon = Vector3.one * 1.3f;
				TweenilkTime = Time.time;
				islemEase = 1;
			}
			if ((double)base.transform.localScale.y > (double)TeenScaleSon.y * 0.9 && islemEase == 1)
			{
				TeenScaleilk = Vector3.one * 1.3f;
				TeenScaleSon = Vector3.one * 0.7f;
				TweenilkTime = Time.time;
				islemEase = 2;
			}
			if ((double)base.transform.localScale.y < (double)TeenScaleSon.y * 1.1 && islemEase == 2)
			{
				islemEase = 3;
			}
			float t = (Time.time - TweenilkTime) / TweenSuresi;
			base.transform.localScale = Vector3.Lerp(TeenScaleilk, TeenScaleSon, t);
			if ((double)base.transform.localScale.y <= 0.7 && islemEase == 3)
			{
				Tetikbitti = true;
				base.gameObject.SetActive(false);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		Bulunanlar.Remove(kim.gameObject);
	}

	private void OnTriggerStay2D(Collider2D kim)
	{
		if (!kim.gameObject.name.Contains("candy") || tetik)
		{
			return;
		}
		if (kim.gameObject.name.Contains("candy") && kim.GetComponent<Crush>().kendiRengi == kendiRengi && !Bulunanlar.Contains(kim.gameObject))
		{
			Bulunanlar.Add(kim.gameObject);
		}
		if (Bulunanlar.Count <= 1)
		{
			return;
		}
		for (int i = 0; i < Bulunanlar.Count; i++)
		{
			GameObject gameObject = Bulunanlar[i];
			gameObject.GetComponent<Crush>().tetik = true;
		}
		for (int j = 0; j < Bulunanlar.Count; j++)
		{
			GameObject gameObject2 = Bulunanlar[j];
			Rigidbody2D component = gameObject2.GetComponent<Rigidbody2D>();
			if ((bool)component)
			{
				Object.Destroy(component);
			}
			BoxCollider2D[] components = gameObject2.GetComponents<BoxCollider2D>();
			BoxCollider2D[] array = components;
			foreach (BoxCollider2D obj in array)
			{
				Object.Destroy(obj);
			}
		}
		Object.Destroy(GetComponent<Rigidbody2D>());
		Component[] components2 = GetComponents<BoxCollider2D>();
		Component[] array2 = components2;
		foreach (Component component2 in array2)
		{
			Object.Destroy(component2 as BoxCollider2D);
		}
		tetik = true;
	}
}
