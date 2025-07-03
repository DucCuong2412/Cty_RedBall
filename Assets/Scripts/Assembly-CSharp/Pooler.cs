using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
	protected List<GameObject> DepoDizisi;

	protected GameObject PoolerGo;

	public string ADdegis = string.Empty;

	public bool DiziBuyuyebilir;

	public int DepoSayisi = 10;

	private bool HideHierarsi;

	protected void PoolerAwake()
	{
		DepoDizisi = new List<GameObject>();
		for (int i = 0; i < DepoSayisi; i++)
		{
			GameObject gameObject = Object.Instantiate(PoolerGo, base.transform.position, base.transform.rotation);
			if (ADdegis != string.Empty)
			{
				gameObject.name = ADdegis + "_" + i;
			}
			else
			{
				gameObject.name = "pool1";
			}
			gameObject.SetActive(false);
			if (HideHierarsi)
			{
				gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
			DepoDizisi.Add(gameObject);
		}
		PoolerGo.transform.parent = null;
		PoolerGo.SetActive(false);
		DepoDizisi.Add(PoolerGo);
	}

	protected void PoolerGoDegis(int no, GameObject Go)
	{
		DepoDizisi[no] = Go;
	}

	protected GameObject DepodanAl()
	{
		for (int i = 0; i < DepoDizisi.Count; i++)
		{
			if (!DepoDizisi[i].activeInHierarchy)
			{
				return DepoDizisi[i];
			}
		}
		if (DiziBuyuyebilir)
		{
			GameObject gameObject = Object.Instantiate(PoolerGo, base.transform.position, base.transform.rotation);
			if (ADdegis != string.Empty)
			{
				gameObject.name = ADdegis + "_X";
			}
			else
			{
				gameObject.name = "pool2";
			}
			if (HideHierarsi)
			{
				gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
			DepoDizisi.Add(gameObject);
			return gameObject;
		}
		return null;
	}
}
