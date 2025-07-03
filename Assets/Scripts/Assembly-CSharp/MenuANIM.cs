using UnityEngine;

public class MenuANIM : MonoBehaviour
{
	public enum YonelimNe
	{
		Ustten = 1,
		Sagdan = 2,
		Asagidan = 3,
		Soldan = 4,
		OrtadaCik = 5
	}

	private static bool MenuAcik;

	public int AnimDurum;

	public bool Aktif;

	private float speed = 0.5f;

	public float Uzaklik = 10f;

	private float ilkZaman;

	private Vector3 ilkKonum;

	private Vector3 sonKonum;

	public YonelimNe HareketSekli = YonelimNe.Sagdan;

	private void Start()
	{
		Vector3 position = Vector3.one * 10f;
		position.z = 1f;
		base.transform.position = position;
	}

	public void MenuOrtala()
	{
		MenuAcik = true;
		Player.MenuAcik = true;
		sonKonum = (ilkKonum = Vector3.zero);
		ilkKonum.z = (sonKonum.z = 1f);
		Aktif = true;
		AnimDurum = 3;
		base.transform.position = ilkKonum;
		Time.timeScale = 0f;
		base.gameObject.SetActive(true);
	}

	public void AnimGetir()
	{
		if (!MenuAcik && AnimDurum != 3)
		{
			sonKonum = (ilkKonum = Vector3.zero);
			ilkKonum.z = (sonKonum.z = 1f);
			Aktif = true;
			AnimDurum = 1;
			ilkZaman = Time.realtimeSinceStartup;
			switch (HareketSekli)
			{
			case YonelimNe.Ustten:
				ilkKonum.y = Uzaklik;
				break;
			case YonelimNe.Sagdan:
				ilkKonum.x = Uzaklik;
				break;
			case YonelimNe.Asagidan:
				ilkKonum.y = 0f - Uzaklik;
				break;
			case YonelimNe.Soldan:
				ilkKonum.x = 0f - Uzaklik;
				break;
			}
			base.transform.localPosition = ilkKonum;
			Time.timeScale = 0f;
			base.gameObject.SetActive(true);
			Player.MenuAcik = true;
		}
	}

	public void AnimGotur()
	{
		MenuAcik = false;
		Player.MenuAcik = false;
		AnimDurum = 2;
		ilkZaman = Time.realtimeSinceStartup;
		ilkKonum = (sonKonum = Vector3.zero);
		ilkKonum.z = (sonKonum.z = 1f);
		switch (HareketSekli)
		{
		case YonelimNe.Ustten:
			sonKonum.y = Uzaklik + 1f;
			break;
		case YonelimNe.Sagdan:
			sonKonum.x = Uzaklik + 1f;
			break;
		case YonelimNe.Asagidan:
			sonKonum.y = 0f - Uzaklik - 1f;
			break;
		case YonelimNe.Soldan:
			sonKonum.x = 0f - Uzaklik - 1f;
			break;
		}
	}

	private void Update()
	{
		if (Aktif)
		{
			MenuAnim();
		}
	}

	private void MenuAnim()
	{
		if (AnimDurum <= 0)
		{
			return;
		}
		if (AnimDurum == 3 && base.transform.localPosition == sonKonum)
		{
			AnimDurum = 0;
			return;
		}
		float t = (Time.realtimeSinceStartup - ilkZaman) / speed;
		base.transform.localPosition = Vector3.Lerp(ilkKonum, sonKonum, t);
		if (AnimDurum == 1)
		{
			switch (HareketSekli)
			{
			case YonelimNe.Sagdan:
				if (base.transform.localPosition.x <= 0f)
				{
					AnimDurum = 3;
				}
				break;
			case YonelimNe.Soldan:
				if (base.transform.localPosition.x >= 0f)
				{
					if (base.name.Contains("menuMarket"))
					{
						GetComponent<MARKETsc>().ParaGuncelle();
					}
					AnimDurum = 3;
				}
				break;
			case YonelimNe.Ustten:
				if (base.transform.localPosition.y <= 0f)
				{
					if (base.name.Contains("menuWin"))
					{
						GetComponent<menuWINsc>().StarBaslat();
					}
					AnimDurum = 3;
				}
				break;
			case YonelimNe.Asagidan:
				if (base.transform.localPosition.y >= 0f)
				{
					if (base.name == "menuCPoints")
					{
						GetComponent<MenuCPointSc>().StarBaslat();
					}
					if (base.name == "menuLost")
					{
						GetComponent<menuLOSTsc>().ParaGuncelle();
					}
					AnimDurum = 3;
				}
				break;
			}
		}
		if (AnimDurum != 2)
		{
			return;
		}
		switch (HareketSekli)
		{
		case YonelimNe.Ustten:
			if (base.transform.localPosition.y > Uzaklik * 0.9f)
			{
				Aktif = false;
				AnimDurum = 0;
				base.gameObject.SetActive(false);
			}
			break;
		case YonelimNe.Sagdan:
			if (base.transform.localPosition.x > Uzaklik * 0.9f)
			{
				Aktif = false;
				AnimDurum = 0;
				base.gameObject.SetActive(false);
			}
			break;
		case YonelimNe.Asagidan:
			if (base.transform.localPosition.y < (0f - Uzaklik) * 0.9f)
			{
				Aktif = false;
				AnimDurum = 0;
				base.gameObject.SetActive(false);
			}
			break;
		case YonelimNe.Soldan:
			if (base.transform.localPosition.x < (0f - Uzaklik) * 0.9f)
			{
				Aktif = false;
				AnimDurum = 0;
				base.gameObject.SetActive(false);
			}
			break;
		}
	}
}
