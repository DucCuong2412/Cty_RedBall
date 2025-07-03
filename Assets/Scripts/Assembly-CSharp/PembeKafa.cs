using UnityEngine;

public class PembeKafa : MonoBehaviour
{
	private float moveForce = 5f;

	private float maxSpeed = 4.2f;

	private float jumpForce = 5.5f;

	public bool grounded;

	public bool Aktif;

	public bool Dead;

	private GameObject zeminkontrol;

	public bool facingRight = true;

	public bool Zipla;

	public bool Sag;

	public bool Sol;

	private Animator anim;

	private float Deep;

	private GameObject plyr;

	private bool Dustu;

	public void ReLive()
	{
		GetComponent<Rigidbody2D>().isKinematic = false;
		Dead = false;
		anim.SetBool("alive", true);
		anim.SetBool("uzgun", false);
		anim.SetBool("dead", false);
		grounded = false;
		Invoke("AliveFalse", 1f);
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().angularVelocity = 0f;
	}

	private void AliveFalse()
	{
		anim.SetBool("alive", false);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player")
		{
			Aktif = true;
		}
	}

	public void DeaktifYap()
	{
		Aktif = false;
	}

	public void DeadYap()
	{
		Aktif = false;
		Dead = true;
		anim.SetBool("dead", true);
		Camera.main.GetComponent<CameraFollow>().SadnessSesi();
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			Player component = gameObject.GetComponent<Player>();
			component.UzgunSurat();
		}
	}

	public void UzgunYap()
	{
		if (!Dead)
		{
			anim.SetBool("uzgun", true);
		}
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
		plyr = GameObject.FindGameObjectWithTag("Player");
		GameObject gameObject = GameObject.Find("DeepDead");
		if ((bool)gameObject)
		{
			Deep = gameObject.transform.position.y;
		}
	}

	private void Update()
	{
		if (Dead)
		{
			return;
		}
		Vector2 point = base.transform.position;
		point.y -= 0.5f;
		LayerMask layerMask = (1 << LayerMask.NameToLayer("haraketli")) | (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("Kirilan")) | (1 << LayerMask.NameToLayer("Robot")) | (1 << LayerMask.NameToLayer("Bocek")) | (1 << LayerMask.NameToLayer("zemin"));
		grounded = Physics2D.OverlapPoint(point, layerMask);
		if (Aktif)
		{
			if (plyr.transform.position.x < base.transform.position.x)
			{
				Sol = true;
				Sag = false;
			}
			else
			{
				Sol = false;
				Sag = true;
			}
			float num = Vector3.Distance(plyr.transform.position, base.transform.position);
			if (num > 2.8f)
			{
				Zipla = true;
			}
			else
			{
				Zipla = false;
			}
			if (num < 1f)
			{
				anim.SetBool("happy", true);
			}
			else
			{
				anim.SetBool("happy", false);
			}
		}
	}

	private void OnCollisionStay2D(Collision2D sutun)
	{
		if (!Aktif || grounded)
		{
			return;
		}
		ContactPoint2D[] contacts = sutun.contacts;
		for (int i = 0; i < contacts.Length; i++)
		{
			ContactPoint2D contactPoint2D = contacts[i];
			if (contactPoint2D.collider.gameObject.layer != LayerMask.NameToLayer("zemin") && contactPoint2D.point.y < base.transform.position.y)
			{
				grounded = true;
			}
		}
	}

	private void FixedUpdate()
	{
		if (base.transform.position.y < Deep - 10f && !Dustu)
		{
			MonoBehaviour.print("dead 1");
			DeadYap();
			GetComponent<Rigidbody2D>().isKinematic = true;
			Dustu = true;
		}
		if (Dead)
		{
			return;
		}
		if (base.transform.position.y < Deep)
		{
			Dead = true;
			MonoBehaviour.print("dead 2");
			if ((bool)plyr)
			{
				Player component = plyr.GetComponent<Player>();
				component.UzgunSurat();
			}
		}
		if (Zipla && grounded)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
			grounded = false;
		}
		float num = Vector3.Distance(plyr.transform.position, base.transform.position);
		Vector2 zero = Vector2.zero;
		if (Sag)
		{
			if (facingRight)
			{
				Flip();
			}
			if (grounded)
			{
				zero.x += moveForce;
			}
			else
			{
				zero.x += moveForce * 0.2f;
			}
			if (GetComponent<Rigidbody2D>().velocity.x < maxSpeed && num > 1f)
			{
				GetComponent<Rigidbody2D>().AddForce(zero);
			}
		}
		else if (Sol)
		{
			if (!facingRight)
			{
				Flip();
			}
			if (grounded)
			{
				zero.x -= moveForce;
			}
			else
			{
				zero.x -= moveForce * 0.2f;
			}
			if (GetComponent<Rigidbody2D>().velocity.x > 0f - maxSpeed && num > 1f)
			{
				GetComponent<Rigidbody2D>().AddForce(zero);
			}
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
	}
}
