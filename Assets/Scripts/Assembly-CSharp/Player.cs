using System.Collections.Generic;
using UnityEngine;

public class Player : Pooler
{
	public static bool paused;

	public static bool MenuAcik;

	public static bool k_sag;

	public static bool k_sol;

	public static bool k_ust;

	public static bool k_kalkan;

	public static bool k_jet;

	public static bool k_bomba;

	public static bool Geberdi;

	public static bool Gdustu;

	public static bool GravityTers;

	private Color RenkBeyaz;

	private Color RenkKirmizi;

	public bool DonduKaldi;

	public bool jump;

	public float moveForce = 13.5f;

	public float maxSpeed = 4.3f;

	public float jumpForce = 6.7f;

	public float AtesForce = 10f;

	public bool grounded;

	public bool groundedH;

	public bool JetGround;

	private Vector2 vec = new Vector2(0f, 0f);

	private Vector2 groundCheck;

	private Vector2 CollisionPoint;

	private GameObject DeepDead;

	private GameObject ParaSay;

	private TrailRenderer JetTrail;

	private float UpDeadF;

	private bool bombaAtar = true;

	private int BombaSay;

	private int KalkanSay;

	private Animator anim;

	public Vector2 aktif_velocity;

	private SpriteRenderer KalkanSR;

	private GameObject KalkanGo;

	private bool KalkanSon;

	public bool JetVar;

	public bool BombaVar;

	public bool KalkanAktif;

	public bool TakipVar;

	private CameraFollow CameraSc;

	private ParmaklarSc parnakSc;

	private int KalkanBlinkSay;

	private LayerMask PlLM;

	private bool JoystikVar;

	private bool GebMgeldi;

	private bool ZsesOk = true;

	public List<Vector2> CPoints;

	public List<Vector2> UPoints;

	public List<GameObject> PointsGo;

	private Vector2 CilkNokta;

	public bool CpointAktif;

	private int CAktifNokta;

	private Vector3 OncekiNokta;

	private Vector3 SonrakiNokta;

	private bool CpointGidiyor;

	private float ilkZaman;

	private float Cspeed = 0.3f;

	private int PointSay = 1;

