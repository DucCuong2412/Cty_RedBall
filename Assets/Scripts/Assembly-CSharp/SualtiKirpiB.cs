using UnityEngine;

public class SualtiKirpiB : MonoBehaviour
{
	public bool YonSaga;

	public float Hiz = 1f;

	private float sol;

	private float sag;

	private bool SagaBakiyor;

	private GameObject diken1;

	private GameObject diken2;

	private SpriteRenderer SR;

	private PolygonCollider2D PolyC;

	public float DikenVarSuresi = 2f;

	public float DikenGizleSure = 2f;

	private bool bittim;

	private Player PlayerSc;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			Player.Geberdi = true;
			PlayerSc.GeberSurat();
		}
	}

	public void Oldur()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		CancelInvoke();
		Gizle();
		bittim = true;
	}

	private void Goster()
	{
		if (!bittim)
		{
			SR.color = Color.red;
			diken1.SetActive(true);
			diken2.SetActive(true);
			PolyC.enabled = true;
			Invoke("Gizle", DikenVarSuresi);
		}
	}

	private void Gizle()
	{
		if (!bittim)
		{
			SR.color = Color.white;
			diken1.SetActive(false);
			diken2.SetActive(false);
			PolyC.enabled = false;
			Invoke("Goster", DikenGizleSure);
		}
	}

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Sol")
			{
				sol = gameObject.transform.position.x;
			}
			if (gameObject.name == "Sag")
			{
				sag = gameObject.transform.position.x;
			}
			if (gameObject.name == "diken1")
			{
				diken1 = gameObject;
				PolyC = gameObject.gameObject.GetComponent<PolygonCollider2D>();
			}
			if (gameObject.name == "diken2")
			{
				diken2 = gameObject;
			}
		}
		SR = GetComponent<SpriteRenderer>();
		diken1.SetActive(false);
		diken2.SetActive(false);
		Invoke("Gizle", DikenVarSuresi);
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void Flip()
	{
		SagaBakiyor = !SagaBakiyor;
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		Vector3 position = base.transform.position;
		position.y += Mathf.Sin(Time.time) * Time.deltaTime * 0.3f;
		base.transform.position = position;
		if (YonSaga)
		{
			if (sag < base.transform.position.x)
			{
				YonSaga = false;
			}
		}
		else if (base.transform.position.x < sol)
		{
			YonSaga = true;
		}
		if (YonSaga)
		{
			if (!SagaBakiyor)
			{
				Flip();
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			if (SagaBakiyor)
			{
				Flip();
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
	}
}
