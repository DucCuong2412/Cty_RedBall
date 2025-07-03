using UnityEngine;

public class KapiTrigger : MonoBehaviour
{
	public string KapiAdi = string.Empty;

	private bool Bulundu;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!Bulundu && col.gameObject.name.Contains("anahtar"))
		{
			Anahtar component = col.gameObject.GetComponent<Anahtar>();
			if (component.KapiTriggerAdi == KapiAdi)
			{
				component.HedefGoster(col.gameObject.transform.position, base.transform.position, true);
				Invoke("Tetikle", 1f);
				Bulundu = true;
			}
			else
			{
				MonoBehaviour.print("ANAHTAR ve TRİGGERDEKİ AYNI OLACAK :" + component.KapiTriggerAdi + ":" + KapiAdi);
			}
		}
	}

	private void Tetikle()
	{
		GameObject gameObject = GameObject.Find(KapiAdi);
		if (gameObject == null)
		{
			Debug.Log("KAPI Bulunamadı : " + KapiAdi);
			return;
		}
		Kapi_X component = gameObject.GetComponent<Kapi_X>();
		if (component == null)
		{
			Debug.Log("KAPIya Kapi_X Scripti ver :" + KapiAdi);
			return;
		}
		component.Ac();
		Object.Destroy(this);
	}
}
