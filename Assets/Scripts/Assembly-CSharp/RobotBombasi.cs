using UnityEngine;

public class RobotBombasi : MonoBehaviour
{
	private Animator ani;

	private Transform Robotu;

	private Quaternion Kendi;

	private float DaireCapi = 1.5f;

	public float HIZ = 1f;

	private Player PlayerSc;

	private bool pinkdead;

	public void Sifirla()
	{
		pinkdead = false;
	}

	private void Awake()
	{
		ani = GetComponent<Animator>();
		ani.enabled = true;
		Robotu = base.transform.parent;
		Kendi = base.transform.rotation;
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void Resetle()
	{
		ani.Rebind();
		ani.SetBool("patliyor", false);
		base.transform.rotation = Kendi;
		base.transform.parent = Robotu;
		base.transform.parent.GetComponent<RobotPervBombaci>().BombaGeliyor();
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().isKinematic = true;
		base.transform.localPosition = new Vector3(-0.004197598f, -0.7175044f, 0f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, DaireCapi);
	}

	private void PatlamaBaslat()
	{
		ani.SetBool("patliyor", true);
		Invoke("Resetle", 0.4f);
		LayerMask layerMask = (1 << LayerMask.NameToLayer("haraketli")) | (1 << LayerMask.NameToLayer("Oyuncu"));
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, DaireCapi, layerMask);
		Collider2D[] array2 = array;
		Vector2 vector = default(Vector2);
		foreach (Collider2D collider2D in array2)
		{
			Rigidbody2D component = collider2D.gameObject.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				vector.x = collider2D.gameObject.transform.position.x - base.transform.position.x;
				vector.y = collider2D.gameObject.transform.position.y - base.transform.position.y + 0.3f;
				component.velocity = vector.normalized * HIZ;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			PlayerSc.GeberSurat();
			Player.Geberdi = true;
		}
		if (kim.gameObject.name == "PinkBall" && !pinkdead)
		{
			PembeKafa component = GameObject.Find("PinkBall").GetComponent<PembeKafa>();
			component.DeadYap();
			pinkdead = true;
		}
		if (kim.gameObject.tag == "Piti")
		{
			kim.gameObject.GetComponent<Piti>().DikenBatti();
		}
		Invoke("PatlamaBaslat", 0.1f);
	}
}
