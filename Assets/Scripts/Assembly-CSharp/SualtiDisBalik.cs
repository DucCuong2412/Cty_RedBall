using UnityEngine;

public class SualtiDisBalik : MonoBehaviour
{
	public bool YonSaga;

	public float Hiz = 1f;

	private float sol;

	private float sag;

	private bool SagaBakiyor;

	private bool bittim;

	private Player PlayerSc;

	private void Awake()
	{
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
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	public void Oldur()
	{
		if (!bittim)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			bittim = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			Player.Geberdi = true;
			PlayerSc.GeberSurat();
		}
	}

	private void Flip()
	{
		SagaBakiyor = !SagaBakiyor;
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		Vector3 position = base.transform.position;
		position.y += Mathf.Sin(Time.time) * Time.deltaTime * 0.3f;
		base.transform.position = position;
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
			if (!SagaBakiyor)
			{
				Flip();
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			if (SagaBakiyor)
			{
				Flip();
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
	}
}
