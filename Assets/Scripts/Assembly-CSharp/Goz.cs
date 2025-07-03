using System;
using System.Collections;
using UnityEngine;

public class Goz : MonoBehaviour
{
	private Vector3[] GozCenter;

	private Transform[] GozPos;

	private float[] GozCap;

	private Transform PlayTrans;

	private int bebekSay;

	private void Awake()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			PlayTrans = gameObject.transform;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name.Contains("bebek_"))
				{
					bebekSay++;
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
		GozCap = new float[bebekSay];
		GozCenter = new Vector3[bebekSay];
		GozPos = new Transform[bebekSay];
		bebekSay = 0;
		IEnumerator enumerator2 = base.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				Transform transform2 = (Transform)enumerator2.Current;
				string text = transform2.name;
				if (text.Contains("bebek_"))
				{
					GozCenter[bebekSay] = transform2.gameObject.transform.localPosition;
					GozPos[bebekSay] = transform2.gameObject.transform;
					GozCap[bebekSay] = 0.1f;
					if (text.Contains("mini_"))
					{
						GozCap[bebekSay] = 0.05f;
					}
					bebekSay++;
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = enumerator2 as IDisposable) != null)
			{
				disposable2.Dispose();
			}
		}
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < bebekSay; i++)
		{
			Vector3 vector = PlayTrans.position - GozPos[i].position;
			vector = Vector3.ClampMagnitude(vector, GozCap[i]);
			Vector3 localPosition = GozCenter[i] + vector;
			GozPos[i].localPosition = localPosition;
		}
	}
}
