using UnityEngine;

public class AsansorYan : MonoBehaviour
{
	private float sol;

	private float sag;

	private bool gotur;

	private SpriteRenderer SolLamba;

	private SpriteRenderer SagLamba;

	public bool YonSaga;

	public bool Aktif = true;

	public float Hiz = 1f;

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
			if (gameObject.name == "lamba_sag")
			{
				SagLamba = gameObject.GetComponent<SpriteRenderer>();
			}
			if (gameObject.name == "lamba_sol")
			{
				SolLamba = gameObject.GetComponent<SpriteRenderer>();
			}
		}
		if (Aktif)
		{
			if (YonSaga)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
			}
		}
	}

	public void AktifYap()
	{
		if (!Aktif)
		{
			Aktif = true;
			if (YonSaga)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
			}
		}
	}

	private void FixedUpdate()
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
		if (base.transform.position.x < sol + 0.2f)
		{
			SagLamba.color = Color.yellow;
			SolLamba.color = Color.white;
		}
		if (base.transform.position.x > sag - 0.2f)
		{
			SagLamba.color = Color.white;
			SolLamba.color = Color.yellow;
		}
		if (base.transform.position.x < sol)
		{
			YonSaga = true;
		}
		if (base.transform.position.x > sag)
		{
			YonSaga = false;
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
