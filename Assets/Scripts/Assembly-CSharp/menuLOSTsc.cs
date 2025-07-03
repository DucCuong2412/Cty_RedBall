using UnityEngine;

public class menuLOSTsc : MonoBehaviour
{
	private GameObject vitrinJet;

	private GameObject vitrinKalkan;

	private GameObject moneyGo;

	private FontSprites ButceFS;

	private int LostSay;

	private float araZaman;

	private SpriteRenderer SR;

	public void ParaGuncelle()
	{
		string text = PlayerPrefs.GetInt("MevcutPara").ToString();
		string kelime = "You have: $ " + text;
		if ((bool)ButceFS)
		{
			ButceFS.MetinDegis(kelime);
		}
	}

	private void Start()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "bonus_jet")
			{
				vitrinJet = gameObject.gameObject;
				vitrinJet.SetActive(false);
			}
			if (gameObject.name == "bonus_zirh")
			{
				vitrinKalkan = gameObject.gameObject;
				vitrinKalkan.SetActive(false);
			}
			if (gameObject.name == "MevcutMoney")
			{
				moneyGo = gameObject.gameObject;
			}
			if (gameObject.name == "butonCpoints")
			{
				for (int num2 = gameObject.gameObject.transform.childCount - 1; num2 >= 0; num2--)
				{
					GameObject gameObject2 = gameObject.gameObject.transform.GetChild(num2).gameObject;
					if (gameObject2.name == "soruisa2")
					{
						SR = gameObject2.gameObject.GetComponent<SpriteRenderer>();
					}
				}
			}
		}
		if ((bool)moneyGo)
		{
			ButceFS = moneyGo.GetComponent<FontSprites>();
		}
		LostSay = PlayerPrefs.GetInt("LostSay");
		araZaman = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if (LostSay < 5 && (bool)SR)
		{
			float num = Time.realtimeSinceStartup - araZaman;
			if (num > 1f)
			{
				araZaman = Time.realtimeSinceStartup;
			}
			else if (num > 0.5f)
			{
				SR.color = Color.black;
			}
			else
			{
				SR.color = Color.yellow;
			}
		}
	}
}
