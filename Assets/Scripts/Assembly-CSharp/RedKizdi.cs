using UnityEngine;

public class RedKizdi : MonoBehaviour
{
	private Transform RedTrans;

	private Transform KimTrans;

	private Player PlayerSc;

	public string KimeKizgin = "Boss";

	private bool Kizdik;

	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		PlayerSc = gameObject.GetComponent<Player>();
		RedTrans = gameObject.transform;
		GameObject gameObject2 = GameObject.Find(KimeKizgin);
		if ((bool)gameObject2)
		{
			KimTrans = gameObject2.transform;
		}
	}

	private void FixedUpdate()
	{
		float num = Vector3.Distance(KimTrans.position, RedTrans.position);
		if (num < 1.5f)
		{
			if (!Kizdik)
			{
				Kizdik = true;
				PlayerSc.KizginSurat();
			}
		}
		else if (num > 2f && Kizdik)
		{
			Kizdik = false;
			PlayerSc.KizginSuratNot();
		}
	}
}
