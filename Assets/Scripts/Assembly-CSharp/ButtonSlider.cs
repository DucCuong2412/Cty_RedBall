using UnityEngine;

public class ButtonSlider : MonoBehaviour
{
	private float SliderMax;

	private GameObject SlidGo;

	private GameObject SliderMaxGo;

	private FontSprites SesLevelTxt;

	protected int DokunmaID = -1;

	protected Vector3 parnak;

	private void Start()
	{
		SlidGo = GameObject.Find("SesAyarUst");
		SliderMaxGo = GameObject.Find("SliderMax");
		SliderMax = SliderMaxGo.transform.localPosition.x;
		SesLevelTxt = GameObject.Find("SesLevelFnt").GetComponent<FontSprites>();
		int @int = PlayerPrefs.GetInt("SesSeviyeVar");
		if (@int == 1)
		{
			int int2 = PlayerPrefs.GetInt("SesSeviyesi");
			float x = SliderMax * (float)int2 / 100f;
			Vector3 localPosition = SlidGo.transform.localPosition;
			localPosition.x = x;
			SlidGo.transform.localPosition = localPosition;
			string kelime = int2.ToString();
			SesLevelTxt.MetinDegis(kelime);
		}
		else
		{
			SesLevelTxt.MetinDegis("100");
		}
	}

	private void Surtundu()
	{
		SurtunmeYeri();
	}

	private void UzakSurtundu()
	{
		SurtunmeYeri();
	}

	private void DokunmaBitti()
	{
		float x = SlidGo.transform.localPosition.x;
		float num = x * 100f / SliderMax;
		if (num < 1f)
		{
			num = 0f;
		}
		if (num > 99f)
		{
			num = 100f;
		}
		int num2 = (int)num;
		string kelime = num2.ToString();
		SesLevelTxt.MetinDegis(kelime);
		PlayerPrefs.SetInt("SesSeviyesi", num2);
		PlayerPrefs.SetInt("SesSeviyeVar", 1);
		float volume = (float)num2 * 0.01f;
		AudioListener.volume = volume;
	}

	private void SurtunmeYeri()
	{
		SliderMaxGo.transform.position = parnak;
		Vector3 localPosition = SlidGo.transform.localPosition;
		localPosition.x = SliderMaxGo.transform.localPosition.x;
		if (localPosition.x > 0f && localPosition.x < SliderMax)
		{
			SlidGo.transform.localPosition = localPosition;
		}
	}

	private void Update()
	{
		if (!(base.gameObject.GetComponent<Collider2D>() != null))
		{
			return;
		}
		if (Input.touchCount < 1)
		{
			parnak = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Input.GetMouseButtonDown(0) && GetComponent<Collider2D>().OverlapPoint(new Vector2(parnak.x, parnak.y)))
			{
				DokunmaID = 1;
			}
			if (Input.GetMouseButtonUp(0))
			{
				DokunmaID = -1;
				DokunmaBitti();
			}
			if (DokunmaID == 1)
			{
				if (GetComponent<Collider2D>().OverlapPoint(new Vector2(parnak.x, parnak.y)))
				{
					Surtundu();
				}
				else
				{
					UzakSurtundu();
				}
			}
			return;
		}
		for (int i = 0; i < Input.touchCount; i++)
		{
			parnak = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
			Vector2 point = new Vector2(parnak.x, parnak.y);
			if (base.gameObject.GetComponent<Collider2D>().OverlapPoint(point))
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					DokunmaID = Input.GetTouch(i).fingerId;
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
}
