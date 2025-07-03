using UnityEngine;

public class BocekUcanDiken : MonoBehaviour
{
	private float sol;

	private float sag;

	public bool YonSaga = true;

	public float Hiz = 1f;

	private bool bittim;

	private GameObject[] dikens = new GameObject[5];

	private bool SagaBakiyor = true;

	private PolygonCollider2D bC;

	private bool Patladi;

	public void Oldur()
	{
		bittim = true;
		SonBaslat();
	}

	private void Awake()
	{
		int num = 0;
		GameObject gameObject = null;
		for (int num2 = base.transform.childCount - 1; num2 >= 0; num2--)
		{
			gameObject = base.transform.GetChild(num2).gameObject;
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
			if (gameObject.name.Contains("diken"))
			{
				dikens[num] = gameObject;
				num++;
				if (gameObject.name == "diken1")
				{
					bC = gameObject.GetComponent<PolygonCollider2D>();
				}
			}
		}
	}

	private void SonBaslat()
	{
		if (!Patladi)
		{
			bC.enabled = false;
			GetComponent<SpriteRenderer>().color = Color.gray;
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			for (int i = 0; i < 5; i++)
			{
				Vector2 vector = dikens[i].transform.position - base.transform.position;
				dikens[i].AddComponent<Rigidbody2D>().velocity = vector * 3f;
				dikens[i].GetComponent<Rigidbody2D>().AddTorque(-4f);
			}
			Invoke("DikenGizle", 1.5f);
			Patladi = true;
		}
	}

	private void DikenGizle()
	{
		for (int i = 0; i < 5; i++)
		{
			dikens[i].SetActive(false);
		}
	}

	private void Flip()
	{
		SagaBakiyor = !SagaBakiyor;
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
	}

	private void Update()
	{
		if (!bittim)
		{
			Vector2 vector;
			Vector2 vector2 = (vector = base.transform.position);
			vector2.y = (vector.y = base.transform.position.y - 0.2822489f);
			vector2.x = base.transform.position.x - 0.2796354f;
			vector.x = base.transform.position.x + 0.2691813f;
			Debug.DrawLine(vector2, vector);
			if ((bool)Physics2D.Linecast(vector2, vector, 1 << LayerMask.NameToLayer("Oyuncu")))
			{
				bittim = true;
				Invoke("SonBaslat", 0.1f);
			}
		}
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		if (YonSaga)
		{
			if (!SagaBakiyor)
			{
				Flip();
			}
			if (GetComponent<Rigidbody2D>().velocity.x < 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
			}
		}
		else
		{
			if (SagaBakiyor)
			{
				Flip();
			}
			if (GetComponent<Rigidbody2D>().velocity.x > 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
			}
		}
		if (YonSaga)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
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
}
