using UnityEngine;

public class BlokTrigger : MonoBehaviour
{
	public enum BasincaNe
	{
		BlokSagaGit = 1,
		BlokSolaGit = 2,
		BlokAsagiGit = 3,
		BlokYukariGit = 4
	}

	public string BlokIsmi = "Blok_1";

	public BasincaNe BasincaNolacak = BasincaNe.BlokYukariGit;

	public void Basti()
	{
		GameObject gameObject = GameObject.Find(BlokIsmi);
		if (gameObject == null)
		{
			Debug.Log("BLOK Ayarla ve iki Script ata ! ismi: " + BlokIsmi);
			return;
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
	}
}
