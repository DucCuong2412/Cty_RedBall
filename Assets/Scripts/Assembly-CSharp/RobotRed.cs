using UnityEngine;

public class RobotRed : Pooler
{
	private Transform RedT;

	private bool Yakin;

	private SpriteRenderer GozSR;

	private float jumpForce = 6.7f;

	private bool grounded;

	public bool Ziplamaz;

	public float HIZ = 20f;

	public float AtmaSuresi = 1f;

	public float DaireCapi = 3f;

	private bool bittim;

	public void Oldur()
	{
		GozSR.color = Color.grey;
		bittim = true;
		GetComponent<Rigidbody2D>().AddTorque(-20f);
	}

	private void Awake()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "ates")
			{
				PoolerGo = gameObject;
			}
			if (gameObject.name == "gozler")
			{
				GozSR = gameObject.GetComponent<SpriteRenderer>();
			}
		}
		PoolerAwake();
		RedT = GameObject.FindGameObjectWithTag("Player").transform;
		InvokeRepeating("Firlat", AtmaSuresi, AtmaSuresi);
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		float num = Vector3.Distance(RedT.position, base.transform.position);
		if (num < DaireCapi)
		{
			Yakin = true;
			if (GozSR.color != Color.red)
			{
				GozSR.color = Color.red;
			}
		}
		else
		{
			Yakin = false;
			if (GozSR.color != Color.white)
			{
				GozSR.color = Color.white;
			}
		}
		if (Yakin)
		{
			Vector2 end = base.transform.position;
			end.y -= 0.441f;
			grounded = Physics2D.Linecast(base.transform.position, end, 1 << LayerMask.NameToLayer("zemin"));
			if (grounded && !Ziplamaz)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
				grounded = false;
			}
		}
	}

	private void Firlat()
	{
		if (Yakin && !bittim)
		{
			GameObject gameObject = DepodanAl();
			if (!(gameObject == null))
			{
				gameObject.SetActive(true);
				gameObject.GetComponent<RobotRedFire>();
				Vector2 vector = default(Vector2);
				vector.x = RedT.position.x - base.transform.position.x;
				vector.y = RedT.position.y - base.transform.position.y;
				vector.Normalize();
				gameObject.transform.position = base.transform.position;
				gameObject.GetComponent<Rigidbody2D>().velocity = vector * HIZ;
				gameObject.GetComponent<RobotRedFire>().Gizleyici();
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(base.transform.position, DaireCapi);
	}
}
