using UnityEngine;

public class ButonKime : MonoBehaviour
{
	public enum BasincaNe
	{
		Ac = 1,
		Kapat = 2,
		KapidaChildSil = 3,
		AsansorYanAktif = 4,
		AsansorUstAktif = 5,
		DonenAktif = 6,
		BlokSagaGit = 7,
		BlokSolaGit = 8,
		BlokAsagiGit = 9,
		BlokYukariGit = 10,
		GravityYukari = 11,
		GravityYarim = 12,
		GravityNormal = 13,
		GravityTersYarim = 14
	}

	public string KapiIsmi = "Kapi_1";

	public BasincaNe BasincaNolacak = BasincaNe.Ac;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "lamba")
			{
			}
		}
	}

	public void Basti()
	{
		GameObject gameObject = GameObject.Find(KapiIsmi);
		if (gameObject == null)
		{
			Debug.Log("KAPIYI Ayarla ve iki Script ata ! ismi: " + KapiIsmi);
			return;
		}
		if (BasincaNolacak == BasincaNe.Ac)
		{
			MonoBehaviour.print("ac");
			gameObject.GetComponent<Kapi_X>().Ac();
		}
		if (BasincaNolacak == BasincaNe.Kapat)
		{
			gameObject.GetComponent<Kapi_X>().Kapat();
		}
		if (BasincaNolacak == BasincaNe.AsansorYanAktif)
		{
			gameObject.GetComponent<Kapi_X>().AsansorYanAktif();
		}
		if (BasincaNolacak == BasincaNe.AsansorUstAktif)
		{
			gameObject.GetComponent<Kapi_X>().AsansorUstAktif();
		}
		if (BasincaNolacak == BasincaNe.DonenAktif)
		{
			MonoBehaviour.print("kim dön dedi kiii");
			gameObject.GetComponent<Kapi_X>().DonenAktifle();
		}
		if (BasincaNolacak == BasincaNe.BlokSagaGit)
		{
			gameObject.GetComponent<Kapi_X>().KayanSagaGit();
		}
		if (BasincaNolacak == BasincaNe.BlokSolaGit)
		{
			gameObject.GetComponent<Kapi_X>().KayanSolaGit();
		}
		if (BasincaNolacak == BasincaNe.BlokAsagiGit)
		{
			gameObject.GetComponent<Kapi_X>().KayanAsagiGit();
		}
		if (BasincaNolacak == BasincaNe.BlokYukariGit)
		{
			gameObject.GetComponent<Kapi_X>().KayanYukariGit();
		}
		if (BasincaNolacak == BasincaNe.KapidaChildSil)
		{
			GameObject gameObject2 = null;
			for (int num = gameObject.transform.childCount - 1; num >= 0; num--)
			{
				gameObject2 = gameObject.transform.GetChild(num).gameObject;
				gameObject2.gameObject.SetActive(false);
			}
		}
		if (BasincaNolacak == BasincaNe.GravityYukari)
		{
			Player.GravityTers = true;
			float y = 9.81f;
			Physics2D.gravity = new Vector2(0f, y);
		}
		if (BasincaNolacak == BasincaNe.GravityYarim)
		{
			Player.GravityTers = false;
			float y2 = -4.905f;
			Physics2D.gravity = new Vector2(0f, y2);
		}
		if (BasincaNolacak == BasincaNe.GravityTersYarim)
		{
			Player.GravityTers = true;
			float y3 = 4.905f;
			Physics2D.gravity = new Vector2(0f, y3);
		}
		if (BasincaNolacak == BasincaNe.GravityNormal)
		{
			Player.GravityTers = false;
			float y4 = -9.81f;
			Physics2D.gravity = new Vector2(0f, y4);
		}
	}

	public void RenkSil()
	{
	}
}
