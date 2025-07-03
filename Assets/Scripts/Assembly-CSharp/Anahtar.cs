using UnityEngine;

public class Anahtar : MonoBehaviour
{
	private Vector3 SabitKonum;

	private bool Takipte;

	private Transform player;

	private Vector2 playerVel;

	private float ilkTime;

	private float GidisSuresi = 1f;

	private bool HedefeGidiyor;

	private Vector3 nereden;

	private Vector3 nereye;

	private bool hedefteGizle;

	public string KapiTriggerAdi = string.Empty;

	private void Start()
	{
		SabitKonum = base.transform.position;
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			player = gameObject.transform;
			playerVel = gameObject.GetComponent<Rigidbody2D>().velocity;
		}
	}

	public void HedefGoster(Vector3 ilk, Vector3 son, bool gizle = false)
	{
		nereden = ilk;
		nereye = son;
		ilkTime = Time.time;
		HedefeGidiyor = true;
		hedefteGizle = gizle;
	}

	public void konumVer(Vector3 yer)
	{
		SabitKonum = yer;
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!Takipte && kim.gameObject.tag == "Player")
		{
			int layer = LayerMask.NameToLayer("OyuncuNot");
			base.gameObject.layer = layer;
			Takipte = true;
		}
	}

	private void FixedUpdate()
	{
		if (HedefeGidiyor)
		{
			if (base.transform.position == nereye)
			{
				if (hedefteGizle)
				{
					base.gameObject.SetActive(false);
				}
				HedefeGidiyor = false;
				SabitKonum = base.transform.position;
			}
			float t = (Time.time - ilkTime) / GidisSuresi;
			base.transform.position = Vector3.Lerp(nereden, nereye, t);
		}
		else if (Takipte)
		{
			float num = 4f;
			float num2 = 4f;
			float x = base.transform.position.x;
			float y = base.transform.position.y;
			num2 = Mathf.Clamp(Mathf.Abs(playerVel.y), 5f, 20f);
			num = Mathf.Clamp(Mathf.Abs(playerVel.x), 5f, 20f);
			x = Mathf.Lerp(base.transform.position.x, player.position.x, num * 0.5f * Time.deltaTime);
			y = Mathf.Lerp(base.transform.position.y, player.position.y + 0.6f, num2 * 0.5f * Time.deltaTime);
			base.transform.position = new Vector3(x, y, base.transform.position.z);
		}
		else
		{
			float num3 = Mathf.PingPong(Time.time * 0.3f, 0.5f);
			num3 += SabitKonum.y;
			base.transform.position = new Vector3(SabitKonum.x, num3, SabitKonum.z);
		}
	}
}
