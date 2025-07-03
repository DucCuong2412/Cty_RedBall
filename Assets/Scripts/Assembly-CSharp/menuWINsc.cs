using UnityEngine;
using UnityEngine.SceneManagement;

public class menuWINsc : MonoBehaviour
{
	private bool YildizYaptik;

	private GameObject[] Aktifler = new GameObject[3];

	private GameObject[] Pasifler = new GameObject[3];

	private FontSprites FSzaman;

	private FontSprites FSstar;

	private FontSprites FSmoney;

	private GameObject TekStar;

	private int Yildiztoplam;

	private int CtoplamStar;

	private int TopMoney;

	private float ilkZaman;

	private float araZaman;

	private float araZaman2;

	private Vector3 ilkKonum;

	private int HangiStar;

	private float sspeed = 0.3f;

	private GameObject[] miniStars = new GameObject[5];

	private Vector3[] SonkonumSs = new Vector3[5]
	{
		new Vector3(0f, 0.004f, 0f),
		new Vector3(0.00365f, 0.0012f, 0f),
		new Vector3(0.00234f, -0.003f, 0f),
		new Vector3(-0.0021f, -0.0031f, 0f),
		new Vector3(-0.00333f, 0.0011f, 0f)
	};

	private int starsay;

	private int goldSay;

