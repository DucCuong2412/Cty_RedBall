using UnityEngine;

public class VotedSc : MonoBehaviour
{
	private void Start()
	{
		int @int = PlayerPrefs.GetInt("Voted");
		if (@int == 1)
		{
			base.gameObject.SetActive(false);
		}
	}
}
