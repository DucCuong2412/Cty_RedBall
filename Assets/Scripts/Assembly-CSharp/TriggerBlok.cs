using UnityEngine;

public class TriggerBlok : MonoBehaviour
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
		BlokYukariGit = 10
	}

	public string KapiIsmi = "Kapi_1";

	public BasincaNe BasincaNolacak = BasincaNe.Ac;

	private bool TetikOk;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!TetikOk && kim.gameObject.tag == "Player")
		{
			Tetikledi();
			TetikOk = true;
		}
	}

	public void Tetikledi()
	{
		GameObject gameObject = GameObject.Find(KapiIsmi);
		if (gameObject == null)
		{
			Debug.Log("KAPIYI Ayarla ve iki Script ata ! ismi: " + KapiIsmi);
			return;
		}
		if (BasincaNolacak == BasincaNe.Ac)
		{
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
	}
}
