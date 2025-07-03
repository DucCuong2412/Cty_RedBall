using System.Collections.Generic;
using UnityEngine;

public class MenuX : MonoBehaviour
{
	public Texture2D AnaTexture;

	public string SpriteAdi;

	[HideInInspector]
	public Sprite[] MenuSprites;

	[HideInInspector]
	[SerializeField]
	private List<GameObject> ChildsGo = new List<GameObject>();

	private void OnEnable()
	{
		Hizala();
	}

	public void Boya()
	{
		Color color = GetComponent<SpriteRenderer>().color;
		for (int i = 0; i < ChildsGo.Count; i++)
		{
			ChildsGo[i].GetComponent<SpriteRenderer>().color = color;
		}
	}

	public void BirkezUret()
	{
		if (MenuSprites.Length < 1)
		{
			Debug.Log("Ana Texture paketini getir !");
			return;
		}
		ChildsGo.Clear();
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		for (int i = 0; i < MenuSprites.Length; i++)
		{
			GameObject gameObject = null;
			gameObject = new GameObject("ken_" + i, typeof(SpriteRenderer));
			SpriteRenderer component2 = gameObject.GetComponent<SpriteRenderer>();
			component2.sprite = MenuSprites[i];
			component2.sortingLayerName = component.sortingLayerName;
			component2.sortingOrder = component.sortingOrder + 1;
			component2.color = component.color;
			gameObject.transform.parent = base.transform;
			gameObject.hideFlags = HideFlags.HideInHierarchy;
			ChildsGo.Add(gameObject);
		}
		Hizala();
	}

	public void Sil()
	{
		int childCount = base.transform.childCount;
		for (int num = childCount - 1; num > -1; num--)
		{
			if (base.transform.GetChild(num).gameObject.name.Contains("ken_"))
			{
				Object.DestroyImmediate(base.transform.GetChild(num).gameObject);
			}
		}
	}

	public void Gizle()
	{
		int childCount = base.transform.childCount;
		for (int num = childCount - 1; num > -1; num--)
		{
			if (base.transform.GetChild(num).gameObject.name.Contains("ken_"))
			{
				base.transform.GetChild(num).gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
		}
	}

	private void Hizala()
	{
		if (!(SpriteAdi == string.Empty))
		{
			Vector2 vector = GetComponent<Renderer>().bounds.center;
			Vector2 vector2 = GetComponent<Renderer>().bounds.extents;
			ChildsGo[0].transform.position = new Vector2(vector.x, vector.y + vector2.y);
			ChildsGo[1].transform.position = new Vector2(vector.x + vector2.x, vector.y + vector2.y);
			ChildsGo[2].transform.position = new Vector2(vector.x + vector2.x, vector.y);
			ChildsGo[3].transform.position = new Vector2(vector.x + vector2.x, vector.y - vector2.y);
			ChildsGo[4].transform.position = new Vector2(vector.x, vector.y - vector2.y);
			ChildsGo[5].transform.position = new Vector2(vector.x - vector2.x, vector.y - vector2.y);
			ChildsGo[6].transform.position = new Vector2(vector.x - vector2.x, vector.y);
			ChildsGo[7].transform.position = new Vector2(vector.x - vector2.x, vector.y + vector2.y);
			Vector3 localScale = ChildsGo[0].transform.localScale;
			Vector3 localScale2 = ChildsGo[6].transform.localScale;
			localScale.x = 1f;
			localScale2.y = 1f;
			ChildsGo[0].transform.localScale = localScale;
			ChildsGo[4].transform.localScale = localScale;
			ChildsGo[2].transform.localScale = localScale2;
			ChildsGo[6].transform.localScale = localScale2;
			GameObject gameObject = base.transform.GetChild(0).gameObject;
			Color color = gameObject.GetComponent<SpriteRenderer>().color;
			Color color2 = GetComponent<SpriteRenderer>().color;
			if (color != color2)
			{
				Boya();
			}
		}
	}
}
