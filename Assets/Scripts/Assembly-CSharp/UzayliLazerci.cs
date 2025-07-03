using UnityEngine;

public class UzayliLazerci : MonoBehaviour
{
	private bool Dead;

	public bool grounded;

	public bool Aktif;

	public float moveForce = 1f;

	public float jumpForce = 5.5f;

	public float maxSpeed = 4.2f;

	public bool Zipla;

	public bool Sag;

	public bool Sol;

	public bool CiftinLideri;

	private Transform KarsiMerkez;

	private Transform KendiMerkz;

	private Transform PlayrTrans;

	private LineRenderer LR;

	private bool Yakaladi;

	private bool pairkonumOk;

	private GameObject yanik;

	private Vector3 orta;

	private Vector3 ceyrek;

	private Vector3 ceyrek2;

	private bool uste = true;

	private bool uste2;

	public void HayatSon()
	{
		Dead = true;
	}

	private void Update()
	{
		if (Dead)
		{
			return;
		}
		Vector2 point = base.transform.position;
		point.y -= 0.5f;
		grounded = Physics2D.OverlapPoint(point, (1 << LayerMask.NameToLayer("zemin")) | (1 << LayerMask.NameToLayer("Oyuncu")));
		if (!Yakaladi && pairkonumOk)
		{
			float distance = Vector3.Distance(KendiMerkz.position, KarsiMerkez.position);
			Vector2 direction = (KarsiMerkez.position - KendiMerkz.position).normalized;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, direction, distance, 1 << LayerMask.NameToLayer("Oyuncu"));
			if (raycastHit2D.collider != null)
			{
				Vector3 position = raycastHit2D.point;
				Yakaladi = true;
				yanik.transform.position = position;
				yanik.SetActive(true);
				yanik.transform.parent = PlayrTrans;
				Player.Geberdi = true;
			}
		}
	}

	private void Awake()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			PlayrTrans = gameObject.transform;
		}
		if (CiftinLideri)
		{
			GameObject gameObject2 = GameObject.Find("uzayliLazerix2");
			if ((bool)gameObject2)
			{
				KarsiMerkez = gameObject2.transform;
			}
			GameObject gameObject3 = GameObject.Find("uzayliLazerix1");
			if ((bool)gameObject3)
			{
				KendiMerkz = gameObject3.transform;
			}
			if ((bool)gameObject2 && (bool)gameObject3)
			{
				pairkonumOk = true;
			}
			for (int num = base.transform.childCount - 1; num >= 0; num--)
			{
				GameObject gameObject4 = base.transform.GetChild(num).gameObject;
				if (gameObject4.name == "yanik")
				{
					yanik = gameObject4.gameObject;
					yanik.SetActive(false);
					yanik.transform.parent = null;
				}
			}
			LR = gameObject3.GetComponent<LineRenderer>();
			if ((bool)gameObject2 && (bool)gameObject3)
			{
				LR.SetPosition(0, KarsiMerkez.position);
				LR.SetPosition(1, KendiMerkz.position);
			}
			LR.sortingLayerName = "Oyun";
			LR.sortingOrder = 19;
		}
		else
		{
			GameObject gameObject5 = GameObject.Find("uzayliLazerix1");
			if ((bool)gameObject5)
			{
				KarsiMerkez = gameObject5.transform;
			}
		}
		InvokeRepeating("Yondegis", 0.3f, 0.3f);
		InvokeRepeating("KenarYondegis", 0.2f, 0.2f);
	}

	private void FixedUpdate()
	{
		if (CiftinLideri)
		{
			LR.SetPosition(0, KarsiMerkez.position);
			orta = (KarsiMerkez.position + KendiMerkz.position) * 0.5f;
			Vector3 vector = Vector3.Cross(KendiMerkz.position, Vector3.forward);
			Vector3 vector2 = Vector3.Cross(KendiMerkz.position, Vector3.back);
			vector.Normalize();
			vector2.Normalize();
			Vector3 position = 0.12f * vector2 + orta;
			ceyrek = (orta + KendiMerkz.position) * 0.5f;
			ceyrek2 = (orta + KarsiMerkez.position) * 0.5f;
			Vector3 position2 = 0.1f * vector + ceyrek;
			Vector3 position3 = 0.1f * vector + ceyrek2;
			Vector3 position4 = 0.1f * vector2 + ceyrek;
			Vector3 position5 = 0.1f * vector2 + ceyrek2;
			LR.SetPosition(3, position2);
			LR.SetPosition(1, position3);
			if (uste2)
			{
				LR.SetPosition(3, position4);
				LR.SetPosition(1, position3);
			}
			else
			{
				LR.SetPosition(3, position2);
				LR.SetPosition(1, position5);
			}
			if (uste)
			{
				LR.SetPosition(2, orta);
			}
			else
			{
				LR.SetPosition(2, position);
			}
			LR.SetPosition(4, KendiMerkz.position);
		}
		if (Dead)
		{
			return;
		}
		Vector3 position6 = PlayrTrans.position;
		if (CiftinLideri)
		{
			float num = Vector3.Distance(KendiMerkz.position, KarsiMerkez.position);
			position6.x = PlayrTrans.position.x - 1f;
			if (position6.x < base.transform.position.x)
			{
				Sol = true;
				Sag = false;
			}
			else if (KarsiMerkez.position.x - 5f > base.transform.position.x)
			{
				Sol = false;
				Sag = true;
			}
			else if (num < 1f)
			{
				Sol = true;
				Sag = false;
			}
			else if (KendiMerkz.position.y > PlayrTrans.position.y)
			{
				Sol = false;
				Sag = true;
			}
		}
		else
		{
			position6.x = PlayrTrans.position.x + 1f;
			if (position6.x > base.transform.position.x)
			{
				Sol = false;
				Sag = true;
			}
			if (KarsiMerkez.position.x - 5f > base.transform.position.x)
			{
				Sol = true;
				Sag = false;
			}
		}
		if (grounded && Aktif)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
			grounded = false;
		}
		Vector2 zero = Vector2.zero;
		if (Sag && Aktif)
		{
			if (grounded)
			{
				zero.x += moveForce;
			}
			else
			{
				zero.x += moveForce * 0.3f;
			}
			if (GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			{
				GetComponent<Rigidbody2D>().AddForce(zero);
			}
		}
		else if (Sol && Aktif)
		{
			if (grounded)
			{
				zero.x -= moveForce;
			}
			else
			{
				zero.x -= moveForce * 0.3f;
			}
			if (GetComponent<Rigidbody2D>().velocity.x > 0f - maxSpeed)
			{
				GetComponent<Rigidbody2D>().AddForce(zero);
			}
		}
	}

	private void Yondegis()
	{
		if (uste)
		{
			uste = false;
		}
		else
		{
			uste = true;
		}
	}

	private void KenarYondegis()
	{
		if (uste2)
		{
			uste2 = false;
		}
		else
		{
			uste2 = true;
		}
	}
}
