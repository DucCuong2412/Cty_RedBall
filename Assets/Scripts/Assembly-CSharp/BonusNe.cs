using UnityEngine;

public class BonusNe : MonoBehaviour
{
	private Vector3 SabitKonum = Vector3.zero;

	private bool Aldi;

	private SpriteRenderer SR;

	private Color gizlirenk = Color.white;

	private bool TweenBasladi;

	private Vector3 TweenYeri;

	private float TweenilkTime;

	private float TweenSuresi = 0.3f;

	private Vector3 TweenTarget;

	private BoxCollider2D BoxCol;

	protected bool Salinir = true;

	protected virtual void Ozelislem(GameObject go)
	{
	}

	private void Awake()
	{
		gizlirenk.a = 0f;
		SabitKonum = base.transform.position;
		SR = GetComponent<SpriteRenderer>();
		BoxCol = GetComponent<BoxCollider2D>();
		if (SabitKonum.x == 0f)
		{
			MonoBehaviour.print("dummy");
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!Aldi && (col.gameObject.tag == "Player" || col.gameObject.name == "takipli"))
		{
			Ozelislem(col.gameObject);
			CameraFollow component = Camera.main.GetComponent<CameraFollow>();
			if ((bool)component)
			{
				component.BonusSes();
			}
			TweenBasladi = true;
			TweenYeri = base.transform.position;
			TweenilkTime = Time.time;
			TweenTarget = base.transform.position;
			TweenTarget.y += 1.7f;
			Aldi = true;
			BoxCol.enabled = false;
		}
	}

	private void FixedUpdate()
	{
		if (TweenBasladi)
		{
			float t = (Time.time - TweenilkTime) / TweenSuresi;
			base.transform.position = Vector3.Lerp(TweenYeri, TweenTarget, t);
			SR.color = Color.Lerp(Color.white, gizlirenk, t);
			if (base.transform.position.y > TweenTarget.y - 0.01f)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	public void SabitKonumDegis(Vector3 nere)
	{
		SabitKonum = nere;
	}
}
