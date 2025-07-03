using UnityEngine;

public class Piti : MonoBehaviour
{
	public enum Durums
	{
		Mutlu = 0,
		Uyuyor = 1,
		Uyanik = 2,
		Agliyor = 3,
		Dusuyor = 4,
		Yuruyor = 5,
		DikenBatti = 6,
		Ezildi = 7
	}

	public Durums ilkDurumu = Durums.Uyuyor;

	[HideInInspector]
	public bool Geberdi;

	public float Hiz = 0.5f;

	public bool YonSaga;

	private Durums Durum;

	private Animator ani;

	private bool grounded;

	private bool SagaBakiyor;

	private float EzilmeHizi = 5f;

	private BoxCollider2D boxC;

	private PolygonCollider2D polyC;

	private Sprite[] Tipler = new Sprite[8];

	private SpriteRenderer SR;

	private GameObject Agiz;

	private Durums HavadanOncekiDurum;

	private void Awake()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name.Contains("piti_"))
			{
				string s = gameObject.name.Replace("piti_", string.Empty);
				int num2 = int.Parse(s);
				Tipler[num2] = gameObject.gameObject.GetComponent<SpriteRenderer>().sprite;
				Object.DestroyImmediate(gameObject.gameObject);
			}
		}
		Tipler[2] = (Tipler[5] = GetComponent<SpriteRenderer>().sprite);
	}

	private void Start()
	{
		boxC = GetComponent<BoxCollider2D>();
		boxC.enabled = false;
		polyC = base.gameObject.GetComponent<PolygonCollider2D>();
		SR = base.gameObject.GetComponent<SpriteRenderer>();
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "uyu")
			{
				Agiz = gameObject.gameObject;
			}
		}
		Agiz.SetActive(false);
		ilkDurum(ilkDurumu);
	}

	private void OnCollisionEnter2D(Collision2D ne)
	{
		if (Geberdi)
		{
			return;
		}
		GameObject gameObject = ne.gameObject;
		if (Durum == Durums.Uyuyor && (gameObject.tag == "Player" || gameObject.tag == "hareketli"))
		{
			DurumDegis(Durums.Yuruyor);
		}
		if (Durum == Durums.Yuruyor && gameObject.tag == "Player")
		{
			if (!YonSaga)
			{
				if ((double)gameObject.GetComponent<Rigidbody2D>().velocity.x > 4.5)
				{
					YonSaga = true;
				}
			}
			else if ((double)gameObject.GetComponent<Rigidbody2D>().velocity.x < -4.5)
			{
				YonSaga = false;
			}
		}
		if (gameObject.tag == "hareketli" && gameObject.transform.position.y > base.transform.position.y)
		{
			if (ne.relativeVelocity.y > EzilmeHizi && gameObject.GetComponent<Rigidbody2D>().mass > GetComponent<Rigidbody2D>().mass * 2f)
			{
				DurumDegis(Durums.Ezildi);
				boxC.enabled = true;
				polyC.enabled = false;
				GetComponent<Rigidbody2D>().velocity = Vector2.up * -1f;
				Geberdi = true;
			}
			else
			{
				DurumDegis(Durums.Yuruyor);
			}
		}
	}

	public void Agla()
	{
		DurumDegis(Durums.Agliyor);
	}

	private void Agizi(int drm)
	{
		if (drm == 1)
		{
			Agiz.SetActive(true);
		}
		else
		{
			Agiz.SetActive(false);
		}
	}

	private void ilkDurum(Durums darm)
	{
		Durum = darm;
		if (Tipler[(int)darm] != null)
		{
			SR.sprite = Tipler[(int)darm];
		}
		Agizi((int)darm);
	}

	private void DurumDegis(Durums darm)
	{
		if (Durum != darm)
		{
			Durum = darm;
			if (Tipler[(int)darm] != null)
			{
				SR.sprite = Tipler[(int)darm];
			}
			Agizi((int)darm);
		}
	}

	public void NormaleDon()
	{
		if (!Geberdi)
		{
			DurumDegis(Durums.Yuruyor);
		}
	}

	public void OdulBuldu()
	{
		if (!Geberdi)
		{
			DurumDegis(Durums.Mutlu);
		}
	}

	public void DikenBatti()
	{
		if (!Geberdi)
		{
			DurumDegis(Durums.DikenBatti);
			Geberdi = true;
		}
	}

	private void Flip()
	{
		SagaBakiyor = !SagaBakiyor;
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Hediye")
			{
				Vector3 localScale2 = gameObject.transform.localScale;
				localScale2.x *= -1f;
				gameObject.transform.localScale = localScale2;
			}
		}
	}

	private void FixedUpdate()
	{
		if (Geberdi)
		{
			return;
		}
		if (Durum == Durums.Uyuyor)
		{
			float num = Mathf.PingPong(Time.time * 0.3f, 0.4f);
			num += 0.6f;
			Agiz.transform.localScale = new Vector3(num, num, 1f);
		}
		if (Durum == Durums.Agliyor)
		{
			float num = Mathf.PingPong(Time.time * 0.4f, 0.1f);
			num += 0.9f;
			base.transform.localScale = new Vector3(1f, num, 1f);
		}
		if (Durum == Durums.Yuruyor)
		{
			float num2 = 1f;
			if (base.transform.localScale.x < 0f)
			{
				num2 = -1f;
			}
			float num3 = Mathf.PingPong(Time.time * 0.4f, 0.1f);
			num3 += 0.9f;
			base.transform.localScale = new Vector3(num3 * num2, 1f, 1f);
		}
		if (YonSaga)
		{
			if (!SagaBakiyor)
			{
				Flip();
			}
		}
		else if (SagaBakiyor)
		{
			Flip();
		}
		if (Durum == Durums.Yuruyor && grounded)
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
		if (Durum == Durums.Yuruyor || Durum == Durums.Dusuyor || Durum == Durums.Uyanik)
		{
			if (HavadanOncekiDurum != Durum && Durum != Durums.Dusuyor)
			{
				HavadanOncekiDurum = Durum;
			}
			Vector3 position;
			Vector3 vector = (position = base.transform.position);
			vector.x -= 0.25f;
			position.x += 0.25f;
			vector.y = (position.y = vector.y - 0.1f);
			Debug.DrawLine(vector, position, Color.yellow);
			grounded = Physics2D.Linecast(vector, position, 1 << LayerMask.NameToLayer("zemin"));
			if (!grounded)
			{
				DurumDegis(Durums.Dusuyor);
			}
			else
			{
				Durums havadanOncekiDurum = HavadanOncekiDurum;
				DurumDegis(havadanOncekiDurum);
			}
		}
		if (Durum == Durums.Dusuyor && GetComponent<Rigidbody2D>().velocity.y > 0f)
		{
			DurumDegis(Durums.Uyanik);
		}
		Vector3 position2;
		Vector3 vector2 = (position2 = base.transform.position);
		vector2.x += 0.4f;
		position2.x -= 0.4f;
		position2.y = (vector2.y = base.transform.position.y + 0.33f);
		Debug.DrawLine(position2, vector2, Color.blue);
		LayerMask layerMask = (1 << LayerMask.NameToLayer("haraketli")) | (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("zemin"));
		if (YonSaga)
		{
			if ((bool)Physics2D.OverlapPoint(vector2, layerMask))
			{
				YonSaga = false;
			}
		}
		else if ((bool)Physics2D.OverlapPoint(position2, layerMask))
		{
			YonSaga = true;
		}
	}
}