	private void Awake()
	{
		k_sag = false;
		k_sol = false;
		k_ust = false;
		k_kalkan = false;
		k_jet = false;
		k_bomba = false;
		Geberdi = false;
		Gdustu = false;
		GravityTers = false;
		JetVar = false;
		BombaVar = false;
		KalkanAktif = false;
		TakipVar = false;
		PlayerPrefs.SetInt("MarketJet", 0);
		PlayerPrefs.SetInt("MarketBomba", 0);
		PlayerPrefs.SetInt("MarketKalkan", 0);
		PlayerPrefs.SetInt("MarketTakip", 0);
		PlayerPrefs.SetInt("KalkanDepo", 0);
		anim = GetComponent<Animator>();
		PoolerGo = Object.Instantiate(Resources.Load("redBomba", typeof(GameObject))) as GameObject;
		PoolerAwake();
		JetTrail = GetComponent<TrailRenderer>();
		JetTrail.enabled = false;
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "redkalkan")
			{
				KalkanSR = gameObject.gameObject.GetComponent<SpriteRenderer>();
				KalkanGo = gameObject.gameObject;
			}
		}
		KalkanGo.SetActive(false);
		if (!Camera.main)
		{
			MonoBehaviour.print("KAMERA YOOOK");
		}
		CameraSc = Camera.main.GetComponent<CameraFollow>();
	}

	public void pKalkanDurum(int KacTane)
	{
		if (KalkanSay == 0)
		{
			ParmaklarSc.ParlayanButon = 6;
		}
		KalkanSay = KacTane;
		PlayerPrefs.SetInt("KalkanDepo", KacTane);
		PlayerPrefs.Save();
		CameraSc.KalkanDurum(KalkanSay);
		parnakSc.KalkanButonGoster();
	}

	public void KalkanArttir(int KacTane)
	{
		int @int = PlayerPrefs.GetInt("KalkanDepo");
		if (KalkanSay == 0)
		{
			ParmaklarSc.ParlayanButon = 6;
		}
		@int += KacTane;
		PlayerPrefs.SetInt("KalkanDepo", @int);
		KalkanSay = @int;
		CameraSc.KalkanDurum(KalkanSay);
		parnakSc.hepsini_seffaf_yap();
		parnakSc.KalkanButonGoster();
	}

	private void geberMenuSifirla()
	{
		GebMgeldi = false;
		anim.SetBool("normal", false);
	}

	private void KalkanAktifle()
	{
		if (KalkanAktif)
		{
			return;
		}
		int @int = PlayerPrefs.GetInt("KalkanDepo");
		if (@int >= 0)
		{
			@int = (KalkanSay = @int - 1);
			if (KalkanSay == 0)
			{
				KalkanButonGizle();
			}
			if (@int < 1)
			{
				@int = 0;
				KalkanButonGizle();
			}
			PlayerPrefs.SetInt("KalkanDepo", @int);
			CameraSc.KalkanDurum(@int);
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Oyuncu"), LayerMask.NameToLayer("Tehlike"));
			CameraSc.kalkanSesi();
			KalkanGo.GetComponent<Renderer>().enabled = true;
			KalkanSR.color = Color.white;
			KalkanGo.SetActive(true);
			KalkanSon = false;
			KalkanAktif = true;
			KalkanBlinkSay = 0;
			Invoke("KalkanBitiyor", 10f);
		}
	}

	private void KalkanBitiyor()
	{
		Invoke("KalkanBlinkOff", 0.2f);
	}

	private void KalkanBlinkOff()
	{
		KalkanGo.GetComponent<Renderer>().enabled = false;
		KalkanBlinkSay++;
		if (KalkanBlinkSay > 4)
		{
			KalkanAktif = false;
			KalkanGo.SetActive(false);
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Oyuncu"), LayerMask.NameToLayer("Tehlike"), false);
		}
		else
		{
			Invoke("KalkanBlinkOn", 0.2f);
		}
	}

	private void KalkanBlinkOn()
	{
		KalkanGo.GetComponent<Renderer>().enabled = true;
		Invoke("KalkanBlinkOff", 0.2f);
	}

	public void KalkanButonAktif()
	{
		parnakSc.KalkanButonGoster();
	}

	public void KalkanButonGizle()
	{
		parnakSc.KalkanButonGizle();
	}

	public void BombaDurum(int Kactane)
	{
		BombaSay = Kactane;
		CameraSc.Bombadurum(Kactane);
		parnakSc.BombaButonGoster();
	}

	public void BombaVer(int Kactane)
	{
		if (BombaSay == 0)
		{
			ParmaklarSc.ParlayanButon = 3;
		}
		BombaSay += Kactane;
		CameraSc.Bombadurum(BombaSay);
		parnakSc.BombaButonGoster();
	}

	public void JetVer()
	{
		if (!JetVar)
		{
			JetVar = true;
			if ((bool)parnakSc)
			{
				parnakSc.hepsini_seffaf_yap();
				parnakSc.JetButonGoster();
			}
			ParmaklarSc.ParlayanButon = 5;
		}
	}

	public void TakipVer()
	{
		if (!TakipVar)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load("takipli", typeof(GameObject))) as GameObject;
			gameObject.name = "takipli";
			TakipVar = true;
		}
	}

	public void GeberSurat()
	{
		anim.SetBool("dead", true);
		anim.SetBool("kizgin", false);
		anim.SetBool("uzgun", false);
	}

	public void KizginSurat()
	{
		anim.SetBool("kizgin", true);
	}

	public void KizginSuratNot()
	{
		anim.SetBool("kizgin", false);
	}

	public void UzgunSurat()
	{
		CameraSc.sesLoopKapat();
		anim.SetBool("uzgun", true);
	}

	private void Start()
	{
		PlLM = (1 << LayerMask.NameToLayer("zemin")) | (1 << LayerMask.NameToLayer("Default"));
		GameObject gameObject = GameObject.Find("UPDead");
		if ((bool)gameObject)
		{
			UpDeadF = gameObject.transform.position.y;
		}
		DeepDead = GameObject.Find("DeepDead");
		TrailGizle();
		CPoints.Add(base.transform.position);
		GameObject gameObject2 = GameObject.Find("Parmaklar");
		if (gameObject2 != null)
		{
			parnakSc = gameObject2.GetComponent<ParmaklarSc>();
			if (parnakSc == null)
			{
				MonoBehaviour.print("ParmaklarSC YOOOO");
			}
		}
		if (JoystikVar)
		{
			Debug.Log("gerçek joystik");
		}
	}

	private void OnCollisionStay2D(Collision2D sutun)
	{
		if (groundedH)
		{
			return;
		}
		ContactPoint2D[] contacts = sutun.contacts;
		for (int i = 0; i < contacts.Length; i++)
		{
			ContactPoint2D contactPoint2D = contacts[i];
			if (contactPoint2D.collider.gameObject.layer == LayerMask.NameToLayer("zemin"))
			{
				continue;
			}
			if (GravityTers)
			{
				if (contactPoint2D.point.y < base.transform.position.y + 0.2f && CollisionPoint != contactPoint2D.point)
				{
					CollisionPoint = contactPoint2D.point;
					groundedH = true;
				}
			}
			else if (contactPoint2D.point.y < base.transform.position.y - 0.2f && CollisionPoint != contactPoint2D.point)
			{
				CollisionPoint = contactPoint2D.point;
				groundedH = true;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D sutun)
	{
		if (groundedH)
		{
			return;
		}
		ContactPoint2D[] contacts = sutun.contacts;
		for (int i = 0; i < contacts.Length; i++)
		{
			ContactPoint2D contactPoint2D = contacts[i];
			if (GravityTers)
			{
				if (contactPoint2D.point.y < base.transform.position.y + 0.2f && CollisionPoint != contactPoint2D.point)
				{
					CollisionPoint = contactPoint2D.point;
					groundedH = true;
				}
			}
			else if (contactPoint2D.point.y < base.transform.position.y - 0.2f && CollisionPoint != contactPoint2D.point)
			{
				CollisionPoint = contactPoint2D.point;
				groundedH = true;
			}
		}
	}

	private void geberMenuAc()
	{
		if (!GebMgeldi)
		{
			GebMgeldi = true;
			CameraSc.sesLoopKapat();
			CameraSc.MenuGelsin(3);
		}
	}

	private void Update()
	{
		if (CpointAktif)
		{
			CpointHareket();
		}
		else
		{
			if (DonduKaldi)
			{
				return;
			}
			if (Geberdi)
			{
				if (!GebMgeldi)
				{
					Invoke("geberMenuAc", 0.1f);
				}
				return;
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				k_kalkan = true;
			}
			if (Input.GetKeyUp(KeyCode.K))
			{
				k_kalkan = false;
			}
			if (k_kalkan && KalkanSay > 0)
			{
				KalkanAktifle();
			}
			if (k_jet && JetVar && JetGround)
			{
				k_jet = false;
				JetGround = false;
				CameraSc.JetSesi();
				if (k_sag)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(AtesForce, GetComponent<Rigidbody2D>().velocity.y);
				}
				else if (k_sol)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0f - AtesForce, GetComponent<Rigidbody2D>().velocity.y);
				}
				JetTrail.enabled = true;
				Invoke("TrailGizle", 0.5f);
			}
			if (k_bomba && bombaAtar && BombaSay > 0)
			{
				BombaSay--;
				bombaAtar = false;
				Invoke("BombaAtarYap", 0.5f);
				PlayerPrefs.SetInt("BombaVar", BombaSay);
				GameObject gameObject = DepodanAl();
				gameObject.transform.position = base.transform.position;
				if (gameObject == null)
				{
					Debug.Log("bomba yok");
					return;
				}
				gameObject.GetComponent<RedBomba>().Beklemede();
				gameObject.SetActive(true);
				if (BombaSay == 0)
				{
					parnakSc.BombaButonGizle();
				}
				CameraSc.BombaBirak();
				CameraSc.Bombadurum(BombaSay);
			}
			Ziplama();
		}
	}

	private void TrailGizle()
	{
		JetTrail.enabled = false;
	}

	private void ZipsesOkFc()
	{
		ZsesOk = true;
	}

	private void ZiplamaSesi()
	{
		if (ZsesOk)
		{
			CameraSc.ZiplamaSes();
			ZsesOk = false;
			Invoke("ZipsesOkFc", 0.2f);
		}
	}

	private void BombaAtarYap()
	{
		bombaAtar = true;
	}

	private void Ziplama()
	{
		groundCheck = base.transform.position;
		float y;
		if (GravityTers)
		{
			groundCheck.y += 0.42f;
			y = jumpForce * -1f;
		}
		else
		{
			groundCheck.y -= 0.42f;
			y = jumpForce;
		}
		grounded = Physics2D.Linecast(base.transform.position, groundCheck, PlLM);
		if (!JetGround && (grounded || groundedH))
		{
			JetGround = true;
		}
		anim.SetBool("ziplama", !grounded);
		aktif_velocity = GetComponent<Rigidbody2D>().velocity;
		if (Mathf.Abs(aktif_velocity.x) < 0.06f && grounded)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		if (k_ust && grounded)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, y);
			grounded = false;
			if (GetComponent<Rigidbody2D>().gravityScale == 1f)
			{
				ZiplamaSesi();
			}
		}
		else if (k_ust && groundedH)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, y);
			groundedH = false;
			if (GetComponent<Rigidbody2D>().gravityScale == 1f)
			{
				ZiplamaSesi();
			}
		}
	}

	private void FixedUpdate()
	{
		if (Geberdi || DonduKaldi)
		{
			return;
		}
		if (KalkanAktif)
		{
			if (KalkanSon)
			{
				Color white = Color.white;
				white.a = 0.5f;
				KalkanSR.color = Color.Lerp(white, Color.black, Mathf.PingPong(Time.time, 0.6f));
			}
			KalkanGo.transform.Rotate(0f, 0f, -2f);
		}
		if (!grounded || GetComponent<Rigidbody2D>().velocity.y < 0f)
		{
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Oyuncu"), LayerMask.NameToLayer("tekyon"), GetComponent<Rigidbody2D>().velocity.y > 0f || grounded);
		}
		if (UpDeadF != 0f && base.transform.position.y > UpDeadF)
		{
			anim.SetBool("dead", true);
			if (base.transform.position.y > UpDeadF + 8f)
			{
				Gdustu = true;
				Geberdi = true;
			}
		}
		if ((bool)DeepDead && base.transform.position.y < DeepDead.transform.position.y)
		{
			anim.SetBool("dead", true);
			if (base.transform.position.y < DeepDead.transform.position.y - 8f)
			{
				Gdustu = true;
				Geberdi = true;
			}
		}
		float num = 0.5f;
		vec = Vector2.zero;
		if (k_sag)
		{
			if (grounded)
			{
				vec.x += moveForce;
			}
			else
			{
				vec.x += moveForce * num;
			}
			if (GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			{
				GetComponent<Rigidbody2D>().AddForce(vec);
			}
		}
		if (k_sol)
		{
			if (grounded)
			{
				vec.x -= moveForce;
			}
			else
			{
				vec.x -= moveForce * num;
			}
			if (GetComponent<Rigidbody2D>().velocity.x > 0f - maxSpeed)
			{
				GetComponent<Rigidbody2D>().AddForce(vec);
			}
		}
		if (grounded)
		{
			CpointEkle();
		}
	}

	private void CpointEkle()
	{
		bool flag = true;
		for (int i = 0; i < CPoints.Count; i++)
		{
			float num = Vector2.Distance(CPoints[i], base.transform.position);
			if (num < 10f)
			{
				flag = false;
			}
		}
		if (flag)
		{
			CPoints.Add(base.transform.position);
		}
	}

	private void BayrakEkle(Vector2 nere, bool Sari = false)
	{
		GameObject gameObject = null;
		FontSprites fontSprites = null;
		GameObject gameObject2 = Object.Instantiate(Resources.Load("BayrakX", typeof(GameObject))) as GameObject;
		for (int num = gameObject2.transform.childCount - 1; num >= 0; num--)
		{
			if (gameObject2.transform.GetChild(num).gameObject.name == "Font")
			{
				gameObject = gameObject2.transform.GetChild(num).gameObject;
			}
		}
		if (gameObject != null)
		{
			fontSprites = gameObject.GetComponent<FontSprites>();
		}
		if (fontSprites != null)
		{
			string kelime = PointSay.ToString();
			if (Sari)
			{
				fontSprites.colorTint = Color.yellow;
				gameObject2.name = "Upo";
			}
			else
			{
				gameObject2.name = "Cpo";
			}
			fontSprites.MetinDegis(kelime);
			PointSay++;
		}
		gameObject2.transform.position = nere;
		PointsGo.Add(gameObject2);
	}

	public void UpointEkle()
	{
		for (int i = 0; i < UPoints.Count; i++)
		{
			float num = Vector2.Distance(UPoints[i], base.transform.position);
			if (num < 8f)
			{
				return;
			}
		}
		CPoints.Add(base.transform.position);
		UPoints.Add(base.transform.position);
		GameObject gameObject = Object.Instantiate(Resources.Load("BayrakX", typeof(GameObject))) as GameObject;
		gameObject.AddComponent<UpointSc>();
		PointsGo.Add(gameObject);
		GameObject gameObject2 = null;
		FontSprites fontSprites = null;
		for (int num2 = gameObject.transform.childCount - 1; num2 >= 0; num2--)
		{
			if (gameObject.transform.GetChild(num2).gameObject.name == "Font")
			{
				gameObject2 = gameObject.transform.GetChild(num2).gameObject;
			}
		}
		if (gameObject2 != null)
		{
			fontSprites = gameObject2.GetComponent<FontSprites>();
		}
		if (fontSprites != null)
		{
			fontSprites.colorTint = Color.yellow;
			string kelime = PointSay.ToString();
			PointSay++;
			fontSprites.MetinDegis(kelime);
		}
		gameObject.transform.position = base.transform.position;
	}

	public void CpointSistem()
	{
		parnakSc.Chepsini_gizle();
		PointSay = 1;
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < UPoints.Count; i++)
		{
			list.Add(UPoints[i]);
			BayrakEkle(UPoints[i], true);
		}
		for (int j = 0; j < CPoints.Count; j++)
		{
			bool flag = true;
			for (int k = 0; k < UPoints.Count; k++)
			{
				float num = Vector2.Distance(CPoints[j], UPoints[k]);
				if (num < 8f)
				{
					flag = false;
				}
			}
			if (flag)
			{
				list.Add(CPoints[j]);
				BayrakEkle(CPoints[j]);
			}
		}
		CPoints.Clear();
		for (int l = 0; l < list.Count; l++)
		{
			CPoints.Add(list[l]);
		}
		list.Clear();
		CpointAktif = true;
		CAktifNokta = CPoints.Count - 1;
		OncekiNokta = base.transform.position;
		if (CPoints.Count > 0)
		{
			SonrakiNokta = CPoints[CPoints.Count - 1];
			CpointGidiyor = true;
		}
	}

	public void SonrakiPoint()
	{
		if (CPoints.Count > 1 && CAktifNokta != CPoints.Count - 1 && CPoints.Count > CAktifNokta + 1)
		{
			SonrakiNokta = CPoints[CAktifNokta + 1];
			OncekiNokta = CPoints[CAktifNokta];
			CpointGidiyor = true;
			ilkZaman = Time.realtimeSinceStartup;
			CAktifNokta++;
		}
	}

	public void OncekiPoint()
	{
		if (CPoints.Count > 1 && CAktifNokta != 0 && CPoints[CAktifNokta - 1] != Vector2.zero)
		{
			SonrakiNokta = CPoints[CAktifNokta - 1];
			OncekiNokta = CPoints[CAktifNokta];
			CpointGidiyor = true;
			ilkZaman = Time.realtimeSinceStartup;
			CAktifNokta--;
		}
	}

	private void CpointHareket()
	{
		if (CpointAktif && CpointGidiyor)
		{
			float t = (Time.realtimeSinceStartup - ilkZaman) / Cspeed;
			if (Camera.main.transform.position != SonrakiNokta)
			{
				Vector3 position = Vector3.Lerp(OncekiNokta, SonrakiNokta, t);
				position.z = -10f;
				Camera.main.transform.position = position;
			}
		}
	}

	public void PointsGoSil()
	{
		for (int i = 0; i < PointsGo.Count; i++)
		{
			PointsGo[i].SetActive(false);
		}
		if (!PointsGo[0].activeInHierarchy)
		{
			PointsGo.Clear();
		}
	}

	private void SesLoopOk()
	{
		CameraSc.sesLoopAc();
	}

	private void AliveSesVer()
	{
		CameraSc.AliveSesi();
		Invoke("SesLoopOk", 1.5f);
	}

	public void CkalkanAC()
	{
		int @int = PlayerPrefs.GetInt("LostSay");
		if (@int < 7)
		{
			@int++;
			PlayerPrefs.SetInt("LostSay", @int);
			PlayerPrefs.Save();
		}
		k_sag = false;
		k_sol = false;
		k_ust = false;
		k_kalkan = false;
		k_jet = false;
		k_bomba = false;
		Geberdi = false;
		Gdustu = false;
		Invoke("AliveSesVer", 0.2f);
		parnakSc.Chepsini_goster();
		Time.timeScale = 1f;
		Vector3 position = CPoints[CAktifNokta];
		base.transform.position = position;
		int int2 = PlayerPrefs.GetInt("KalkanDepo");
		int2++;
		PlayerPrefs.SetInt("KalkanDepo", int2);
		KalkanAktifle();
		anim.SetBool("dead", false);
		anim.SetBool("kizgin", false);
		anim.SetBool("uzgun", false);
		anim.SetBool("normal", true);
		Invoke("geberMenuSifirla", 1f);
		GetComponent<Rigidbody2D>().rotation = 0f;
		CpointAktif = false;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().angularVelocity = 0f;
		PointSay = 1;
		Diken[] array = Object.FindObjectsOfType(typeof(Diken)) as Diken[];
		Diken[] array2 = array;
		foreach (Diken diken in array2)
		{
			diken.Sifirla();
		}
		Testere[] array3 = Object.FindObjectsOfType(typeof(Testere)) as Testere[];
		Testere[] array4 = array3;
		foreach (Testere testere in array4)
		{
			testere.Sifirla();
		}
		DikenLiYaprak[] array5 = Object.FindObjectsOfType(typeof(DikenLiYaprak)) as DikenLiYaprak[];
		DikenLiYaprak[] array6 = array5;
		foreach (DikenLiYaprak dikenLiYaprak in array6)
		{
			dikenLiYaprak.Sifirla();
		}
		RobotBombasi[] array7 = Object.FindObjectsOfType(typeof(RobotBombasi)) as RobotBombasi[];
		RobotBombasi[] array8 = array7;
		foreach (RobotBombasi robotBombasi in array8)
		{
			robotBombasi.Sifirla();
		}
		RobotPervDummy[] array9 = Object.FindObjectsOfType(typeof(RobotPervDummy)) as RobotPervDummy[];
		RobotPervDummy[] array10 = array9;
		foreach (RobotPervDummy robotPervDummy in array10)
		{
			robotPervDummy.Sifirla();
		}
		Yanardag[] array11 = Object.FindObjectsOfType(typeof(Yanardag)) as Yanardag[];
		Yanardag[] array12 = array11;
		foreach (Yanardag yanardag in array12)
		{
			yanardag.Sifirla();
		}
		YukselenSu[] array13 = Object.FindObjectsOfType(typeof(YukselenSu)) as YukselenSu[];
		YukselenSu[] array14 = array13;
		foreach (YukselenSu yukselenSu in array14)
		{
			yukselenSu.Sifirla();
		}
	}
}
