using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_In_Out : MonoBehaviour
{
	private bool iceri;

	private bool LevelSon;

	private bool Pinky;

	private GameObject particle;

	private GameObject RedBl;

	private float AnimSure = 2f;

	private SpriteRenderer SR;

	private bool Yakaladi;

	private Vector3 nereden;

	private Vector3 nereye;

	private float ilkTime;

	private float GidisSuresi = 0.5f;

	private Color Seffaf;

	private Transform portalTwirl;

	private bool SesCaldi;

	private void Start()
	{
		if (base.name.Contains("_Level"))
		{
			LevelSon = true;
		}
		if (base.name.Contains("_Pink"))
		{
			Pinky = true;
		}
		if (base.name.Contains("_In_"))
		{
			iceri = true;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name == "Particle")
				{
					particle = transform.gameObject;
					particle.SetActive(false);
				}
				if (LevelSon && transform.name == "portalTwirl")
				{
					portalTwirl = transform.gameObject.transform;
					transform.gameObject.transform.parent = null;
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
		Seffaf = Color.white;
		Seffaf.a = 0.5f;
		SR = base.gameObject.GetComponent<SpriteRenderer>();
		SR.color = Seffaf;
	}

	private void FixedUpdate()
	{
		if (Yakaladi)
		{
			if (!SesCaldi)
			{
				CameraFollow component = Camera.main.GetComponent<CameraFollow>();
				if ((bool)component)
				{
					SesCaldi = true;
					component.PortalSesi();
				}
			}
			float t = (Time.time - ilkTime) / GidisSuresi;
			RedBl.transform.position = Vector3.Lerp(nereden, nereye, t);
		}
		if (LevelSon && (bool)portalTwirl)
		{
			portalTwirl.Rotate(new Vector3(0f, 0f, -3f));
		}
	}

	private void Gonder()
	{
		if (LevelSon)
		{
			string text = SceneManager.GetActiveScene().name;
			text = text.Replace("Level_", string.Empty);
			int num = int.Parse(text);
			if (num > 1)
			{
			}
			int @int = PlayerPrefs.GetInt("sonLevelRB2");
			if (@int < num)
			{
				PlayerPrefs.SetInt("sonLevelRB2", num);
			}
			Camera.main.GetComponent<CameraFollow>().LevelBitti();
			return;
		}
		string text2 = base.name.Replace("Portal_In_", string.Empty);
		GameObject gameObject = GameObject.Find("Portal_Out_" + text2);
		if (!gameObject)
		{
			Debug.Log("HEDEF PORTAL YOK !");
			Debug.Log("Portal_Out_" + text2);
			return;
		}
		gameObject.GetComponent<Portal_In_Out>().Geldi();
		RedBl.transform.position = gameObject.transform.position;
		Yakaladi = false;
		SesCaldi = false;
		if (RedBl.tag == "Player")
		{
			RedBl.gameObject.GetComponent<Player>().DonduKaldi = false;
		}
		ParticleKapat();
	}

	public void Geldi()
	{
		particle.SetActive(true);
		Invoke("ParticleKapat", AnimSure);
		SR.color = Color.white;
	}

	private void ParticleKapat()
	{
		particle.SetActive(false);
		SR.color = Seffaf;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (Pinky)
		{
			if (base.name.Contains("In_Level") && col.gameObject.name != "PinkBall")
			{
				return;
			}
		}
		else if (base.name.Contains("In_Level") && col.gameObject.tag != "Player")
		{
			return;
		}
		if (iceri && !Yakaladi && !(col.gameObject.GetComponent<Rigidbody2D>() == null))
		{
			particle.SetActive(true);
			RedBl = col.gameObject;
			if (col.gameObject.tag == "Player")
			{
				col.gameObject.GetComponent<Player>().DonduKaldi = true;
			}
			col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			Yakaladi = true;
			nereden = RedBl.transform.position;
			nereye = base.transform.position;
			ilkTime = Time.time;
			Invoke("Gonder", AnimSure);
			SR.color = Color.white;
		}
	}
}
