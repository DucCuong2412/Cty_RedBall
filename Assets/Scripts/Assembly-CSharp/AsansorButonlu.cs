using UnityEngine;

public class AsansorButonlu : MonoBehaviour
{
	private float sol;

	private float sag;

	private bool gotur;

	public bool yonSaga;

	public bool Aktif = true;

	public float Hiz = 1f;

	private SliderJoint2D SolSlider;

	private SliderJoint2D SagSlider;

	private JointMotor2D moto;

	private SpriteRenderer SolLamba;

	private SpriteRenderer SagLamba;

	private void Start()
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
			if (gameObject.name == "dumeSol")
			{
				SolSlider = gameObject.GetComponent<SliderJoint2D>();
			}
			if (gameObject.name == "dumeSag")
			{
				SagSlider = gameObject.GetComponent<SliderJoint2D>();
			}
			if (gameObject.name == "lamba_sag")
			{
				SagLamba = gameObject.GetComponent<SpriteRenderer>();
			}
			if (gameObject.name == "lamba_sol")
			{
				SolLamba = gameObject.GetComponent<SpriteRenderer>();
			}
		}
		if (Aktif)
		{
			if (yonSaga)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
			}
		}
		moto = SagSlider.motor;
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (gotur && kim.tag == "Player")
		{
			kim.transform.parent = null;
			moto.motorSpeed = -10f;
			SagSlider.motor = moto;
			moto.motorSpeed = -10f;
			SolSlider.motor = moto;
		}
	}

	public void SolaGitEmri()
	{
		Aktif = true;
		yonSaga = false;
		moto.motorSpeed = 10f;
		SolSlider.motor = moto;
		moto.motorSpeed = -10f;
		SagSlider.motor = moto;
		GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
	}

	public void SagaGitEmri()
	{
		Aktif = true;
		yonSaga = true;
		moto.motorSpeed = 10f;
		SagSlider.motor = moto;
		moto.motorSpeed = -10f;
		SolSlider.motor = moto;
		GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
	}

	private void FixedUpdate()
	{
		if (!Aktif)
		{
			if (GetComponent<Rigidbody2D>().velocity.x != 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			return;
		}
		if (yonSaga)
		{
			if (GetComponent<Rigidbody2D>().velocity.x < 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
			}
			SagLamba.color = Color.yellow;
			SolLamba.color = Color.white;
		}
		else
		{
			if (GetComponent<Rigidbody2D>().velocity.x > 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
			}
			SagLamba.color = Color.white;
			SolLamba.color = Color.yellow;
		}
		if (base.transform.position.x < sol + 0.1f)
		{
			yonSaga = true;
		}
		if (base.transform.position.x > sag - 0.1f)
		{
			yonSaga = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!gotur && kim.tag == "Player")
		{
			kim.gameObject.transform.parent = base.transform;
			kim.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			gotur = true;
		}
	}
}
