using UnityEngine;

public class ParaSc : MonoBehaviour
{
	private bool gitti;

	private static Color gizlirenk = Color.white;

	private SpriteRenderer SR;

	private bool TweenBasladi;

	private Vector3 TweenYeri;

	private float TweenilkTime;

	private float TweenSuresi = 0.3f;

	private Vector3 TweenTarget;

	private void Start()
	{
		gizlirenk.a = 0f;
		SR = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!gitti && (col.gameObject.tag == "Player" || col.gameObject.name == "takipli"))
		{
			GetComponent<BoxCollider2D>().enabled = false;
			if (col.gameObject.name == "takipli")
			{
				col.gameObject.GetComponent<Takipli>().ParaAlindi();
			}
			gitti = true;
			TweenBasladi = true;
			TweenYeri = base.transform.position;
			TweenilkTime = Time.time;
			TweenTarget = base.transform.position;
			TweenTarget.y += 1.7f;
		}
	}

	private void FixedUpdate()
	{
		if (TweenBasladi)
		{
			float t = (Time.time - TweenilkTime) / TweenSuresi;
			base.transform.position = Vector3.Lerp(TweenYeri, TweenTarget, t);
			SR.color = Color.Lerp(Color.white, gizlirenk, t);
			if (base.transform.position.y > TweenTarget.y - 0.1f)
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
