using UnityEngine;

public class Bocek1 : MonoBehaviour
{
	public bool YonSaga = true;

	public float Hiz = 1f;

	private Vector3 sagtaraf;

	private Vector3 soltaraf;

	private bool bittim;

	private GameObject kapagoz1;

	private GameObject kapagoz2;

	private GameObject goz1;

	private GameObject goz2;

	private GameObject[] dikens = new GameObject[2];

	private Color32 GriMat = Color.gray;

	private bool Patladi;

	private BoxCollider2D bC;

	public void Oldur()
	{
		bittim = true;
		SonBaslat();
	}

	private void Awake()
	{
		GriMat.r = (GriMat.g = (GriMat.b = 200));
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "goz1")
			{
				goz1 = gameObject;
			}
			if (gameObject.name == "goz2")
			{
				goz2 = gameObject;
			}
			if (gameObject.name == "kapaligoz1")
			{
				kapagoz1 = gameObject;
			}
			if (gameObject.name == "kapaligoz2")
			{
				kapagoz2 = gameObject;
			}
			if (gameObject.name == "kapaligoz2")
			{
				kapagoz2 = gameObject;
			}
			if (gameObject.name.Contains("diken"))
			{
				if (gameObject.name == "diken1")
				{
					bC = gameObject.gameObject.GetComponent<BoxCollider2D>();
				}
				if (dikens[0] == null)
				{
					dikens[0] = gameObject;
				}
				else
				{
					dikens[1] = gameObject;
				}
			}
		}
		kapagoz1.SetActive(false);
		kapagoz2.SetActive(false);
	}

	private void FixedUpdate()
	{
		if (!bittim)
		{
			if (YonSaga)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}

	private void SonBaslat()
	{
		if (!Patladi)
		{
			bC.enabled = false;
			dikens[0].AddComponent<Rigidbody2D>().AddTorque(100f);
			dikens[0].GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 2f);
			dikens[1].AddComponent<Rigidbody2D>().AddTorque(-100f);
			dikens[1].GetComponent<Rigidbody2D>().velocity = Vector2.one * 2f;
			GetComponent<SpriteRenderer>().color = GriMat;
			kapagoz1.SetActive(true);
			kapagoz2.SetActive(true);
			goz1.SetActive(false);
			goz2.SetActive(false);
			Invoke("DikenGizle", 1.5f);
			Patladi = true;
		}
	}

	private void DikenGizle()
	{
		dikens[0].SetActive(false);
		dikens[1].SetActive(false);
	}

	private void Update()
	{
		if (bittim)
		{
			return;
		}
		Vector2 vector;
		Vector2 vector2 = (vector = base.transform.position);
		vector2.y = (vector.y = base.transform.position.y + 0.4f);
		vector2.x = base.transform.position.x - 0.45f;
		vector.x = base.transform.position.x + 0.45f;
		Debug.DrawLine(vector2, vector);
		if ((bool)Physics2D.Linecast(vector2, vector, 1 << LayerMask.NameToLayer("Oyuncu")))
		{
			bittim = true;
			Invoke("SonBaslat", 0.1f);
		}
		sagtaraf = (soltaraf = base.transform.position);
		sagtaraf.x += 0.75f;
		soltaraf.x -= 0.75f;
		if (YonSaga)
		{
			if ((bool)Physics2D.OverlapPoint(sagtaraf, 1 << LayerMask.NameToLayer("zemin")))
			{
				YonSaga = false;
			}
		}
		else if ((bool)Physics2D.OverlapPoint(soltaraf, 1 << LayerMask.NameToLayer("zemin")))
		{
			YonSaga = true;
		}
	}
}
