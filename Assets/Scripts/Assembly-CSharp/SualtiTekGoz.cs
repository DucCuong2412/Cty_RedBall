using UnityEngine;

public class SualtiTekGoz : MonoBehaviour
{
	private float Ust;

	private float ilkPos;

	private GameObject Goz;

	private GameObject sac1;

	private GameObject sac2;

	private bool Aktif;

	private Animator ani;

	public float Hiz = 2f;

	public float UyanmaSuresi = 2f;

	public float UykuSuresi = 3f;

	private bool bittim;

	public void Oldur()
	{
		ani.SetBool("uyu", true);
		Aktif = false;
		Goz.SetActive(false);
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().freezeRotation = false;
		GetComponent<Rigidbody2D>().AddTorque(100f);
		bittim = true;
	}

	private void Awake()
	{
		ani = GetComponent<Animator>();
		ilkPos = base.transform.position.y;
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Ust")
			{
				Ust = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "goz")
			{
				Goz = gameObject;
				Goz.SetActive(false);
			}
			if (gameObject.name == "sac1")
			{
				sac1 = gameObject;
				sac1.SetActive(false);
			}
			if (gameObject.name == "sac2")
			{
				sac2 = gameObject;
				sac2.SetActive(false);
			}
		}
	}

	private void Start()
	{
		Aktif = false;
		ani.SetBool("uyu", true);
		Invoke("Uyan", UykuSuresi);
	}

	private void Uyan()
	{
		if (!bittim)
		{
			Aktif = true;
			ani.SetBool("uyu", false);
			Invoke("Uyu", UyanmaSuresi);
		}
	}

	private void Uyu()
	{
		if (!bittim)
		{
			Invoke("Uyan", UykuSuresi);
		}
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		if (base.transform.position.y > Ust)
		{
			ani.SetBool("uyu", true);
			Aktif = false;
			Goz.SetActive(false);
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		if (base.transform.position.y == ilkPos)
		{
			GetComponent<Rigidbody2D>().isKinematic = true;
		}
		if (Aktif)
		{
			if (!Goz.activeInHierarchy)
			{
				Goz.SetActive(true);
			}
			if (base.transform.position.y < ilkPos + 0.01f)
			{
				Vector3 position = base.transform.position;
				position.y += 0.01f;
				base.transform.position = position;
			}
			GetComponent<Rigidbody2D>().velocity = Vector2.up * Hiz;
		}
	}
}
