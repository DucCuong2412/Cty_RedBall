using UnityEngine;

public class UzayanBlok : MonoBehaviour
{
	public float UzunBoyu = 200f;

	public float KisaBoyu = 50f;

	public float UzamaSuresi = 5f;

	public float KisalmaSuresi = 5f;

	private BoxCollider2D col2D;

	private bool Uzuyor;

	private bool Kisaliyor;

	private float ilkTime;

	private Vector3 nereden;

	private Vector3 nereye;

	private GameObject sag;

	private GameObject sol;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Sag")
			{
				sag = gameObject;
			}
			if (gameObject.name == "Sol")
			{
				sol = gameObject;
			}
		}
		Uzuyor = true;
		nereden = (nereye = base.transform.localScale);
	}

	private void FixedUpdate()
	{
		if (Uzuyor)
		{
			nereye.x = UzunBoyu;
			float t = (Time.time - ilkTime) / UzamaSuresi;
			base.transform.localScale = Vector3.Lerp(nereden, nereye, t);
			Vector3 one = Vector3.one;
			one.x = 1f / base.transform.localScale.x;
			sag.transform.localScale = one;
			sol.transform.localScale = one;
			if (base.transform.localScale.x == UzunBoyu)
			{
				ilkTime = Time.time;
				Kisaliyor = true;
				nereden = base.transform.localScale;
				Uzuyor = false;
			}
		}
		if (Kisaliyor)
		{
			nereye.x = KisaBoyu;
			float t2 = (Time.time - ilkTime) / KisalmaSuresi;
			base.transform.localScale = Vector3.Lerp(nereden, nereye, t2);
			Vector3 one2 = Vector3.one;
			one2.x = 1f / base.transform.localScale.x;
			sag.transform.localScale = one2;
			sol.transform.localScale = one2;
			if (base.transform.localScale.x == KisaBoyu)
			{
				ilkTime = Time.time;
				nereden = base.transform.localScale;
				Uzuyor = true;
				Kisaliyor = false;
			}
		}
	}
}
