using UnityEngine;
using UnityEngine.SceneManagement;

public class menuStartSc : MonoBehaviour
{
	private GameObject[] vitrinler;

	private GameObject[] Bonuslar;

	private GameObject FSbomba;

	private GameObject FSkalkan;

	private GameObject DeepDead;

	private Vector3 sonPos;

	private void Start()
	{
		if (SceneManager.GetActiveScene().name == "Level_3")
		{
			GameObject gameObject = GameObject.Find("ElleControl");
			if ((bool)gameObject)
			{
				ElleControler component = gameObject.GetComponent<ElleControler>();
				if ((bool)component)
				{
					component.ElAktar(base.transform);
					component.ElGit(0);
				}
			}
		}
		DeepDead = GameObject.Find("DeepDead");
		vitrinler = new GameObject[4];
		Bonuslar = new GameObject[4];
		int num = 0;
		string kelime = string.Empty;
		if ((bool)DeepDead)
		{
			LevelHediyeSc component2 = DeepDead.GetComponent<LevelHediyeSc>();
			if ((bool)component2)
			{
				kelime = component2.LevelAd;
			}
		}
		string text = SceneManager.GetActiveScene().name;
		text = text.Replace("Level_", string.Empty);
		text = "        Level " + text;
		for (int num2 = base.transform.childCount - 1; num2 >= 0; num2--)
		{
			GameObject gameObject2 = base.transform.GetChild(num2).gameObject;
			if (gameObject2.name.Contains("LevelNoText"))
			{
				FontSprites component3 = gameObject2.gameObject.GetComponent<FontSprites>();
				component3.MetinDegis(text);
			}
			if (gameObject2.name.Contains("LevelText"))
			{
				FontSprites component4 = gameObject2.gameObject.GetComponent<FontSprites>();
				component4.MetinDegis(kelime);
			}
			if (gameObject2.name.Contains("vitrin"))
			{
				vitrinler[num] = base.transform.GetChild(num2).gameObject;
				num++;
			}
			if (gameObject2.name.Contains("Bonus_"))
			{
				string s = gameObject2.name.Replace("Bonus_", string.Empty);
				int num3 = int.Parse(s);
				Bonuslar[num3] = gameObject2;
			}
		}
		for (int i = 0; i < vitrinler.Length; i++)
		{
			vitrinler[i].SetActive(false);
		}
		if (SceneManager.GetActiveScene().name != "Level_3")
		{
			int num4 = Random.Range(0, 3);
			vitrinler[num4].SetActive(true);
		}
		FSbomba = GameObject.Find("BombaSayFont");
		FSkalkan = GameObject.Find("KalkanSayFont");
		for (int j = 0; j < Bonuslar.Length; j++)
		{
			Bonuslar[j].SetActive(false);
		}
		sonPos = Bonuslar[0].transform.localPosition;
		sonPos.x = -0.001988634f;
		Invoke("PlayerCheck", 0.3f);
		if (SceneManager.GetActiveScene().name == "HowtoPlay")
		{
			Player.Geberdi = false;
			Player.Gdustu = false;
			Player.MenuAcik = false;
			Time.timeScale = 1f;
		}
	}

	public void MarkettenGeldi()
	{
		if (ElleControler.durum == 4 && SceneManager.GetActiveScene().name == "Level_3")
		{
			GameObject gameObject = GameObject.Find("ElleControl");
			if ((bool)gameObject)
			{
				ElleControler component = gameObject.GetComponent<ElleControler>();
				if ((bool)component)
				{
					component.OkKoy();
					ElleControler.durum = 5;
				}
			}
		}
		PlayerCheck();
	}

	private void PlayerCheck()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int @int = PlayerPrefs.GetInt("MarketJet");
		int int2 = PlayerPrefs.GetInt("MarketBomba");
		int int3 = PlayerPrefs.GetInt("MarketKalkan");
		int int4 = PlayerPrefs.GetInt("MarketTakip");
		sonPos = Bonuslar[0].transform.localPosition;
		sonPos.x = -0.001988634f;
		if ((bool)DeepDead)
		{
			LevelHediyeSc component = DeepDead.GetComponent<LevelHediyeSc>();
			if (component.Jet)
			{
				num = 1;
			}
			if (component.Takip)
			{
				num4 = 1;
			}
			if (component.Bomba > 0)
			{
				num2 = component.Bomba;
			}
			if (component.Kalkan > 0)
			{
				num3 = component.Kalkan;
			}
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if (!gameObject)
		{
			MonoBehaviour.print("Player YOK");
		}
		Player component2 = gameObject.GetComponent<Player>();
		Rigidbody2D component3 = gameObject.GetComponent<Rigidbody2D>();
		component3.velocity = Vector2.zero;
		Player.k_sag = false;
		Player.k_ust = false;
		Player.k_sol = false;
		Player.GravityTers = false;
		if (num == 1 || @int == 1)
		{
			Bonuslar[3].SetActive(true);
			Bonuslar[3].transform.localPosition = sonPos;
			sonPos.x += 0.0015f;
			component2.JetVer();
		}
		if (num4 == 1 || int4 == 1)
		{
			Bonuslar[1].SetActive(true);
			Bonuslar[1].transform.localPosition = sonPos;
			sonPos.x += 0.0013f;
			component2.TakipVer();
		}
		if (num2 > 0 || int2 > 0)
		{
			if ((bool)FSbomba)
			{
				FontSprites component4 = FSbomba.GetComponent<FontSprites>();
				num2 = int2 + num2;
				string kelime = num2.ToString();
				component4.MetinDegis(kelime);
				Bonuslar[0].SetActive(true);
				Bonuslar[0].transform.localPosition = sonPos;
				sonPos.x += 0.0013f;
				component2.BombaDurum(num2);
			}
		}
		else
		{
			FSbomba.SetActive(false);
		}
		if (num3 > 0 || int3 > 0)
		{
			Bonuslar[2].SetActive(true);
			Bonuslar[2].transform.localPosition = sonPos;
			sonPos.x += 0.0013f;
			num3 = int3 + num3;
			FontSprites component5 = FSkalkan.GetComponent<FontSprites>();
			string kelime2 = num3.ToString();
			component5.MetinDegis(kelime2);
			component2.pKalkanDurum(num3);
		}
		else
		{
			FSkalkan.SetActive(false);
		}
	}
}
