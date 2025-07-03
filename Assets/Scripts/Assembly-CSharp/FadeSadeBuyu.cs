using UnityEngine;

public class FadeSadeBuyu : MonoBehaviour
{
	public float HedefBoyut = 1.5f;

	public float Hiz = 1f;

	private Vector3 baseScale;

	private SpriteRenderer sR;

	private Color sonRenk;

	private Vector3 hedefScale;

	private void Start()
	{
		hedefScale = base.transform.localScale * HedefBoyut;
		sR = GetComponent<SpriteRenderer>();
		sonRenk = sR.color;
		sonRenk.a = 0f;
	}

	private void Update()
	{
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, hedefScale, Hiz * Time.deltaTime);
		sR.color = Color.Lerp(sR.color, sonRenk, Hiz * Time.deltaTime);
		if ((double)sR.color.a < 0.05)
		{
			base.gameObject.SetActive(false);
		}
	}
}
