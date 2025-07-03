using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeSc : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("mainMenu");
		}
	}
}
