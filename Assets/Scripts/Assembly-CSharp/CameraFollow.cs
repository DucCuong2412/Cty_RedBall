using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
	public float xMargin = 1f;

	public float xSmooth = 4f;

	public float ySmooth = 4f;

	public Vector2 min;

	public Vector2 max;

	private Transform player;

	private Vector2 playerVel;

	private Rigidbody2D pRigid;

	private GameObject buluts;

	public AudioClip[] Sesler;

	public bool SabitKamera;

	public bool Sesvar = true;

	public bool BgSesvar = true;

	public int PuanSay;

	public int ToplamPuan;

	private FontSprites ParaSc;

	private int BombaSay;

	private int KalkanAdet;

	private Vector3 bulutsiLK;

	private GameObject parnaks;

	public float toplamZaman;

	public float ilkZaman;

	public float ekZaman;

	public GameObject[] MenuLER;

	private Vector3[] menuilkBoy;

	public Transform MenuTransformVer(int menuNo)
	{
		return MenuLER[menuNo].transform;
	}

	public void MenuGitsin(int giden)
	{
		MenuLER[giden].GetComponent<MenuANIM>().AnimGotur();
	}

	public void MenuGelsin(int gelen)
	{
		if (gelen == 3)
		{
			KaybettiSes();
		}
		MenuLER[gelen].GetComponent<MenuANIM>().AnimGetir();
	}

	public void sesLoopKapat()
	{
		AudioSource component = GetComponent<AudioSource>();
		if ((bool)component)
		{
			component.loop = false;
			component.Stop();
		}
	}

	public void sesLoopAc()
	{
		if (Sesvar)
		{
			AudioSource component = GetComponent<AudioSource>();
			if ((bool)component)
			{
				component.loop = true;
				component.Play();
			}
		}
	}

	public void LevelBitti()
	{
		ZaferSes();
		sesLoopKapat();
		SecondBitti();
		MenuLER[2].GetComponent<MenuANIM>().AnimGetir();
		string text = SceneManager.GetActiveScene().name;
		if (!text.Contains("Level_"))
		{
			return;
		}
		text = text.Replace("Level_", string.Empty);
		int num = int.Parse(text);
		if (num > 3 && num != 7)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("AdsMerkex");
			if ((bool)gameObject)
			{
				gameObject.GetComponent<AdsCenter>().InterGoster();
			}
		}
	}

	public void ZiplamaSes()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[0], base.transform.position);
		}
	}

	public void ParaSes()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[1], base.transform.position);
		}
	}

	public void ZaferSes()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[2], base.transform.position);
		}
	}

	public void KaybettiSes()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[3], base.transform.position);
		}
	}

	public void BonusSes()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[4], base.transform.position);
		}
	}

	public void BombaBirak()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[5], base.transform.position);
		}
	}

	public void BombaPatla()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[6], base.transform.position);
		}
	}

	public void JetSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[7], base.transform.position);
		}
	}

	public void kalkanSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[8], base.transform.position);
		}
	}

	public void TahtaSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[9], base.transform.position);
		}
	}

	public void TuglaSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[10], base.transform.position);
		}
	}

	public void CamSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[11], base.transform.position);
		}
	}

	public void UfoSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[12], base.transform.position);
		}
	}

	public void SulukSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[13], base.transform.position);
		}
	}

	public void PortalSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[14], base.transform.position);
		}
	}

	public void TutorOKSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[15], base.transform.position);
		}
	}

	public void SadnessSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[16], base.transform.position);
		}
	}

	public void AliveSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[17], base.transform.position);
		}
	}

	public void RewardSesi()
	{
		if (Sesvar)
		{
			AudioSource.PlayClipAtPoint(Sesler[18], base.transform.position);
		}
	}

	private void MetinYaz()
	{
		string empty = string.Empty;
		empty = ((PuanSay != ToplamPuan) ? (PuanSay + "/" + ToplamPuan + "* ") : "Ok * ");
		if (BombaSay > 0)
		{
			empty = empty + BombaSay + "# ";
		}
		if (KalkanAdet > 0)
		{
			string text = KalkanAdet.ToString();
			empty = empty + text + "%  ";
		}
		ParaSc.MetinDegis(empty);
	}

	public void ParaArttir()
	{
		ParaSes();
		PuanSay++;
		MetinYaz();
	}

	public void Bombadurum(int Kactane)
	{
		BombaSay = Kactane;
		MetinYaz();
	}

	public void KalkanDurum(int Kactane)
	{
		KalkanAdet = Kactane;
		MetinYaz();
	}

	public void SecondBasladi()
	{
		ekZaman = (toplamZaman = 0f);
		ilkZaman = Time.timeSinceLevelLoad;
	}

	public void SecondRESUME()
	{
		ilkZaman = Time.timeSinceLevelLoad;
	}

	public void SecondPAUSE()
	{
		float num = Time.timeSinceLevelLoad - ilkZaman;
		if (ekZaman != 0f)
		{
			ekZaman += num;
		}
	}

	public void SecondBitti()
	{
		toplamZaman = Time.timeSinceLevelLoad - ilkZaman;
		if (ekZaman != 0f)
		{
			toplamZaman = ekZaman + toplamZaman;
		}
	}

	public ParmaklarSc parmakSCver()
	{
		if ((bool)parnaks)
		{
			MonoBehaviour.print("parmak var");
			return parnaks.GetComponent<ParmaklarSc>();
		}
		MonoBehaviour.print("parmak YOOOK");
		return null;
	}

	private void Awake()
	{
		PuanSay = 0;
		float num = GameObject.FindGameObjectsWithTag("yildiz").Length;
		ToplamPuan = (int)num;
		MenuleriYukle();
		if (!SabitKamera)
		{
			parnaks = Object.Instantiate(Resources.Load("Parmaklar", typeof(GameObject))) as GameObject;
			parnaks.transform.position = Vector3.zero;
			parnaks.name = "Parmaklar";
			GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
			if ((bool)gameObject)
			{
				player = gameObject.transform;
				pRigid = gameObject.GetComponent<Rigidbody2D>();
			}
			if (SceneManager.GetActiveScene().name == "Level_0")
			{
				MenuLER[7].GetComponent<MenuANIM>().MenuOrtala();
			}
			else
			{
				MenuLER[1].GetComponent<MenuANIM>().MenuOrtala();
			}
		}
		GameObject gameObject2 = null;
		for (int num2 = base.transform.childCount - 1; num2 >= 0; num2--)
		{
			gameObject2 = base.transform.GetChild(num2).gameObject;
			if (gameObject2.name == "YildizSay")
			{
				ParaSc = gameObject2.gameObject.GetComponent<FontSprites>();
				ParaSc.MetinDegis("0/" + ToplamPuan + "*");
				if (SceneManager.GetActiveScene().name == "HowtoPlay")
				{
					gameObject2.gameObject.SetActive(false);
				}
			}
			if (gameObject2.name == "Levelismi")
			{
				string text = "        Level X";
				text = SceneManager.GetActiveScene().name;
				text = text.Replace("Level_", string.Empty);
				text = "        Level " + text;
				FontSprites component = gameObject2.gameObject.GetComponent<FontSprites>();
				component.MetinDegis(text);
				if (SceneManager.GetActiveScene().name == "HowtoPlay")
				{
					gameObject2.gameObject.SetActive(false);
				}
			}
		}
	}

	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("AdsMerkex");
		if ((bool)gameObject)
		{
			gameObject.GetComponent<AdsCenter>().LevelBasladi();
		}
		PlayerPrefs.SetInt("IzlenenVid", 0);
		int @int = PlayerPrefs.GetInt("Sesvar");
		if (@int == 2)
		{
			Sesvar = false;
			sesLoopKapat();
		}
		else
		{
			Sesvar = true;
			int int2 = PlayerPrefs.GetInt("SesSeviyeVar");
			if (int2 == 1)
			{
				int int3 = PlayerPrefs.GetInt("SesSeviyesi");
				float volume = (float)int3 * 0.01f;
				AudioListener.volume = volume;
				sesLoopAc();
			}
		}
		if (!SabitKamera)
		{
			buluts = GameObject.Find("Bulutlar");
			if ((bool)buluts)
			{
				bulutsiLK = buluts.transform.position;
			}
			if (SceneManager.GetActiveScene().name == "HowtoPlay")
			{
				MenuGitsin(1);
			}
		}
	}

	private void FixedUpdate()
	{
		if (!SabitKamera)
		{
			float x = base.transform.position.x;
			float y = base.transform.position.y;
			xSmooth = Mathf.Clamp(Mathf.Abs(pRigid.velocity.x), 15f, 20f);
			ySmooth = Mathf.Clamp(Mathf.Abs(pRigid.velocity.y), 5f, 20f);
			x = Mathf.Lerp(base.transform.position.x, player.position.x, xSmooth * 0.5f * Time.deltaTime);
			y = Mathf.Lerp(base.transform.position.y, player.position.y, ySmooth * 0.5f * Time.deltaTime);
			if (min.x != 0f)
			{
				x = Mathf.Clamp(x, min.x, max.x);
			}
			if (min.y != 0f)
			{
				y = Mathf.Clamp(y, min.y, max.y);
			}
			base.transform.position = new Vector3(x, y, base.transform.position.z);
			if ((bool)buluts)
			{
				buluts.transform.position = new Vector3(bulutsiLK.x + x / 2f, bulutsiLK.y + y / 2f, bulutsiLK.z);
			}
		}
	}

	private void MenuleriYukle()
	{
		MenuLER = new GameObject[10];
		MenuLER[0] = Object.Instantiate(Resources.Load("Menuler/menuPause", typeof(GameObject))) as GameObject;
		MenuLER[1] = Object.Instantiate(Resources.Load("Menuler/menuStart", typeof(GameObject))) as GameObject;
		MenuLER[2] = Object.Instantiate(Resources.Load("Menuler/menuWin", typeof(GameObject))) as GameObject;
		MenuLER[3] = Object.Instantiate(Resources.Load("Menuler/menuLost", typeof(GameObject))) as GameObject;
		MenuLER[4] = Object.Instantiate(Resources.Load("Menuler/menuMarket", typeof(GameObject))) as GameObject;
		MenuLER[5] = Object.Instantiate(Resources.Load("Menuler/menuExit", typeof(GameObject))) as GameObject;
		MenuLER[6] = Object.Instantiate(Resources.Load("Menuler/menuSettings", typeof(GameObject))) as GameObject;
		MenuLER[7] = Object.Instantiate(Resources.Load("Menuler/menuTutorial", typeof(GameObject))) as GameObject;
		MenuLER[8] = Object.Instantiate(Resources.Load("Menuler/menuCPoints", typeof(GameObject))) as GameObject;
		MenuLER[9] = Object.Instantiate(Resources.Load("Menuler/MenuIzle", typeof(GameObject))) as GameObject;
		Vector3 position = Camera.main.transform.position;
		position.z = 1f;
		menuilkBoy = new Vector3[MenuLER.Length];
		for (int i = 0; i < MenuLER.Length; i++)
		{
			MenuLER[i].transform.parent = Camera.main.transform;
			MenuLER[i].transform.position = position;
			MenuLER[i].name = MenuLER[i].name.Replace("(Clone)", string.Empty);
			menuilkBoy[i] = MenuLER[i].transform.localScale;
			MenuLER[i].SetActive(false);
		}
		if (Camera.main.orthographicSize > 3.5f)
		{
			KameraBuyuMenu();
		}
	}

	public void KameraBuyuMenu()
	{
		float num = Camera.main.orthographicSize / 3.5f;
		for (int i = 0; i < MenuLER.Length; i++)
		{
			MenuLER[i].transform.localScale = menuilkBoy[i] * num;
		}
	}
}
