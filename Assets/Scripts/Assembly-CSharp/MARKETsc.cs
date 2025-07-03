using UnityEngine;
using UnityEngine.SceneManagement;

public class MARKETsc : MonoBehaviour
{
	private int MevcutPara;

	private SpriteRenderer soruisRender;

	private Sprite soruSprite;

	private GameObject[] iTexts;

	private int[] Fiyatlar;

	private int OdenecekUcret;

	private int istenenItem;

	private bool BJetVar;

	private bool BTakipVar;

	private GameObject JetOk;

	private GameObject BombaOk;

	private GameObject KalkanOk;

	private GameObject TakipOk;

	private FontSprites BombaFS;

	private FontSprites KalkanFS;

	private FontSprites ButceFS;

	private FontSprites FiyatlaRR;

	private GameObject FakirOkGo;

	public void ParaGuncelle()
	{
		MevcutPara = PlayerPrefs.GetInt("MevcutPara");
		string text = MevcutPara.ToString();
		string kelime = "You have: $ " + text;
		ButceFS.MetinDegis(kelime);
		AdsMenuden component = base.gameObject.GetComponent<AdsMenuden>();
		component.ajaxBAK();
	}

	private void Start()
	{
		Fiyatlar = new int[4];
		Fiyatlar[0] = 100;
		Fiyatlar[1] = 200;
		Fiyatlar[2] = 400;
		Fiyatlar[3] = 300;
		GameObject gameObject = GameObject.Find("ButceFont");
		ButceFS = gameObject.GetComponent<FontSprites>();
		GameObject gameObject2 = GameObject.Find("soruisa");
		if (gameObject2 != null)
		{
			soruisRender = gameObject2.GetComponent<SpriteRenderer>();
			soruSprite = soruisRender.sprite;
		}
		iTexts = new GameObject[5];
		for (int i = 0; i < iTexts.Length; i++)
		{
			iTexts[i] = GameObject.Find("infoText" + i);
			iTexts[i].SetActive(false);
		}
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject3 = base.transform.GetChild(num).gameObject;
			if (gameObject3.name == "Jet_Okey")
			{
				JetOk = gameObject3;
				JetOk.SetActive(false);
			}
			if (gameObject3.name == "Takip_Okey")
			{
				TakipOk = gameObject3;
				TakipOk.SetActive(false);
			}
			if (gameObject3.name == "Bomb_Okey")
			{
				BombaOk = gameObject3;
			}
			if (gameObject3.name == "Kalkan_Okey")
			{
				KalkanOk = gameObject3;
			}
			if (gameObject3.name == "FiyatlarFont")
			{
				FiyatlaRR = gameObject3.gameObject.GetComponent<FontSprites>();
				if (FiyatlaRR != null && SceneManager.GetActiveScene().name == "Level_3")
				{
					string kelime = "Free   200     300     400";
					FiyatlaRR.MetinDegis(kelime);
				}
			}
		}
		for (int num2 = base.transform.childCount - 1; num2 >= 0; num2--)
		{
			GameObject gameObject3 = base.transform.GetChild(num2).gameObject;
			if (gameObject3.name.Contains("_Okey"))
			{
				gameObject3.SetActive(false);
			}
		}
		DepoKontrol();
		if (SceneManager.GetActiveScene().name != "Level_3")
		{
			FakirOkGo = Object.Instantiate(Resources.Load("elle1", typeof(GameObject))) as GameObject;
			FakirOkGo.transform.parent = base.transform;
			Vector3 localScale = FakirOkGo.transform.localScale;
			localScale.x *= -1f;
			FakirOkGo.transform.localScale = localScale;
			Vector3 zero = Vector3.zero;
			zero.x = -0.0033f;
			zero.y = -0.00545f;
			FakirOkGo.transform.localPosition = zero;
			FakirOkGo.SetActive(false);
		}
	}

	private void DepoKontrol()
	{
		int @int = PlayerPrefs.GetInt("MarketJet");
		int int2 = PlayerPrefs.GetInt("MarketBomba");
		int int3 = PlayerPrefs.GetInt("MarketKalkan");
		int int4 = PlayerPrefs.GetInt("MarketTakip");
		if (@int == 1)
		{
			BJetVar = true;
			JetOk.SetActive(true);
		}
		else
		{
			JetOk.SetActive(false);
		}
		GameObject gameObject = GameObject.Find("BombaFont");
		BombaFS = gameObject.GetComponent<FontSprites>();
		string kelime = int2.ToString();
		BombaFS.MetinDegis(kelime);
		GameObject gameObject2 = GameObject.Find("KalkanFont");
		KalkanFS = gameObject2.GetComponent<FontSprites>();
		string kelime2 = int3.ToString();
		KalkanFS.MetinDegis(kelime2);
		if (int2 > 0)
		{
			BombaOk.SetActive(true);
		}
		else
		{
			BombaOk.SetActive(false);
		}
		if (int3 > 0)
		{
			KalkanOk.SetActive(true);
		}
		else
		{
			KalkanOk.SetActive(false);
		}
		if (int4 == 1)
		{
			BTakipVar = true;
			TakipOk.SetActive(true);
		}
		else
		{
			TakipOk.SetActive(false);
		}
	}

	public void MarketPAID()
	{
		if (SceneManager.GetActiveScene().name == "Level_3" && istenenItem == 0)
		{
			if (!BJetVar)
			{
				PlayerPrefs.SetInt("MarketJet", 1);
				JetOk.SetActive(true);
				BJetVar = true;
			}
			return;
		}
		MevcutPara = PlayerPrefs.GetInt("MevcutPara");
		if (OdenecekUcret == 0 || MevcutPara < OdenecekUcret)
		{
			return;
		}
		Player component = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		if (istenenItem == 0)
		{
			if (BJetVar)
			{
				return;
			}
			PlayerPrefs.SetInt("MarketJet", 1);
			JetOk.SetActive(true);
			BJetVar = true;
		}
		if (istenenItem == 1)
		{
			component.BombaVar = true;
			int @int = PlayerPrefs.GetInt("MarketBomba");
			@int++;
			PlayerPrefs.SetInt("MarketBomba", @int);
			string kelime = @int.ToString();
			BombaFS.MetinDegis(kelime);
			BombaOk.SetActive(true);
		}
		if (istenenItem == 2)
		{
			int int2 = PlayerPrefs.GetInt("MarketKalkan");
			int2++;
			PlayerPrefs.SetInt("MarketKalkan", int2);
			string kelime2 = int2.ToString();
			KalkanFS.MetinDegis(kelime2);
			KalkanOk.SetActive(true);
		}
		if (istenenItem == 3)
		{
			if (BTakipVar)
			{
				return;
			}
			PlayerPrefs.SetInt("MarketTakip", 1);
			TakipOk.SetActive(true);
			BTakipVar = true;
		}
		MonoBehaviour.print(OdenecekUcret);
		int value = MevcutPara - OdenecekUcret;
		PlayerPrefs.SetInt("MevcutPara", value);
		PlayerPrefs.Save();
		string text = value.ToString();
		string kelime3 = "You have: $ " + text;
		ButceFS.MetinDegis(kelime3);
	}

	public void MarketICONdegis(int no)
	{
		int num = Fiyatlar[no];
		if (MevcutPara < num)
		{
			for (int i = 0; i < iTexts.Length; i++)
			{
				iTexts[i].SetActive(false);
			}
			iTexts[4].SetActive(true);
			if (FakirOkGo != null)
			{
				FakirOkGo.SetActive(true);
			}
			soruisRender.sprite = soruSprite;
			return;
		}
		if (FakirOkGo != null)
		{
			FakirOkGo.SetActive(false);
		}
		for (int j = 0; j < iTexts.Length; j++)
		{
			iTexts[j].SetActive(false);
		}
		iTexts[no].SetActive(true);
		if (no == 0 && BJetVar)
		{
			soruisRender.sprite = soruSprite;
			return;
		}
		if (no == 3 && BTakipVar)
		{
			soruisRender.sprite = soruSprite;
			return;
		}
		GameObject gameObject = GameObject.Find((new string[4] { "ikon_roket", "ikon_bomb", "ikon_kalkan", "ikon_takip" })[no]);
		soruisRender.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
		OdenecekUcret = num;
		istenenItem = no;
	}
}
