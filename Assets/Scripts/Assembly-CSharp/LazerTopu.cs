using UnityEngine;

public class LazerTopu : MonoBehaviour
{
	private HingeJoint2D hinge;

	private JointMotor2D moto;

	private Transform UcuTr;

	private GameObject cx;

	private GameObject YanikTR;

	private float UzunlukCarpan = 100f;

	private bool Buldu;

	private Vector3 hedef = Vector3.zero;

	private Color Renk = Color.white;

	private Color LazerR = Color.yellow;

	private SpriteRenderer SR;

	private bool Kilitlendi;

	private bool Geberdi;

	public bool Aktif = true;

	public float DaireCapi = 5f;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, DaireCapi);
	}

	private void Start()
	{
		hinge = GetComponent<HingeJoint2D>();
		moto = hinge.motor;
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Ucu")
			{
				UcuTr = gameObject.transform;
			}
			if (gameObject.name == "yanik")
			{
				YanikTR = gameObject;
			}
		}
		YanikTR.SetActive(false);
		Renk.a = 0.3f;
		LazerR.a = 0.5f;
		cx = Object.Instantiate(Resources.Load("Nokta", typeof(GameObject))) as GameObject;
		SR = cx.GetComponent<SpriteRenderer>();
		SR.sortingOrder = 8;
		SR.sortingLayerName = "Oyun";
	}

	private void FixedUpdate()
	{
		if (!Aktif || Geberdi)
		{
			return;
		}
		if (hinge.jointAngle < -39f)
		{
			moto.motorSpeed = 20f;
			hinge.motor = moto;
		}
		if (hinge.jointAngle > 89f)
		{
			moto.motorSpeed = -20f;
			hinge.motor = moto;
		}
		Vector3 normalized = (UcuTr.position - base.transform.position).normalized;
		if (!Kilitlendi)
		{
			if (Buldu)
			{
				SR.color = LazerR;
				cx.SetActive(true);
				CizgiCiz(UcuTr.position, hedef, 10f);
				Invoke("Yapisti", 0.2f);
			}
			else
			{
				SR.color = Renk;
				cx.SetActive(false);
				CizgiCiz(UcuTr.position, base.transform.position + normalized * 5f, 3f);
			}
		}
	}

	private void SonDurum()
	{
		if (Geberdi)
		{
			return;
		}
		Vector2 direction = (UcuTr.position - base.transform.position).normalized;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, direction, DaireCapi, 1 << LayerMask.NameToLayer("Oyuncu"));
		if (raycastHit2D.collider != null)
		{
			Player component = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			if (!component.KalkanAktif)
			{
				MonoBehaviour.print("sondurum");
				component.GeberSurat();
				Player.Geberdi = true;
				Geberdi = true;
				YanikTR.SetActive(true);
				SR.color = Color.red;
				GameObject gameObject = Object.Instantiate(YanikTR);
				gameObject.transform.position = raycastHit2D.point;
				gameObject.transform.parent = raycastHit2D.collider.gameObject.transform;
				GameObject gameObject2 = Object.Instantiate(YanikTR);
				gameObject2.transform.position = raycastHit2D.point;
			}
			else
			{
				Kilitlendi = (Buldu = false);
				hinge.useMotor = true;
				GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
		else
		{
			Kilitlendi = (Buldu = false);
			hinge.useMotor = true;
			GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}

	private void Yapisti()
	{
		if (!Geberdi)
		{
			Vector2 direction = (UcuTr.position - base.transform.position).normalized;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, direction, DaireCapi, 1 << LayerMask.NameToLayer("Oyuncu"));
			if (raycastHit2D.collider != null)
			{
				hedef = raycastHit2D.point;
				CizgiCiz(UcuTr.position, hedef, 10f);
				Kilitlendi = true;
				hinge.useMotor = false;
				GetComponent<Rigidbody2D>().isKinematic = true;
				Invoke("SonDurum", 0.2f);
			}
		}
	}

	private void Update()
	{
		if (Geberdi)
		{
			YanikTR.transform.Rotate(0f, 0f, 10f);
		}
		else if (!Kilitlendi)
		{
			Vector2 direction = (UcuTr.position - base.transform.position).normalized;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, direction, DaireCapi, 1 << LayerMask.NameToLayer("Oyuncu"));
			if (raycastHit2D.collider != null)
			{
				hedef = raycastHit2D.point;
				Buldu = true;
			}
			else
			{
				Buldu = false;
			}
		}
	}

	private void CizgiCiz(Vector3 from, Vector3 to, float Genislik)
	{
		cx.SetActive(true);
		Vector3 vector = to - from;
		Vector3 position = from + vector / 2f;
		position.z = 0f;
		cx.transform.position = position;
		cx.transform.up = vector.normalized;
		cx.transform.localScale = new Vector3(Genislik, vector.magnitude * UzunlukCarpan, Genislik);
	}
}
