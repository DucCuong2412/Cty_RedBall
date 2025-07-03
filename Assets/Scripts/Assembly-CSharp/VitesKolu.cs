using UnityEngine;

public class VitesKolu : MonoBehaviour
{
	public string HedefBlokIsmi = "Kapak_1";

	private bool SoldaZaten;

	private bool SagdaZaten;

	public void Solda()
	{
		if (!SoldaZaten)
		{
			SagdaZaten = false;
			SoldaZaten = true;
			GameObject gameObject = GameObject.Find(HedefBlokIsmi);
			if (HedefBlokIsmi == "Miknatis")
			{
				gameObject.GetComponent<Miknatis>().Aktif = false;
			}
			else
			{
				gameObject.GetComponent<Kapi_X>().KayanSolaGit();
			}
		}
	}

	public void Sagda()
	{
		if (!SagdaZaten)
		{
			SoldaZaten = false;
			SagdaZaten = true;
			GameObject gameObject = GameObject.Find(HedefBlokIsmi);
			if (HedefBlokIsmi == "Miknatis")
			{
				gameObject.GetComponent<Miknatis>().Aktif = true;
			}
			else
			{
				gameObject.GetComponent<Kapi_X>().KayanSagaGit();
			}
		}
	}
}
