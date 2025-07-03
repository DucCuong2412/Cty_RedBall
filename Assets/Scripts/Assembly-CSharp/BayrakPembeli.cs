using UnityEngine;
using UnityEngine.SceneManagement;

public class BayrakPembeli : MonoBehaviour
{
	private bool MenuGeldi;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!MenuGeldi && kim.gameObject.name == "PinkBall")
		{
			Camera.main.GetComponent<CameraFollow>().LevelBitti();
			MenuGeldi = true;
			string text = SceneManager.GetActiveScene().name;
			text = text.Replace("Level_", string.Empty);
			int num = int.Parse(text);
			int @int = PlayerPrefs.GetInt("sonLevelRB2");
			if (@int < num)
			{
				PlayerPrefs.SetInt("sonLevelRB2", num);
			}
		}
	}
}
