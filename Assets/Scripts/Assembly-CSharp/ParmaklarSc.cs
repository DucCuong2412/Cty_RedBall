using UnityEngine;
using UnityEngine.SceneManagement;

public class ParmaklarSc : MonoBehaviour
{
	public bool ButonVar = true;

	public Sprite[] Oklar;

	private GameObject[] OklarGo;

	private SpriteRenderer[] OklarSprR;

	private SpriteRenderer[] OklarIcons;

	private Vector2 TekDumeBoyu;

	private Vector2 TekDumeBoyP;

	private Rect[] Alan;

	private float genislik;

	private float yukseklik;

	public bool[] Tusbasili;

	public int[] TouchID;

	private bool SanalJoystik;

	private Vector3 SanalJilkPos;

	private bool JetButonVar;

	private bool KalkanButonVar;

	private bool BombaButonVar;

	public static int ParlayanButon;

	private float araZaman;

	private float tF;

	private bool ParlaGizli = true;

	private bool ParlaGoster;

	private int ParlaSay;

	private GameObject CAmPara;

	private GameObject CAmlevelis;

	private bool[] Cbutonlar = new bool[7];

	private GUIStyle boxStyle;

	private void Start()
	{
		if (SceneManager.GetActiveScene().name.Contains("Level_"))
		{
			string text = SceneManager.GetActiveScene().name;
			text = text.Replace("Level_", string.Empty);
			int value = int.Parse(text);
			PlayerPrefs.SetInt("GeldiimLevel", value);
		}
		Physics2D.gravity = new Vector2(0f, -9.81f);
		genislik = Screen.width;
		yukseklik = Screen.height;
		TouchID = new int[7] { -1, -1, -1, -1, -1, -1, -1 };
		Tusbasili = new bool[7];
		ekrana_hizala();
		hepsini_seffaf_yap();
	}

	public void JetButonGoster()
	{
		JetButonVar = true;
		OklarGo[5].SetActive(true);
	}

	public void KalkanButonGoster()
	{
		KalkanButonVar = true;
		OklarGo[6].SetActive(true);
	}

	public void KalkanButonGizle()
	{
		KalkanButonVar = false;
		OklarGo[6].SetActive(false);
	}

	public void BombaButonGoster()
	{
		BombaButonVar = true;
		OklarGo[3].SetActive(true);
	}

	public void BombaButonGizle()
	{
		BombaButonVar = false;
		OklarGo[3].SetActive(false);
	}

