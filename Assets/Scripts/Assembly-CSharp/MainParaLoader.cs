using UnityEngine;

public class MainParaLoader : MonoBehaviour
{
	private FontSprites ButceFS;

	private GameObject AdsGO;

	private void Awake()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("AdsMerkex");
		if (gameObject == null)
		{
			AdsGO = Object.Instantiate(Resources.Load("AdCenter", typeof(GameObject))) as GameObject;
			AdsGO.name = "AdsCenterGo";
			Object.DontDestroyOnLoad(AdsGO);
		}
	}

	private void Start()
	{
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		ButceFS = gameObject.GetComponent<FontSprites>();
		Time.timeScale = 1f;
		Guncelle();
	}

	public void Guncelle()
	{
		string text = PlayerPrefs.GetInt("MevcutPara").ToString();
		string kelime = "$ " + text;
		ButceFS.MetinDegis(kelime);
	}
}
