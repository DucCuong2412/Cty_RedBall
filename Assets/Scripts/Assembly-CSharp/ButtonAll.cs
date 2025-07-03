using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAll : MonoBehaviour
{
	public int DokunmaID = -1;

	public Color LambaParla = Color.yellow;

	private Vector3 wp3;

	private Vector2 wp2;

	private Color Bey = Color.white;

	private Color Kir = Color.red;

	private SpriteRenderer Sr;

	private SpriteRenderer SrLamb;

	private Vector3 OrjinalV3 = Vector3.one;

	private GameObject SesiptalGo;

	private CameraFollow CameraSc;

	private bool MenuPenceresindeyim;

	private bool Level5;

	private GameObject ajax;

	private float araZaman;

	public void AjxGizle()
	{
		if ((bool)ajax)
		{
			ajax.SetActive(false);
		}
	}

	public void AjxGoster()
	{
		if ((bool)ajax)
		{
			ajax.SetActive(true);
		}
	}

	private void Start()
	{
		if (base.name == "butonWatchVideo" || base.name == "butonWatchCpoinT")
		{
			ajax = UnityEngine.Object.Instantiate(Resources.Load("ajax", typeof(GameObject))) as GameObject;
			ajax.transform.parent = base.transform;
			ajax.transform.localPosition = Vector3.zero;
			ajax.SetActive(false);
		}
		if (SceneManager.GetActiveScene().name == "Level_3")
		{
			Level5 = true;
		}
		Transform parent = base.transform.parent;
		if (parent != null)
		{
			MenuPenceresindeyim = true;
		}
		CameraSc = Camera.main.GetComponent<CameraFollow>();
		if (base.name.Contains("main"))
		{
			Kir = LambaParla;
		}
		else
		{
			Kir = new Color(0.7f, 0.7f, 0.7f, 1f);
		}
		if (base.transform.childCount > 0)
		{
			GameObject gameObject = null;
			for (int num = base.transform.childCount - 1; num >= 0; num--)
			{
				gameObject = base.transform.GetChild(num).gameObject;
				if (gameObject.name.Contains("ikon"))
				{
					Sr = gameObject.gameObject.GetComponent<SpriteRenderer>();
				}
				if (gameObject.name.Contains("lamba"))
				{
					SrLamb = gameObject.gameObject.GetComponent<SpriteRenderer>();
				}
			}
		}
		DokunmaID = -1;
		OrjinalV3 = base.transform.localScale;
		if (!(base.name == "butonSes"))
		{
			return;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name == "iptal")
				{
					SesiptalGo = transform.gameObject;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		if (CameraSc.Sesvar)
		{
			SesgoYOK();
		}
		else
		{
			SesGoVar();
		}
	}

	private void SesgoYOK()
	{
		SesiptalGo.SetActive(false);
		CameraFollow component = Camera.main.GetComponent<CameraFollow>();
		if ((bool)component)
		{
			component.sesLoopAc();
		}
	}

	private void SesGoVar()
	{
		SesiptalGo.SetActive(true);
		CameraFollow component = Camera.main.GetComponent<CameraFollow>();
		if ((bool)component)
		{
			component.sesLoopKapat();
		}
	}

	private void Update()
	{
		if (!MenuPenceresindeyim && Player.MenuAcik)
		{
			return;
		}
		if ((bool)ajax && ajax.activeInHierarchy)
		{
			float num = Time.realtimeSinceStartup - araZaman;
			if (num > 0.04f)
			{
				araZaman = Time.realtimeSinceStartup;
				ajax.transform.Rotate(0f, 0f, -7f);
			}
		}
		if (!(base.gameObject.GetComponent<Collider2D>() != null))
		{
			return;
		}
		if (Input.touchCount < 1)
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Input.GetMouseButtonDown(0) && GetComponent<Collider2D>().OverlapPoint(new Vector2(vector.x, vector.y)))
			{
				Basladi();
			}
			if (Input.GetMouseButtonUp(0) && GetComponent<Collider2D>().OverlapPoint(new Vector2(vector.x, vector.y)))
			{
				DokunmaBitti();
			}
			return;
		}
		for (int i = 0; i < Input.touchCount; i++)
		{
			wp3 = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
			wp2 = new Vector2(wp3.x, wp3.y);
			if (base.gameObject.GetComponent<Collider2D>().OverlapPoint(wp2))
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					DokunmaID = Input.GetTouch(i).fingerId;
					Basladi();
				}
				if (Input.GetTouch(i).phase == TouchPhase.Moved)
				{
					DokunmaID = Input.GetTouch(i).fingerId;
					Surtundu();
				}
			}
			else if (Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).fingerId != DokunmaID)
			{
				UzakSurtundu();
				DokunmaID = -1;
			}
			if (Input.GetTouch(i).phase == TouchPhase.Ended && Input.GetTouch(i).fingerId == DokunmaID)
			{
				DokunmaBitti();
				DokunmaID = -1;
			}
		}
	}

	private void Buyut()
	{
		Vector3 localScale = OrjinalV3 * 1.05f;
		base.gameObject.transform.localScale = localScale;
		if ((bool)SrLamb)
		{
			SrLamb.color = LambaParla;
		}
		if ((bool)Sr)
		{
			Sr.color = Kir;
		}
	}

	private void Kucult()
	{
		base.gameObject.transform.localScale = OrjinalV3;
		if ((bool)SrLamb)
		{
			SrLamb.color = Color.white;
		}
		if ((bool)Sr)
		{
			Sr.color = Bey;
		}
	}

	public void Basladi()
	{
		if (base.name == "butonPlayilk")
		{
			if (Level5 && ElleControler.durum == 1)
			{
				return;
			}
			if (Level5 && ElleControler.durum == 5)
			{
				GameObject gameObject = GameObject.Find("ElleControl");
				if ((bool)gameObject)
				{
					ElleControler component = gameObject.GetComponent<ElleControler>();
					if ((bool)component)
					{
						component.OkGizle();
						ParmaklarSc.ParlayanButon = 5;
					}
				}
			}
		}
		if (((!(base.name == "butonMarketKapa") && !(base.name == "butonPaid")) || !Level5 || ElleControler.durum != 2) && (!(base.name == "butonMarketKapa") || !Level5 || ElleControler.durum >= 4))
		{
			Buyut();
		}
	}

	public void Surtundu()
	{
		Buyut();
	}

	public void UzakSurtundu()
	{
		Kucult();
	}

	private void Sifirla()
	{
		Player.Geberdi = false;
		Player.MenuAcik = false;
		Player.paused = false;
		Player.Gdustu = false;
	}

	public void DokunmaBitti()
	{
		if (base.name == "butonAddUP")
		{
			GameObject gameObject = GameObject.Find("TheRedBALL");
			if ((bool)gameObject)
			{
				gameObject.GetComponent<Player>().UpointEkle();
			}
			Player.paused = false;
			Time.timeScale = 1f;
			CameraSc.MenuGitsin(0);
			Kucult();
		}
		if (base.name == "butonCpoints")
		{
			CameraSc.MenuGitsin(3);
			CameraSc.MenuGelsin(8);
			GameObject gameObject2 = GameObject.Find("TheRedBALL");
			if ((bool)gameObject2)
			{
				gameObject2.GetComponent<Player>().CpointSistem();
			}
			Kucult();
		}
		if (base.name == "butonPointOK")
		{
			int @int = PlayerPrefs.GetInt("MevcutPara");
			if (@int < 500)
			{
				CameraSc.MenuGelsin(9);
			}
			else
			{
				int value = @int - 500;
				PlayerPrefs.SetInt("MevcutPara", value);
				CameraSc.MenuGitsin(8);
				Sifirla();
				GameObject gameObject3 = GameObject.Find("TheRedBALL");
				if ((bool)gameObject3)
				{
					Player component = gameObject3.GetComponent<Player>();
					if ((bool)component)
					{
						component.CkalkanAC();
						component.PointsGoSil();
					}
				}
			}
			Kucult();
		}
		if (base.name == "butonPointGeri")
		{
			GameObject gameObject4 = GameObject.Find("TheRedBALL");
			if ((bool)gameObject4)
			{
				gameObject4.GetComponent<Player>().OncekiPoint();
			}
			Kucult();
		}
		if (base.name == "butonPointIleri")
		{
			GameObject gameObject5 = GameObject.Find("TheRedBALL");
			if ((bool)gameObject5)
			{
				gameObject5.GetComponent<Player>().SonrakiPoint();
			}
			Kucult();
		}
		if (base.name == "butonContinue")
		{
			Player.paused = false;
			Time.timeScale = 1f;
			Kucult();
			CameraSc.MenuGitsin(0);
		}
		if (base.name == "butonPlayTotor")
		{
			Sifirla();
			CameraSc.ilkZaman = 0f;
			CameraSc.ekZaman = 0f;
			CameraSc.toplamZaman = 0f;
			CameraSc.PuanSay = 0;
			Time.timeScale = 1f;
			Kucult();
			CameraSc.MenuGitsin(7);
		}
		if (base.name == "butonSkipTut")
		{
			PlayerPrefs.SetInt("TutorSkip", 1);
			Time.timeScale = 1f;
			Kucult();
			Sifirla();
			SceneManager.LoadScene("Level_1");
		}
		if (base.name == "butonHome")
		{
			Time.timeScale = 1f;
			Kucult();
			Sifirla();
			SceneManager.LoadScene("mainMenu");
		}
		if (base.name == "butonReplay")
		{
			Sifirla();
			Player.Geberdi = false;
			Player.Gdustu = false;
			Time.timeScale = 1f;
			Kucult();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (base.name == "butonSes")
		{
			Kucult();
			if (CameraSc.Sesvar)
			{
				SesGoVar();
				AudioListener.volume = 0f;
				CameraSc.Sesvar = false;
				PlayerPrefs.SetInt("Sesvar", 2);
				PlayerPrefs.Save();
			}
			else
			{
				SesgoYOK();
				int int2 = PlayerPrefs.GetInt("SesSeviyeVar");
				if (int2 == 1)
				{
					int int3 = PlayerPrefs.GetInt("SesSeviyesi");
					float volume = (float)int3 * 0.01f;
					AudioListener.volume = volume;
				}
				CameraSc.Sesvar = true;
				PlayerPrefs.SetInt("Sesvar", 1);
				PlayerPrefs.Save();
			}
		}
		if (base.name == "butonPlayilk")
		{
			if (Level5 && ElleControler.durum == 1)
			{
				return;
			}
			Sifirla();
			CameraSc.ilkZaman = 0f;
			CameraSc.ekZaman = 0f;
			CameraSc.toplamZaman = 0f;
			CameraSc.PuanSay = 0;
			Time.timeScale = 1f;
			Kucult();
			CameraSc.MenuGitsin(1);
		}
		if (base.name == "butonMarket")
		{
			if (Level5 && ElleControler.durum == 1)
			{
				GameObject gameObject6 = GameObject.Find("ElleControl");
				if ((bool)gameObject6)
				{
					ElleControler component2 = gameObject6.GetComponent<ElleControler>();
					if ((bool)component2)
					{
						ElleControler.durum = 2;
						Transform kime = CameraSc.MenuTransformVer(4);
						component2.ElAktar(kime);
						component2.ElGit(1);
					}
				}
			}
			Kucult();
			CameraSc.MenuGitsin(1);
			CameraSc.MenuGelsin(4);
		}
		if (base.name == "butonIzleKapa")
		{
			CameraSc.MenuGitsin(9);
			CameraSc.MenuGitsin(8);
			CameraSc.MenuGelsin(3);
		}
		if (base.name == "butonMarketKapa")
		{
			if (Level5 && ElleControler.durum == 2)
			{
				return;
			}
			if (Level5 && ElleControler.durum == 3)
			{
				GameObject gameObject7 = GameObject.Find("ElleControl");
				if ((bool)gameObject7)
				{
					ElleControler component3 = gameObject7.GetComponent<ElleControler>();
					if ((bool)component3)
					{
						component3.ElGizle();
					}
				}
			}
			Sifirla();
			Kucult();
			CameraSc.MenuGitsin(4);
			CameraSc.MenuGelsin(1);
			GameObject.Find("menuStart").GetComponent<menuStartSc>().MarkettenGeldi();
		}
		if (base.name == "butonRoketBuy")
		{
			if (Level5 && ElleControler.durum == 2)
			{
				GameObject gameObject8 = GameObject.Find("ElleControl");
				if ((bool)gameObject8)
				{
					ElleControler component4 = gameObject8.GetComponent<ElleControler>();
					if ((bool)component4)
					{
						ElleControler.durum = 3;
						component4.ElGit(2);
					}
				}
			}
			MarketICON(0);
			Kucult();
		}
		if (base.name == "butonBombaBuy")
		{
			MarketICON(1);
			Kucult();
		}
		if (base.name == "butonKalkanBuy")
		{
			MarketICON(2);
			Kucult();
		}
		if (base.name == "butonTakipBuy")
		{
			MarketICON(3);
			Kucult();
		}
		if (base.name == "butonPaid")
		{
			if (Level5 && ElleControler.durum == 3)
			{
				GameObject gameObject9 = GameObject.Find("ElleControl");
				if ((bool)gameObject9)
				{
					ElleControler component5 = gameObject9.GetComponent<ElleControler>();
					if ((bool)component5)
					{
						ElleControler.durum = 4;
						component5.ElGit(3);
					}
				}
			}
			Kucult();
			MarketPAY();
		}
		if (base.name == "butonWatchVideo")
		{
			AdsMenuden component6 = base.transform.parent.GetComponent<AdsMenuden>();
			if ((bool)component6)
			{
				component6.VideoGoster();
			}
			Kucult();
		}
		if (base.name == "butonWatchCpoinT")
		{
			AdsMenuden component7 = base.transform.parent.GetComponent<AdsMenuden>();
			if ((bool)component7)
			{
				component7.VideoGoster();
			}
			Kucult();
		}
		if (base.name == "butonLostWatch")
		{
			AdsMenuden component8 = base.transform.parent.GetComponent<AdsMenuden>();
			if ((bool)component8)
			{
				component8.VideoGoster();
			}
			Kucult();
		}
		if (base.name == "butoNmainWATCH")
		{
			int int4 = PlayerPrefs.GetInt("MevcutPara");
			int4 += 500;
			PlayerPrefs.SetInt("MevcutPara", int4);
			GameObject gameObject10 = GameObject.Find("ParaLoader");
			if ((bool)gameObject10)
			{
				gameObject10.GetComponent<MainParaLoader>().Guncelle();
			}
			Time.timeScale = 1f;
			Kucult();
		}
		if (base.name == "butoNmainPLAY")
		{
			Sifirla();
			Time.timeScale = 1f;
			Kucult();
			int int5 = PlayerPrefs.GetInt("GeldiimLevel");
			if (int5 > 46)
			{
				SceneManager.LoadScene("Levels");
			}
			else
			{
				SceneManager.LoadScene("Level_" + int5);
			}
		}
		if (base.name == "butoNmainLEVELS")
		{
			Sifirla();
			Time.timeScale = 1f;
			Kucult();
			SceneManager.LoadScene("Levels");
		}
		if (base.name == "butoNmainSETTNG")
		{
			Time.timeScale = 1f;
			Kucult();
			CameraSc.MenuGelsin(6);
		}
		if (base.name == "butonSettKapa")
		{
			Sifirla();
			Time.timeScale = 1f;
			Kucult();
			CameraSc.MenuGitsin(6);
			if (SceneManager.GetActiveScene().name == "HowtoPlay")
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
		if (base.name == "butoNmainHOWTO")
		{
			Sifirla();
			SceneManager.LoadScene("HowtoPlay");
			Time.timeScale = 1f;
			Kucult();
		}
		if (base.name == "butoNmainEXIT")
		{
			Time.timeScale = 1f;
			Kucult();
			CameraSc.MenuGelsin(5);
		}
		if (base.name == "butonGoOut")
		{
			Sifirla();
			Time.timeScale = 1f;
			Kucult();
			Application.Quit();
		}
		if (base.name == "butonStay")
		{
			Sifirla();
			Time.timeScale = 1f;
			Kucult();
			CameraSc.MenuGitsin(5);
		}
		if (base.name == "butoNmainVOTE")
		{
			int int6 = PlayerPrefs.GetInt("Voted");
			if (int6 != 1)
			{
				PlayerPrefs.SetInt("Voted", 1);
			}
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Gamikro");
			Time.timeScale = 1f;
			Kucult();
		}
		if (base.name == "butonNext")
		{
			Sifirla();
			Time.timeScale = 1f;
			Kucult();
			string text = SceneManager.GetActiveScene().name;
			text = text.Replace("Level_", string.Empty);
			int num = int.Parse(text);
			string text2 = (num + 1).ToString();
			text = "Level_" + text2;
			SceneManager.LoadScene(text);
		}
		if (base.name == "butoNmainLIST")
		{
			Time.timeScale = 1f;
			Kucult();
		}
	}

	private void MarketICON(int no)
	{
		GameObject gameObject = GameObject.Find("menuMarket");
		gameObject.GetComponent<MARKETsc>().MarketICONdegis(no);
	}

	private void MarketPAY()
	{
		GameObject gameObject = GameObject.Find("menuMarket");
		gameObject.GetComponent<MARKETsc>().MarketPAID();
	}
}