	private void Update()
	{
		if (ParlayanButon != 0)
		{
			tF = Time.realtimeSinceStartup - araZaman;
			int parlayanButon = ParlayanButon;
			if (tF > 0.2f)
			{
				if (!ParlaGoster)
				{
					ParlaGoster = true;
					ParlaGizli = false;
					gorunur_yap(parlayanButon);
				}
			}
			else if (!ParlaGizli)
			{
				ParlaGizli = true;
				ParlaGoster = false;
				gizli_yap(parlayanButon);
				ParlaSay++;
				if (ParlaSay > 5)
				{
					ParlaSay = 0;
					ParlayanButon = 0;
				}
			}
			if (tF > 0.4f)
			{
				araZaman = Time.realtimeSinceStartup;
			}
		}
		if (Player.MenuAcik)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OyunuDondur();
		}
		else
		{
			if (Player.paused)
			{
				return;
			}
			for (int i = 0; i < Input.touchCount; i++)
			{
				for (int j = 0; j < Alan.Length; j++)
				{
					PhaseKontrol(Input.GetTouch(i), j);
				}
			}
		}
	}

	public void ButonGoster(bool aktif)
	{
		for (int i = 0; i < 7; i++)
		{
			if (aktif)
			{
				OklarGo[i].gameObject.SetActive(true);
			}
			else
			{
				OklarGo[i].gameObject.SetActive(false);
			}
		}
		if (!JetButonVar)
		{
			OklarGo[5].SetActive(false);
		}
		if (!BombaButonVar)
		{
			OklarGo[3].SetActive(false);
		}
		if (!KalkanButonVar)
		{
			OklarGo[6].SetActive(false);
		}
	}

	private void PhaseKontrol(Touch touch, int no)
	{
		switch (touch.phase)
		{
		case TouchPhase.Began:
			if (PositionKontrol(touch.position, Alan[no]) && !Tusbasili[no])
			{
				if (SanalJoystik && no == 0)
				{
					Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
					position.z = -5f;
					OklarGo[0].transform.position = position;
					SanalJilkPos = touch.position;
				}
				TouchID[no] = touch.fingerId;
				TusBasti(no);
			}
			break;
		case TouchPhase.Moved:
		{
			if (Tusbasili[no] && TouchID[no] == touch.fingerId && !PositionKontrol(touch.position, Alan[no]))
			{
				TusBirakti(no);
				TouchID[no] = -1;
				break;
			}
			if (PositionKontrol(touch.position, Alan[5]))
			{
				TusBasti(5);
				TouchID[5] = touch.fingerId;
				break;
			}
			for (int i = 0; i < 5; i++)
			{
				if (!PositionKontrol(touch.position, Alan[i]))
				{
					continue;
				}
				if (SanalJoystik && i == 0)
				{
					if (((Vector3)Input.GetTouch(0).position).x < SanalJilkPos.x)
					{
						Player.k_sol = true;
						Player.k_sag = false;
						Vector3 zero = Vector3.zero;
						zero.x = -0.4f;
						zero.y = -0.03f;
						OklarIcons[0].transform.localPosition = zero;
					}
					else
					{
						Player.k_sol = false;
						Player.k_sag = true;
						Vector3 zero2 = Vector3.zero;
						zero2.x = 0.4f;
						zero2.y = -0.03f;
						OklarIcons[0].transform.localPosition = zero2;
					}
				}
				else
				{
					TusBasti(i);
					TouchID[i] = touch.fingerId;
				}
			}
			break;
		}
		case TouchPhase.Ended:
		case TouchPhase.Canceled:
			if (TouchID[no] == touch.fingerId && Tusbasili[no])
			{
				TouchID[no] = -1;
				TusBirakti(no);
				if (SanalJoystik && no == 0)
				{
					MonoBehaviour.print("sanalJ bıraktı");
					OklarIcons[0].transform.localPosition = Vector3.zero;
					Player.k_sag = false;
					Player.k_sol = false;
				}
			}
			break;
		case TouchPhase.Stationary:
			break;
		}
	}

	private void OyunuDondur()
	{
		Player.paused = true;
		Camera.main.GetComponent<CameraFollow>().MenuGelsin(0);
		Camera.main.GetComponent<CameraFollow>().SecondPAUSE();
	}

	private void TusBasti(int no)
	{
		Tusbasili[no] = true;
		gorunur_yap(no);
		switch (no)
		{
		case 0:
			Player.k_sol = true;
			break;
		case 1:
			Player.k_sag = true;
			break;
		case 2:
			Player.k_ust = true;
			break;
		case 3:
			if (BombaButonVar)
			{
				Player.k_bomba = true;
			}
			break;
		case 4:
			OyunuDondur();
			break;
		case 5:
			if (JetButonVar)
			{
				Player.k_jet = true;
			}
			break;
		case 6:
			if (KalkanButonVar)
			{
				Player.k_kalkan = true;
			}
			break;
		}
		if (Player.k_sol && Player.k_sag)
		{
			Player.k_sol = false;
			TusBirakti(0);
		}
	}

	private void TusBirakti(int no)
	{
		Tusbasili[no] = false;
		gizli_yap(no);
		switch (no)
		{
		case 0:
			Player.k_sol = false;
			break;
		case 1:
			Player.k_sag = false;
			break;
		case 2:
			Player.k_ust = false;
			break;
		case 3:
			Player.k_bomba = false;
			break;
		case 5:
			Player.k_jet = false;
			break;
		case 6:
			Player.k_kalkan = false;
			break;
		case 4:
			break;
		}
	}

	private bool PositionKontrol(Vector2 tPos, Rect kare)
	{
		Vector3 point = tPos;
		point.y = yukseklik - tPos.y;
		if (kare.Contains(point))
		{
			return true;
		}
		return false;
	}

	public void kameraBuyuduu()
	{
		float num = Camera.main.orthographicSize / 3.5f;
		for (int i = 0; i < OklarGo.Length; i++)
		{
			OklarGo[i].transform.localScale = Vector3.one * num;
		}
		TekDumeBoyu.x = Oklar[0].bounds.extents.x * num;
		TekDumeBoyu.y = Oklar[0].bounds.extents.y * num;
		TekDumeBoyP.x = Oklar[4].bounds.extents.x * num;
		TekDumeBoyP.y = Oklar[4].bounds.extents.y * num;
		ekrana_hizala();
	}

	private void ekrana_hizala()
	{
		float num = genislik / 40f;
		float num2 = genislik / 7f;
		Alan = new Rect[7];
		Alan[0] = new Rect(0f, yukseklik / 2f, num2 + num, yukseklik / 2f);
		Alan[1] = new Rect(num2 + num * 1.1f, yukseklik / 2f, num2 * 2f, yukseklik / 2f);
		Alan[2] = new Rect(genislik - genislik / 4f, yukseklik * 0.65f, genislik / 4f, yukseklik * 0.35f);
		Alan[4] = new Rect(0f, 0f, genislik / 5f, yukseklik / 4f);
		Alan[3] = new Rect(genislik - genislik / 4f, 0f, genislik / 4f, yukseklik * 0.3f);
		Alan[5] = new Rect(genislik - genislik / 4f, yukseklik * 0.325f, genislik / 4f, yukseklik * 0.3f);
		Alan[6] = new Rect(genislik * 0.5f - num2 * 0.5f, 0f, genislik / 5f, yukseklik / 4f);
		if (SanalJoystik)
		{
			Alan[1] = new Rect(0f, 0f, 0f, 0f);
			Alan[0] = new Rect(0f, yukseklik * 0.3f, genislik - genislik / 3f, yukseklik * 0.7f);
		}
		Vector3 position = Vector3.zero;
		position.z = -5f;
		Vector3 zero = Vector3.zero;
		zero.x = Alan[0].x + Alan[0].width - Alan[0].width * 0.1f;
		zero.y = Alan[0].y - Alan[0].height + Alan[0].width * 0.1f;
		Vector3 vector = Camera.main.ScreenToWorldPoint(zero);
		if (SanalJoystik)
		{
			zero.x = Alan[1].x + Alan[0].width * 0.1f;
			zero.y = Alan[1].y - Alan[1].height + Alan[0].width * 0.1f;
			vector = Camera.main.ScreenToWorldPoint(zero);
			position.x = vector.x + TekDumeBoyu.x;
			position.y = vector.y + TekDumeBoyu.y;
			OklarGo[0].transform.position = position;
		}
		else
		{
			vector.z = -5f;
			position = vector;
			position.x -= TekDumeBoyu.x;
			position.y += TekDumeBoyu.y;
			OklarGo[0].transform.position = position;
			zero.x = Alan[1].x + Alan[0].width * 0.1f;
			zero.y = Alan[1].y - Alan[1].height + Alan[0].width * 0.1f;
			vector = Camera.main.ScreenToWorldPoint(zero);
			position.x = vector.x + TekDumeBoyu.x;
			position.y = vector.y + TekDumeBoyu.y;
			OklarGo[1].transform.position = position;
		}
		zero.x = Alan[2].x + Alan[2].width;
		zero.y = yukseklik - Alan[2].y;
		vector = Camera.main.ScreenToWorldPoint(zero);
		position.x = vector.x - TekDumeBoyu.x - TekDumeBoyu.x * 0.3f;
		position.y = vector.y - TekDumeBoyu.y * 1.2f;
		OklarGo[2].transform.position = position;
		zero.x = Alan[5].x + Alan[5].width;
		zero.y = Alan[5].y + Alan[5].height;
		vector = Camera.main.ScreenToWorldPoint(zero);
		position.x = vector.x - TekDumeBoyu.x - TekDumeBoyu.x * 0.3f;
		position.y = vector.y - TekDumeBoyu.y - TekDumeBoyu.y * 0.03f;
		OklarGo[5].transform.position = position;
		zero.x = 0f;
		zero.y = yukseklik;
		vector = Camera.main.ScreenToWorldPoint(zero);
		position.x = vector.x + TekDumeBoyP.x + TekDumeBoyP.x * 0.2f;
		position.y = vector.y - TekDumeBoyP.y - TekDumeBoyP.y * 0.2f;
		OklarGo[4].transform.position = position;
		zero.x = Alan[3].x + Alan[3].width;
		zero.y = yukseklik;
		vector = Camera.main.ScreenToWorldPoint(zero);
		position.x = vector.x - TekDumeBoyu.x - TekDumeBoyu.x * 0.3f;
		position.y = vector.y - TekDumeBoyu.y * 1.85f;
		OklarGo[3].transform.position = position;
		zero.x = Alan[6].x + Alan[6].width * 0.2f;
		zero.y = yukseklik;
		vector = Camera.main.ScreenToWorldPoint(zero);
		position.x = vector.x + TekDumeBoyP.x + TekDumeBoyP.x * 0.2f;
		position.y = vector.y - TekDumeBoyP.y - TekDumeBoyP.y * 0.2f;
		OklarGo[6].transform.position = position;
	}

	private void gorunur_yap(int no)
	{
		Color white = Color.white;
		white.a = 0.7f;
		OklarSprR[no].color = white;
		if (no != 4 && no != 6)
		{
			OklarIcons[no].color = white;
		}
	}

	private void gizli_yap(int no)
	{
		Color white = Color.white;
		white.a = 0.3f;
		OklarSprR[no].color = white;
		if (no != 4 && no != 6)
		{
			OklarIcons[no].color = white;
		}
	}

	public void Chepsini_gizle()
	{
		for (int i = 0; i < OklarGo.Length; i++)
		{
			if (OklarGo[i].activeInHierarchy)
			{
				Cbutonlar[i] = true;
				OklarGo[i].SetActive(false);
			}
		}
		GameObject gameObject = null;
		Transform transform = Camera.main.transform;
		for (int num = transform.childCount - 1; num >= 0; num--)
		{
			gameObject = transform.GetChild(num).gameObject;
			if (gameObject.name == "YildizSay")
			{
				CAmPara = gameObject;
				CAmPara.SetActive(false);
			}
			if (gameObject.name == "Levelismi")
			{
				CAmlevelis = gameObject;
				CAmlevelis.SetActive(false);
			}
		}
	}

	public void Chepsini_goster()
	{
		for (int i = 0; i < OklarGo.Length; i++)
		{
			if (Cbutonlar[i])
			{
				OklarGo[i].SetActive(true);
			}
			Cbutonlar[i] = false;
		}
		if (CAmPara != null)
		{
			CAmPara.SetActive(true);
		}
		if (CAmlevelis != null)
		{
			CAmlevelis.SetActive(true);
		}
	}

	public void hepsini_seffaf_yap()
	{
		Color white = Color.white;
		white.a = 0.3f;
		for (int i = 0; i < 7; i++)
		{
			OklarSprR[i].color = white;
			if (i < 4)
			{
				OklarIcons[i].color = white;
			}
		}
		OklarIcons[5].color = white;
		if (SanalJoystik)
		{
			white.a = 0.7f;
			OklarIcons[0].color = white;
		}
	}

	private void DumeleriYap()
	{
		string sortingLayerName = "Menu";
		int num = 10;
		OklarIcons = new SpriteRenderer[6];
		OklarGo = new GameObject[7];
		OklarSprR = new SpriteRenderer[7];
		GameObject[] array = new GameObject[6];
		for (int i = 0; i < OklarGo.Length; i++)
		{
			OklarGo[i] = new GameObject("Ok_" + i);
			OklarSprR[i] = OklarGo[i].gameObject.AddComponent<SpriteRenderer>();
			OklarSprR[i].sortingLayerName = sortingLayerName;
			OklarSprR[i].sortingOrder = num;
			OklarGo[i].transform.parent = Camera.main.transform;
			if (i == 4 || i == 6)
			{
				if (i == 4)
				{
					OklarSprR[i].sprite = Oklar[i];
					TekDumeBoyu.x = Oklar[0].bounds.extents.x;
					TekDumeBoyu.y = Oklar[0].bounds.extents.y;
					TekDumeBoyP.x = Oklar[4].bounds.extents.x;
					TekDumeBoyP.y = Oklar[4].bounds.extents.y;
				}
				else
				{
					OklarSprR[i].sprite = Oklar[8];
				}
			}
			else
			{
				int num2 = i + 3;
				OklarSprR[i].sprite = Oklar[0];
				array[i] = new GameObject("ic_" + num2);
				array[i].transform.parent = OklarGo[i].transform;
				OklarIcons[i] = array[i].gameObject.AddComponent<SpriteRenderer>();
				OklarIcons[i].sortingLayerName = sortingLayerName;
				OklarIcons[i].sortingOrder = num + 1;
			}
		}
		if (SanalJoystik)
		{
			OklarSprR[0].sprite = Oklar[6];
			OklarIcons[0].sprite = Oklar[7];
			OklarGo[1].SetActive(false);
			array[1].SetActive(false);
		}
		else
		{
			OklarIcons[0].sprite = Oklar[1];
			OklarIcons[1].sprite = Oklar[1];
		}
		OklarIcons[2].sprite = Oklar[2];
		OklarIcons[3].sprite = Oklar[3];
		OklarIcons[5].sprite = Oklar[5];
		Vector3 zero = Vector3.zero;
		zero.x = 0.133f;
		array[1].transform.localPosition = zero;
		zero.x = -0.133f;
		if (!SanalJoystik)
		{
			array[0].transform.localPosition = zero;
			array[0].transform.Rotate(new Vector3(0f, 0f, 180f));
		}
		if (Camera.main.orthographicSize > 3.5f)
		{
			float num3 = Camera.main.orthographicSize / 3.5f;
			for (int j = 0; j < OklarGo.Length; j++)
			{
				OklarGo[j].transform.localScale = Vector3.one * num3;
			}
			TekDumeBoyu.x = Oklar[0].bounds.extents.x * num3;
			TekDumeBoyu.y = Oklar[0].bounds.extents.y * num3;
			TekDumeBoyP.x = Oklar[4].bounds.extents.x * num3;
			TekDumeBoyP.y = Oklar[4].bounds.extents.y * num3;
		}
		if (!JetButonVar)
		{
			OklarGo[5].SetActive(false);
		}
		if (!BombaButonVar)
		{
			OklarGo[3].SetActive(false);
		}
		if (!KalkanButonVar)
		{
			OklarGo[6].SetActive(false);
		}
	}

	private void Awake()
	{
		int @int = PlayerPrefs.GetInt("SanalJoystik");
		if (@int != 1)
		{
			SanalJoystik = false;
		}
		else
		{
			SanalJoystik = true;
		}
		if (!(SceneManager.GetActiveScene().name == "mainMenu"))
		{
			DumeleriYap();
			if (!(SceneManager.GetActiveScene().name == "HowtoPlay"))
			{
			}
		}
	}

	private Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] array = new Color[width * height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = col;
		}
		Texture2D texture2D = new Texture2D(width, height);
		texture2D.SetPixels(array);
		texture2D.Apply();
		return texture2D;
	}

	private void OnGUI()
	{
		if (!(SceneManager.GetActiveScene().name == "HowtoPlay"))
		{
			return;
		}
		GUI.color = Color.black;
		GUI.skin.box.fontSize = 20;
		if (boxStyle == null)
		{
			boxStyle = new GUIStyle(GUI.skin.box);
			boxStyle.alignment = TextAnchor.UpperCenter;
			boxStyle.normal.background = MakeTex(2, 2, new Color(0.5f, 0.8f, 0.5f, 0.3f));
		}
		GUI.skin.box = boxStyle;
		if (Player.paused)
		{
			GUI.Box(Alan[4], "Pause");
		}
		if (Player.k_kalkan)
		{
			GUI.Box(Alan[6], "Shield");
		}
		if (Player.k_jet)
		{
			GUI.Box(Alan[5], "JetPack");
		}
		if (Player.k_bomba)
		{
			GUI.Box(Alan[3], "Bomb");
		}
		if (Player.k_ust)
		{
			GUI.Box(Alan[2], "Jump");
		}
		if (SanalJoystik)
		{
			if (Player.k_sol || Player.k_sag)
			{
				GUI.Box(Alan[0], "V.Joystick");
			}
			return;
		}
		if (Player.k_sol)
		{
			GUI.Box(Alan[0], "Left");
		}
		if (Player.k_sag)
		{
			GUI.Box(Alan[1], "Right");
		}
	}
}
