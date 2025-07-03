using UnityEngine;

public class Tentacle : MonoBehaviour
{
	public bool YonSaga = true;

	public float Hiz = 0.6f;

	public float MaxKalmaSuresi = 5f;

	public float DusmeHizi = 15f;

	private bool bittim;

	private GameObject yapisti;

	private bool yapisok;

	private float LerpSpeed = 1f;

	private float LerpTime;

	private Vector3 LerpPos;

	private Vector3 LerpDest;

	private float moveForce;

	private float jumpforce;

	private Vector3 contaCT;

	private Player PL;

	private Rigidbody2D PLrig;

	private bool LerpBitti;

	private Player PlayerSc;

	private void Awake()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "yapisti")
			{
				yapisti = gameObject;
			}
		}
		yapisti.SetActive(false);
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	public void Oldur()
	{
		if (!yapisok)
		{
			Invoke("DisableEt", 2f);
			base.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
		}
	}

	private void DisableEt()
	{
		base.gameObject.SetActive(false);
	}

	private void BirakGitsin()
	{
		if ((bool)base.transform.parent)
		{
			if (PL.moveForce < 13f)
			{
				PL.moveForce += 1f;
				PL.jumpForce += 1f;
			}
			bittim = true;
			base.transform.position = base.transform.parent.position;
			base.transform.parent = null;
			GetComponent<Rigidbody2D>().freezeRotation = false;
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f);
			Invoke("DisableEt", 2f);
		}
	}

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (!yapisok && kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			CameraFollow component = Camera.main.GetComponent<CameraFollow>();
			if ((bool)component)
			{
				component.SulukSesi();
			}
			yapisok = true;
			base.transform.parent = kim.transform;
			Vector3 vector = kim.transform.InverseTransformPoint(kim.contacts[0].point);
			base.transform.localPosition = vector;
			Vector3 normalized = (vector - Vector3.zero).normalized;
			Vector3 vector2 = base.transform.position - kim.gameObject.transform.position;
			float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			base.transform.rotation = Quaternion.AngleAxis(num + 90f, Vector3.forward);
			base.gameObject.layer = LayerMask.NameToLayer("Oyuncu");
			base.gameObject.GetComponent<SpriteRenderer>().sprite = yapisti.gameObject.GetComponent<SpriteRenderer>().sprite;
			base.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
			base.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			LerpPos = base.transform.localPosition;
			LerpDest = base.transform.localPosition + normalized * -0.1f;
			PL = base.transform.parent.gameObject.GetComponent<Player>();
			if (PL.moveForce > 1f)
			{
				moveForce = PL.moveForce;
				PL.moveForce = moveForce - 1f;
				jumpforce = PL.jumpForce;
				PL.jumpForce = jumpforce - 1f;
			}
			PLrig = kim.gameObject.GetComponent<Rigidbody2D>();
		}
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		float num = Mathf.PingPong(Time.time, 0.2f);
		num += 0.8f;
		base.transform.localScale = new Vector3(1f, num, 1f);
		if ((yapisok || LerpBitti) && Mathf.Abs(PLrig.velocity.x) > DusmeHizi)
		{
			BirakGitsin();
		}
		if (LerpBitti)
		{
			return;
		}
		if (yapisok)
		{
			if (base.transform.localPosition == LerpDest)
			{
				LerpBitti = true;
				Invoke("BirakGitsin", MaxKalmaSuresi);
			}
			LerpTime += Time.deltaTime * LerpSpeed;
			base.transform.localPosition = Vector3.Lerp(LerpPos, LerpDest, LerpTime);
			return;
		}
		Vector3 position;
		Vector3 vector = (position = base.transform.position);
		vector.x += 0.4f;
		position.x -= 0.4f;
		if (YonSaga)
		{
			if ((bool)Physics2D.OverlapPoint(vector, 1 << LayerMask.NameToLayer("zemin")))
			{
				YonSaga = false;
			}
		}
		else if ((bool)Physics2D.OverlapPoint(position, 1 << LayerMask.NameToLayer("zemin")))
		{
			YonSaga = true;
		}
		if (YonSaga)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
	}
}
