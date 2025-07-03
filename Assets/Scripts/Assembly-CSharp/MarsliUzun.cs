using UnityEngine;

public class MarsliUzun : MonoBehaviour
{
	private Animator ani;

	private bool YonSaga = true;

	private float sag;

	private float sol;

	private bool ZatenOrda;

	private bool Uyuyor = true;

	private float DurmaHizi = 4.5f;

	public float Hiz = 1f;

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (!Uyuyor && kim.gameObject.tag == "Player" && (Mathf.Abs(kim.relativeVelocity.x) > DurmaHizi || Mathf.Abs(kim.relativeVelocity.y) > DurmaHizi))
		{
			ani.SetInteger("durum", 2);
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}

	public void Oldur()
	{
		if (!Uyuyor)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().isKinematic = false;
			ani.SetInteger("durum", 2);
		}
	}

	private void Uyan()
	{
		Uyuyor = false;
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (Uyuyor && kim.gameObject.tag == "Player")
		{
			float x = kim.gameObject.transform.position.x;
			if (x < base.transform.position.x)
			{
				YonSaga = false;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
				ani.SetInteger("durum", 3);
			}
			else
			{
				YonSaga = true;
				GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
				ani.SetInteger("durum", 4);
			}
			Invoke("Uyan", 0.2f);
		}
	}

	private void Awake()
	{
		ani = GetComponent<Animator>();
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Sol")
			{
				sol = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "Sag")
			{
				sag = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
		}
	}

	private void FixedUpdate()
	{
		if (Uyuyor)
		{
			return;
		}
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
			if (!ZatenOrda)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
				ZatenOrda = true;
				ani.SetInteger("durum", 4);
			}
		}
		else if (ZatenOrda)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
			ZatenOrda = false;
			ani.SetInteger("durum", 3);
		}
	}
}