	private void Start()
	{
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		GameObject gameObject3 = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject3 = base.transform.GetChild(num).gameObject;
			if (gameObject3.name == "starSdon1")
			{
				Aktifler[0] = gameObject3;
			}
			if (gameObject3.name == "starSdon2")
			{
				Aktifler[1] = gameObject3;
			}
			if (gameObject3.name == "starSdon3")
			{
				Aktifler[2] = gameObject3;
			}
			if (gameObject3.name == "starSyok1")
			{
				Pasifler[0] = gameObject3;
			}
			if (gameObject3.name == "starSyok2")
			{
				Pasifler[1] = gameObject3;
			}
			if (gameObject3.name == "starSyok3")
			{
				Pasifler[2] = gameObject3;
			}
			if (gameObject3.name == "SpriteTime")
			{
				FSzaman = gameObject3.GetComponent<FontSprites>();
			}
			if (gameObject3.name == "SpriteStars")
			{
				FSstar = gameObject3.GetComponent<FontSprites>();
			}
			if (gameObject3.name == "SpriteMoney")
			{
				FSmoney = gameObject3.GetComponent<FontSprites>();
			}
			if (gameObject3.name == "GaripWin")
			{
				gameObject2 = gameObject3.gameObject;
			}
			if (gameObject3.name == "You_Win")
			{
				gameObject = gameObject3.gameObject;
			}
			if (gameObject3.name == "TekYildiz")
			{
				TekStar = gameObject3.gameObject;
				TekStar.transform.position = Vector3.zero;
			}
		}
		if (SceneManager.GetActiveScene().name == "Level_7")
		{
			if ((bool)gameObject)
			{
				gameObject.SetActive(false);
			}
		}
		else if ((bool)gameObject2)
		{
			gameObject2.SetActive(false);
		}
		for (int i = 0; i < 3; i++)
		{
			Pasifler[i].SetActive(false);
			Aktifler[i].SetActive(false);
		}
		WinYildizYap();
	}

	private void ZamanKaydet(int yeniZaman)
	{
		string text = SceneManager.GetActiveScene().name;
		text = text.Replace("Level_", string.Empty);
		int @int = PlayerPrefs.GetInt("lev" + text + "zaman");
		if (@int == 0)
		{
			PlayerPrefs.SetInt("lev" + text + "zaman", yeniZaman);
		}
		else if (yeniZaman < @int)
		{
			PlayerPrefs.SetInt("lev" + text + "zaman", yeniZaman);
		}
	}

	private void WinYildizYap()
	{
		if (!YildizYaptik)
		{
			CameraFollow component = Camera.main.GetComponent<CameraFollow>();
			int yeniZaman = (int)component.toplamZaman;
			CtoplamStar = component.PuanSay;
			ZamanKaydet(yeniZaman);
			YildizYaptik = true;
			float num = Mathf.Floor(component.ToplamPuan / 3);
			if (component.PuanSay == component.ToplamPuan)
			{
				Yildiztoplam = 3;
			}
			else if ((float)component.PuanSay >= num * 2f)
			{
				Yildiztoplam = 2;
			}
			else if ((float)component.PuanSay >= num)
			{
				Yildiztoplam = 1;
			}
			Pasifler[0].SetActive(true);
			Pasifler[1].SetActive(true);
			Pasifler[2].SetActive(true);
			string text = SceneManager.GetActiveScene().name;
			text = text.Replace("Level_", string.Empty);
			int @int = PlayerPrefs.GetInt("level" + text + "rb2star");
			if (Yildiztoplam > @int)
			{
				PlayerPrefs.SetInt("level" + text + "rb2star", Yildiztoplam);
			}
			string kelime = "Time: " + yeniZaman + " sec";
			int num2 = 0;
			if (@int < 3 && Yildiztoplam == 3)
			{
				num2 = 100;
			}
			else if (Yildiztoplam == 3)
			{
				num2 = 50;
			}
			else if (Yildiztoplam == 2)
			{
				num2 = 10;
			}
			else if (Yildiztoplam == 1)
			{
				num2 = 5;
			}
			if (num2 > 0)
			{
				TopMoney = num2;
			}
			else
			{
				TopMoney = 0;
				FSmoney.MetinDegis("Gold: 0 $");
			}
			if (Yildiztoplam == 0)
			{
				TekStar.SetActive(false);
			}
			if (num2 > 0)
			{
				int int2 = PlayerPrefs.GetInt("MevcutPara");
				int value = int2 + num2;
				PlayerPrefs.SetInt("MevcutPara", value);
			}
			FSzaman.MetinDegis(kelime);
		}
	}

	public void StarBaslat()
	{
		for (int i = 0; i < 4; i++)
		{
			miniStars[i] = Object.Instantiate(TekStar, base.transform.position, base.transform.rotation);
			miniStars[i].transform.localScale = Vector3.one;
			miniStars[i].transform.position = Vector3.zero;
			miniStars[i].transform.parent = base.transform;
		}
		miniStars[4] = TekStar;
		SonkonumSs[0] = new Vector3(0.001439244f, 0.0005310054f, 0f);
		SonkonumSs[1] = new Vector3(0.0007636348f, -0.001315985f, 0f);
		SonkonumSs[2] = new Vector3(-0.001292595f, 0.0004064512f, 0f);
		SonkonumSs[3] = new Vector3(0f, 0.001534084f, 0f);
		SonkonumSs[4] = new Vector3(-0.0008777009f, -0.001352784f, 0f);
		ilkKonum = Vector3.zero;
		ilkZaman = (araZaman = (araZaman2 = Time.realtimeSinceStartup));
		HangiStar = 1;
		ilkKonum = Aktifler[0].transform.localPosition;
	}

	private void MinileripTal()
	{
		for (int i = 0; i < 5; i++)
		{
			miniStars[i].SetActive(false);
		}
	}

	private void Update()
	{
		if (CtoplamStar > 0 && starsay < CtoplamStar)
		{
			float num = Time.realtimeSinceStartup - araZaman;
			if (num > 0.04f)
			{
				starsay++;
				araZaman = Time.realtimeSinceStartup;
				string kelime = "Stars: " + starsay;
				FSstar.MetinDegis(kelime);
			}
			if (num > 4f)
			{
				return;
			}
		}
		if (TopMoney > 0 && goldSay < TopMoney)
		{
			float num2 = Time.realtimeSinceStartup - araZaman2;
			if (num2 > 0.02f)
			{
				goldSay++;
				araZaman2 = Time.realtimeSinceStartup;
				string kelime2 = "Gold: " + goldSay + " $";
				FSmoney.MetinDegis(kelime2);
			}
			if (num2 > 4f)
			{
				return;
			}
		}
		if (Yildiztoplam == 0)
		{
			return;
		}
		float num3 = (Time.realtimeSinceStartup - ilkZaman) / sspeed;
		if (HangiStar == 1 && num3 <= 1.1f)
		{
			if (num3 > 0.9f)
			{
				num3 = 0f;
				HangiStar = 2;
				ilkZaman = Time.realtimeSinceStartup;
				ilkKonum = Aktifler[1].transform.localPosition;
				Aktifler[0].SetActive(true);
				Pasifler[0].SetActive(false);
				if (Yildiztoplam == 1)
				{
					MinileripTal();
				}
			}
			for (int i = 0; i < 5; i++)
			{
				Vector3 b = Aktifler[0].transform.localPosition + SonkonumSs[i];
				miniStars[i].transform.localPosition = Vector3.Lerp(ilkKonum, b, num3);
			}
		}
		if (HangiStar == 2 && num3 <= 1.1f && Yildiztoplam > 1)
		{
			if (num3 > 0.9f)
			{
				num3 = 0f;
				HangiStar = 3;
				Aktifler[1].SetActive(true);
				Pasifler[1].SetActive(false);
				ilkZaman = Time.realtimeSinceStartup;
				ilkKonum = Aktifler[2].transform.localPosition;
				if (Yildiztoplam == 2)
				{
					MinileripTal();
				}
			}
			for (int j = 0; j < 5; j++)
			{
				Vector3 b = Aktifler[1].transform.localPosition + SonkonumSs[j];
				miniStars[j].transform.localPosition = Vector3.Lerp(ilkKonum, b, num3);
			}
		}
		if (HangiStar != 3 || !(num3 <= 1.1f) || Yildiztoplam <= 2)
		{
			return;
		}
		if (num3 > 0.9f)
		{
			HangiStar = 3;
			Aktifler[2].SetActive(true);
			Pasifler[2].SetActive(false);
			if (Yildiztoplam == 3)
			{
				MinileripTal();
			}
		}
		for (int k = 0; k < 5; k++)
		{
			Vector3 b = Aktifler[2].transform.localPosition + SonkonumSs[k];
			miniStars[k].transform.localPosition = Vector3.Lerp(ilkKonum, b, num3);
		}
	}
}
