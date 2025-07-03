using UnityEngine;
using UnityEngine.SceneManagement;

public class Bayrak : MonoBehaviour
{
	private bool MenuGeldi;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!MenuGeldi && kim.gameObject.tag == "Player")
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
			MenuGeldi = true;
		}
	}
}
