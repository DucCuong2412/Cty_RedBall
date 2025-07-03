using UnityEngine;

public class KayanBlkAsagi : MonoBehaviour
{
	public float Hiz = 1f;

	private float yukari;

	private float asagi;

	public bool Aktif;

	private bool YonAsagi;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Yukari")
			{
				yukari = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "Asagi")
			{
				asagi = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
		}
	}

	public void AsagiGit()
	{
		if (!(base.transform.position.y < asagi))
		{
			Aktif = true;
			YonAsagi = true;
			GetComponent<Rigidbody2D>().velocity = Vector2.up * (0f - Hiz);
		}
	}

	public void YukariGit()
	{
		if (!(base.transform.position.y > yukari))
		{
			Aktif = true;
			YonAsagi = false;
			GetComponent<Rigidbody2D>().velocity = Vector2.up * Hiz;
		}
	}

	private void Update()
	{
		if (!Aktif && GetComponent<Rigidbody2D>().velocity.y != 0f)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		else if (Aktif)
		{
			if (base.transform.position.y < asagi && YonAsagi)
			{
				Aktif = false;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			if (base.transform.position.y > yukari && !YonAsagi)
			{
				Aktif = false;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
		}
	}
}
