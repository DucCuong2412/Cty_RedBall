using UnityEngine;

public class KayanBlok : MonoBehaviour
{
	public float Hiz = 1f;

	private float sol;

	private float sag;

	public bool Aktif;

	private bool YonSaga;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Sag")
			{
				sag = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "Sol")
			{
				sol = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
		}
	}

	public void SagaGit()
	{
		Aktif = true;
		YonSaga = true;
		GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
	}

	public void SolaGit()
	{
		Aktif = true;
		YonSaga = false;
		GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
	}

	private void Update()
	{
		if (!Aktif)
		{
			if (GetComponent<Rigidbody2D>().velocity.x != 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			return;
		}
		if (YonSaga)
		{
			if (GetComponent<Rigidbody2D>().velocity.x < 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
			}
		}
		else if (GetComponent<Rigidbody2D>().velocity.x > 0f)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
		}
		if (base.transform.position.x < sol)
		{
			Aktif = false;
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		if (base.transform.position.x > sag)
		{
			Aktif = false;
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}
