using UnityEngine;

public class marketElle : MonoBehaviour
{
	private GameObject elle2;

	private float ilkZaman;

	private SpriteRenderer SR;

	private int durum;

	private void Start()
	{
		elle2 = base.transform.GetChild(0).gameObject;
		elle2.SetActive(false);
		ilkZaman = Time.realtimeSinceStartup;
		SR = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		float num = Time.realtimeSinceStartup - ilkZaman;
		if (num > 0.5f && durum == 0)
		{
			SR.enabled = false;
			elle2.SetActive(true);
			durum = 1;
		}
		if (num > 1f && durum == 1)
		{
			ilkZaman = Time.realtimeSinceStartup;
			elle2.SetActive(false);
			SR.enabled = true;
			durum = 0;
		}
	}
}
