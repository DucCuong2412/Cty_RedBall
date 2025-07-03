using UnityEngine;

public class Tutor2 : MonoBehaviour
{
	private bool bitti;

	private GameObject Ok1;

	private FontSprites SaniyyeFS;

	private int Sayac;

	private bool Ustunde;

	private void Start()
	{
		Ok1 = GameObject.Find("okkey22");
		Ok1.SetActive(false);
		GameObject gameObject = GameObject.Find("saniyeSay");
		if ((bool)gameObject)
		{
			SaniyyeFS = gameObject.GetComponent<FontSprites>();
		}
		InvokeRepeating("Sayiyor", 1f, 1f);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!bitti && kim.gameObject.tag == "Player" && !bitti)
		{
			Ustunde = true;
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (!bitti && kim.tag == "Player" && !bitti)
		{
			Ustunde = false;
			Sayac = 0;
			string kelime = Sayac.ToString();
			SaniyyeFS.MetinDegis(kelime);
		}
	}

	private void Sayiyor()
	{
		if (bitti || !Ustunde)
		{
			return;
		}
		Sayac++;
		if (Sayac >= 3)
		{
			bitti = true;
			Ok1.SetActive(true);
			GameObject gameObject = GameObject.Find("wall2");
			gameObject.SetActive(false);
			string kelime = Sayac.ToString();
			SaniyyeFS.MetinDegis(kelime);
			CameraFollow component = Camera.main.GetComponent<CameraFollow>();
			if ((bool)component)
			{
				component.TutorOKSesi();
			}
			Object.Destroy(this);
		}
		else
		{
			string kelime2 = Sayac.ToString();
			SaniyyeFS.MetinDegis(kelime2);
		}
	}
}
