using UnityEngine;

public class Portlek : MonoBehaviour
{
	private GameObject A1;

	private GameObject A2;

	private GameObject A3;

	private GameObject G1;

	private GameObject G2;

	private Transform RedTrans;

	private bool Living = true;

	public float atmaGucu = 5f;

	public float KirilmaHizi = 7f;

	private bool Agridi;

	private void Start()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Agiz1")
			{
				A1 = gameObject.gameObject;
			}
			if (gameObject.name == "Agiz2")
			{
				A2 = gameObject.gameObject;
				A2.SetActive(false);
			}
			if (gameObject.name == "Agiz3")
			{
				A3 = gameObject.gameObject;
				A3.SetActive(false);
			}
			if (gameObject.name == "acikGoz")
			{
				G1 = gameObject.gameObject;
			}
			if (gameObject.name == "kapaliGoz")
			{
				G2 = gameObject.gameObject;
				G2.SetActive(false);
			}
		}
		RedTrans = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void FixedUpdate()
	{
		if (!Living || Agridi)
		{
			return;
		}
		float num = Vector3.Distance(RedTrans.position, base.transform.position);
		if (num < 2f)
		{
			if (!A2.activeInHierarchy)
			{
				A2.SetActive(true);
				A1.SetActive(false);
			}
		}
		else if (!A1.activeInHierarchy)
		{
			A2.SetActive(false);
			A1.SetActive(true);
		}
	}

	private void OnCollisionStay2D(Collision2D sutun)
	{
		if (!Living)
		{
			return;
		}
		ContactPoint2D[] contacts = sutun.contacts;
		for (int i = 0; i < contacts.Length; i++)
		{
			ContactPoint2D contactPoint2D = contacts[i];
			if (contactPoint2D.collider.gameObject.layer != LayerMask.NameToLayer("zemin") && contactPoint2D.point.y < base.transform.position.y && base.gameObject.GetComponent<Rigidbody2D>().velocity.y < 5f && sutun.transform.position.y < base.transform.position.y)
			{
				base.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, atmaGucu);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if ((kim.gameObject.tag == "hareketli" || kim.gameObject.tag == "Player") && (Mathf.Abs(kim.relativeVelocity.x) > KirilmaHizi || Mathf.Abs(kim.relativeVelocity.y) > KirilmaHizi))
		{
			Agridi = true;
			Invoke("AgriGecti", 2f);
			A1.SetActive(false);
			A2.SetActive(false);
			A3.SetActive(true);
			G1.SetActive(false);
			G2.SetActive(true);
		}
	}

	private void AgriGecti()
	{
		Agridi = false;
		A1.SetActive(true);
		A2.SetActive(false);
		A3.SetActive(false);
		G1.SetActive(true);
		G2.SetActive(false);
	}
}
