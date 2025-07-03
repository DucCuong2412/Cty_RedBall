using UnityEngine;

public class RobotElektrik : MonoBehaviour
{
	private float sol;

	private float sag;

	public bool ilkDurumAcik;

	public float aktifZaman = 4f;

	public float kapaliZaman = 4f;

	public bool YonSaga = true;

	public float Hiz = 0.4f;

	private bool bittim;

	private bool Elektri;

	private GameObject goz;

	private GameObject gozk;

	private GameObject Kapak;

	private GameObject[] Eleks = new GameObject[3];

	private Vector3 nereden;

	private Vector3 nereye;

	private float islemHizi = 0.5f;

	private float moveTime;

	private BoxCollider2D boxC;

	public int SonDurum = 1;

	private Player PlayerSc;

	public void Oldur()
	{
		bittim = true;
		Invoke("SonBaslat", 0.2f);
		boxC.enabled = false;
	}

	private void FixedUpdate()
	{
		if (SonDurum == 4)
		{
			if (Kapak.transform.localPosition.y == 0.55f)
			{
				SonDurum = 1;
				Invoke("Ac", kapaliZaman);
			}
			if (Kapak.transform.localPosition.y > 0.55f)
			{
				moveTime += Time.deltaTime * islemHizi;
				Kapak.transform.localPosition = Vector3.Lerp(nereden, nereye, moveTime);
			}
		}
		if (bittim)
		{
			return;
		}
		if (SonDurum == 5)
		{
			if (Kapak.transform.localPosition.y == 2.55f)
			{
				SonDurum = 2;
				Elektri = true;
				boxC.enabled = true;
				for (int i = 0; i < 3; i++)
				{
					Eleks[i].SetActive(true);
				}
				Invoke("Kapat", aktifZaman);
			}
			if (Kapak.transform.localPosition.y < 2.55f)
			{
				moveTime += Time.deltaTime * islemHizi;
				Kapak.transform.localPosition = Vector3.Lerp(nereden, nereye, moveTime);
			}
		}
		if (Elektri)
		{
			ElekGit(0, 2.4f);
			ElekGit(1, 3.5f);
			ElekGit(2, 2.2f);
		}
		if (SonDurum != 1)
		{
			return;
		}
		if (YonSaga)
		{
			if (base.transform.position.x > sag)
			{
				YonSaga = false;
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			if (base.transform.position.x < sol)
			{
				YonSaga = true;
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
	}

	private void ElekGit(int no, float Hiz)
	{
		Vector3 position;
		Vector3 a = (position = Eleks[no].transform.position);
		a.y = Kapak.transform.position.y;
		position.y = base.transform.position.y;
		Eleks[no].transform.position = Vector3.Lerp(a, position, (Mathf.Sin(Time.time * Hiz) + 1f) / 2f);
	}

	private void Start()
	{
		boxC = GetComponent<BoxCollider2D>();
		boxC.enabled = false;
		int num = 0;
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		for (int num2 = base.transform.childCount - 1; num2 >= 0; num2--)
		{
			gameObject = (Kapak = base.transform.GetChild(num2).gameObject);
			nereden = (nereye = Kapak.transform.localPosition);
			if (gameObject.name == "robi1_ust")
			{
				for (int num3 = gameObject.transform.childCount - 1; num3 >= 0; num3--)
				{
					gameObject2 = gameObject.transform.GetChild(num3).gameObject;
					if (gameObject2.name == "goz")
					{
						goz = gameObject2;
					}
					if (gameObject2.name == "gozkapat")
					{
						gozk = gameObject2;
						gozk.SetActive(false);
					}
				}
			}
			if (gameObject.name.Contains("elek"))
			{
				Eleks[num] = gameObject;
				num++;
			}
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
		for (int i = 0; i < 3; i++)
		{
			Eleks[i].SetActive(false);
		}
		if (ilkDurumAcik)
		{
			Ac();
		}
		else
		{
			Kapat();
		}
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void Kapat()
	{
		SonDurum = 4;
		moveTime = 0f;
		nereden.y = 2.55f;
		nereye.y = 0.55f;
		Elektri = false;
		boxC.enabled = false;
		for (int i = 0; i < 3; i++)
		{
			Eleks[i].SetActive(false);
		}
	}

	private void Ac()
	{
		SonDurum = 5;
		moveTime = 0f;
		nereden.y = 0.55f;
		nereye.y = 2.55f;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!bittim && kim.gameObject.tag == "Player")
		{
			if (kim.transform.position.y > Kapak.transform.position.y)
			{
				bittim = true;
				Invoke("SonBaslat", 0.2f);
				boxC.enabled = false;
			}
			if (kim.transform.position.y < Kapak.transform.position.y && !PlayerSc.KalkanAktif)
			{
				PlayerSc.GeberSurat();
				Player.Geberdi = true;
			}
		}
	}

	private void SonBaslat()
	{
		SonDurum = 6;
		CancelInvoke();
		moveTime = 0f;
		nereden.y = Kapak.transform.localPosition.y;
		nereye.y = 0.55f;
		for (int i = 0; i < 3; i++)
		{
			Eleks[i].SetActive(false);
		}
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<Rigidbody2D>().freezeRotation = true;
		goz.SetActive(false);
		gozk.SetActive(true);
	}
}
