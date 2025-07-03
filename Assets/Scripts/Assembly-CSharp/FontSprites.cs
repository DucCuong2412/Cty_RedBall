using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FontSprites : MonoBehaviour
{
	public enum EkranaYeri
	{
		Hicbirsey = 0,
		UstOrta = 1,
		SagUst = 2,
		SagOrta = 3,
		SagDip = 4,
		DipOrta = 5,
		SolDip = 6,
		SolOrta = 7,
		SolUst = 8,
		TamOrta = 9
	}

	public enum HorzAlignment
	{
		Sol = 0,
		Orta = 1,
		Sag = 2
	}

	public enum VertAlignment
	{
		Tepe = 0,
		Orta = 1,
		Alt = 2
	}

	private bool HepsiniGizle;

	private string CharsB = "0123456789ABCDEFGHIJKLMNOPQRSTUVYZWXabcdefghijklmnopqrstuvyzwx/:?!*#.%$";

	private string MiniHarfs = "jyqpg";

	[TextArea(3, 10)]
	[SerializeField]
	protected string Ntext = string.Empty;

	public Texture2D AnaTexture;

	public string SpritePrefix = "txt_";

	public string CharsBmenu = "ABCDEFGHIJKLMNOPQRSTUVYZWX";

	public Color colorTint = Color.white;

	public EkranaYeri EkranaHizala;

	[SerializeField]
	public HorzAlignment YatayHizala = HorzAlignment.Sag;

	[SerializeField]
	public VertAlignment DikeyHizala;

	public float AraMesafe;

	public float Buyut = 1f;

	public string Layerismi = string.Empty;

	public int LayerOrderNo;

	[HideInInspector]
	[SerializeField]
	private List<GameObject> ChildsGo = new List<GameObject>();

	[HideInInspector]
	public Sprite[] HarfSprites;

	private GameObject RootGo;

	[HideInInspector]
	[SerializeField]
	private float KelimeBoyu;

	private int AktifGo;

	private void Start()
	{
		EkranaOzelHiza();
	}

	private void EkranaOzelHiza()
	{
		if (HarfSprites.Length == 0)
		{
			MonoBehaviour.print("EKRANDA ARRAY OLMAYAN Bi FONTSPRITES VaR");
			return;
		}
		Vector2 vector = HarfSprites[0].bounds.extents;
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 1f));
		Vector3 vector3 = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 1f));
		Vector3 position = new Vector3(-1f, -1f, -1f);
		switch (EkranaHizala)
		{
		case EkranaYeri.UstOrta:
			position.x = (vector3.x + vector2.x) * 0.5f;
			position.y = vector3.y - vector.y;
			position.z = -10f;
			break;
		case EkranaYeri.SagUst:
			position.x = vector3.x - vector.x * 3f;
			position.y = vector3.y - vector.y;
			position.z = -10f;
			break;
		case EkranaYeri.SagOrta:
			position.x = vector3.x - vector.x;
			position.y = (vector3.y + vector2.y) * 0.5f;
			position.z = -10f;
			break;
		case EkranaYeri.SagDip:
			position.x = vector3.x - vector.x;
			position.y = vector2.y + vector.y;
			position.z = -10f;
			break;
		case EkranaYeri.DipOrta:
			position.x = (vector3.x + vector2.x) * 0.5f;
			position.y = vector2.y + vector.y;
			position.z = -10f;
			break;
		case EkranaYeri.SolDip:
			position.x = vector2.x + vector.x;
			position.y = vector2.y + vector.y;
			position.z = -10f;
			break;
		case EkranaYeri.SolOrta:
			position.x = vector2.x + vector.x;
			position.y = (vector2.y + vector3.y) * 0.5f;
			position.z = -10f;
			break;
		case EkranaYeri.SolUst:
			position.x = vector2.x + vector.x;
			position.y = vector3.y - vector.y;
			position.z = -10f;
			break;
		case EkranaYeri.TamOrta:
			position.x = (vector3.x + vector2.x) * 0.5f;
			position.y = (vector2.y + vector3.y) * 0.5f;
			position.z = -10f;
			break;
		}
		if (position.x != -1f)
		{
			base.transform.position = position;
		}
	}

	public void KameraBuyudu()
	{
		EkranaOzelHiza();
		RootGoBul();
		Hizala();
	}

	private void OnEnable()
	{
		RootGoBul();
		Hizala();
	}

	private void Hizala()
	{
		if (KelimeBoyu != 0f)
		{
			Vector2 vector = HarfSprites[0].bounds.extents;
			RootGo.transform.localPosition = Vector3.zero;
			Vector3 zero = Vector3.zero;
			switch (YatayHizala)
			{
			case HorzAlignment.Sol:
				zero.x -= KelimeBoyu * Buyut;
				break;
			case HorzAlignment.Orta:
				zero.x -= KelimeBoyu * 0.5f * Buyut;
				break;
			}
			switch (DikeyHizala)
			{
			case VertAlignment.Orta:
				zero.y -= HarfSprites[1].bounds.extents.y * Buyut;
				break;
			case VertAlignment.Alt:
				zero.y -= HarfSprites[1].bounds.extents.y * 2f * Buyut;
				break;
			}
			zero.z = 0f;
			if (YatayHizala == HorzAlignment.Orta)
			{
				Vector3 zero2 = Vector3.zero;
				zero2.x -= KelimeBoyu * 0.5f;
				zero2.y -= vector.y;
				zero2.z = 0f;
				RootGo.transform.localPosition = zero2;
			}
			RootGo.transform.localScale = Vector3.one * Buyut;
			if (EkranaHizala != 0)
			{
				base.transform.parent = Camera.main.transform;
				zero.z = 1f;
				RootGo.transform.localPosition = zero;
			}
			else
			{
				zero.z = 1f;
				RootGo.transform.localPosition = zero;
			}
		}
	}

	private bool RootGoBul()
	{
		if (RootGo != null)
		{
			return true;
		}
		bool result = false;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name == "Root")
				{
					result = true;
					RootGo = transform.gameObject;
				}
			}
			return result;
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public void Boya(Color Renk)
	{
		foreach (GameObject item in ChildsGo)
		{
			if (!(item == null))
			{
				item.gameObject.GetComponent<SpriteRenderer>().color = colorTint;
			}
		}
	}

	public void MetinDegis(string Kelime)
	{
		string text = Kelime.Replace(" ", string.Empty);
		RootGoBul();
		if (AktifGo != text.Length)
		{
			if (text.Length > ChildsGo.Count)
			{
				for (int i = ChildsGo.Count; i < text.Length; i++)
				{
					string text2 = text[i].ToString();
					if (!text2.Contains(" "))
					{
						GameObject gameObject = null;
						gameObject = new GameObject("_ek");
						SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
						spriteRenderer.sortingLayerName = Layerismi;
						spriteRenderer.sortingOrder = LayerOrderNo;
						gameObject.transform.parent = RootGo.transform;
						if (HepsiniGizle)
						{
							gameObject.hideFlags = HideFlags.HideInHierarchy;
						}
						gameObject.transform.localScale = Vector3.one;
						ChildsGo.Add(gameObject);
					}
				}
				AktifGo = ChildsGo.Count;
			}
			else
			{
				for (int j = text.Length; j < ChildsGo.Count; j++)
				{
					ChildsGo[j].GetComponent<Renderer>().enabled = false;
				}
				AktifGo = text.Length;
			}
		}
		int num = -1;
		KelimeBoyu = 0f;
		float num2 = 0f;
		GameObject gameObject2 = null;
		int num3 = 0;
		float num4 = RootGo.transform.position.x;
		float num5 = 1f;
		if (Camera.main.orthographicSize > 3.5f)
		{
			num5 = Camera.main.orthographicSize / 3.5f;
		}
		if (base.transform.parent != null && base.transform.parent.gameObject.name.Contains("Camera"))
		{
			num5 = 1f;
			if (Camera.main.orthographicSize > 4f)
			{
				Buyut = 1f;
			}
		}
		for (int k = 0; k < Kelime.Length; k++)
		{
			string text2 = Kelime[k].ToString();
			if (text2.Contains(" "))
			{
				KelimeBoyu += HarfSprites[0].bounds.extents.x * num5 * Buyut;
				num4 += HarfSprites[0].bounds.extents.x * num5 * Buyut;
				continue;
			}
			num = ((!(CharsBmenu != string.Empty)) ? CharsB.IndexOf(text2) : CharsBmenu.IndexOf(text2));
			if (num != -1)
			{
				gameObject2 = ChildsGo[num3].gameObject;
				num3++;
				if (!gameObject2.GetComponent<Renderer>().enabled)
				{
					gameObject2.GetComponent<Renderer>().enabled = true;
				}
				if (HarfSprites[num] == null)
				{
					MonoBehaviour.print("Harf Dizisi Hatalı");
					return;
				}
				SpriteRenderer component = gameObject2.GetComponent<SpriteRenderer>();
				component.sprite = HarfSprites[num];
				component.color = colorTint;
				num4 += num2;
				num2 = component.sprite.bounds.size.x * Buyut * num5;
				KelimeBoyu += num2;
				Vector3 zero = Vector3.zero;
				zero.x = num4 + AraMesafe + component.sprite.bounds.extents.x;
				zero.y = RootGo.transform.position.y;
				zero.z = RootGo.transform.position.z;
				if (MiniHarfs.IndexOf(text2) != -1)
				{
					zero.y -= component.sprite.bounds.extents.y * 0.4f;
				}
				gameObject2.transform.position = zero;
				gameObject2.transform.parent = RootGo.transform;
			}
		}
		Hizala();
	}
}
