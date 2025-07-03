using UnityEngine;

public class AsansorUst : MonoBehaviour
{
	private float ust;

	private float alt;

	private bool gotur;

	private SpriteRenderer SolLamba;

	private SpriteRenderer SagLamba;

	private bool LambaVar;

	public bool YonYukari;

	public bool Aktif = true;

	public float Hiz = 1f;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Ust")
			{
				ust = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "Alt")
			{
				alt = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "lamba_sag")
			{
				LambaVar = true;
				SagLamba = gameObject.GetComponent<SpriteRenderer>();
			}
			if (gameObject.name == "lamba_sol")
			{
				LambaVar = true;
				SolLamba = gameObject.GetComponent<SpriteRenderer>();
			}
		}
		if (Aktif)
		{
			if (YonYukari)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.up * Hiz;
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.up * (0f - Hiz);
			}
		}
	}

	public void AktifYap()
	{
		if (!Aktif)
		{
			Aktif = true;
			if (YonYukari)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.up * Hiz;
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.up * (0f - Hiz);
			}
		}
	}

	private void FixedUpdate()
	{
		if (!Aktif)
		{
			if (GetComponent<Rigidbody2D>().velocity.y != 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			return;
		}
		if (YonYukari)
		{
			if (GetComponent<Rigidbody2D>().velocity.y < 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.up * Hiz;
			}
		}
		else if (GetComponent<Rigidbody2D>().velocity.y > 0f)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * (0f - Hiz);
		}
		if (LambaVar)
		{
			if (base.transform.position.y < alt + 0.2f)
			{
				SagLamba.color = Color.yellow;
				SolLamba.color = Color.white;
			}
			if (base.transform.position.y > ust - 0.2f)
			{
				SagLamba.color = Color.white;
				SolLamba.color = Color.yellow;
			}
		}
		if (base.transform.position.y < alt)
		{
			YonYukari = true;
		}
		if (base.transform.position.y > ust)
		{
			YonYukari = false;
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
