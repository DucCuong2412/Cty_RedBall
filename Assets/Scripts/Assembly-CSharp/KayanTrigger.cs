using UnityEngine;

public class KayanTrigger : MonoBehaviour
{
	public enum Amac
	{
		YukariGotur = 0,
		SagaGotur = 1,
		AsagiGotur = 2,
		Solagotur = 3
	}

	public string KayanBlokAdi = string.Empty;

	public float AnahtarLerpSuresi = 2f;

	private bool Bulundu;

	public Amac NeYapacak = Amac.AsagiGotur;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!Bulundu && col.gameObject.name.Contains("anahtar"))
		{
			Anahtar component = col.gameObject.GetComponent<Anahtar>();
			if (component.KapiTriggerAdi == base.name)
			{
				component.HedefGoster(col.gameObject.transform.position, base.transform.position, true);
				Invoke("Tetikle", AnahtarLerpSuresi);
				Bulundu = true;
			}
			else
			{
				MonoBehaviour.print("ANAHTAR TRİGGERi BİLİR TRİGGER KAPIYI :" + component.KapiTriggerAdi + ":" + KayanBlokAdi);
			}
		}
	}

	private void Tetikle()
	{
		GameObject gameObject = GameObject.Find(KayanBlokAdi);
		if (gameObject == null)
		{
			Debug.Log("KAPI Bulunamadı : " + KayanBlokAdi);
			return;
		}
		if (NeYapacak == Amac.AsagiGotur)
		{
			gameObject.GetComponent<KayanBlkAsagi>().AsagiGit();
		}
		if (NeYapacak == Amac.YukariGotur)
		{
			gameObject.GetComponent<KayanBlkAsagi>().YukariGit();
		}
		if (NeYapacak == Amac.SagaGotur)
		{
			gameObject.GetComponent<KayanBlok>().SagaGit();
		}
		if (NeYapacak == Amac.Solagotur)
		{
			gameObject.GetComponent<KayanBlok>().SolaGit();
		}
	}
}
