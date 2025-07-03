using UnityEngine;

public class RobotRedFire : MonoBehaviour
{
	public float Hayatsuresi = 2f;

	public void Gizleyici()
	{
		Invoke("Gizle", Hayatsuresi);
	}

	private void Gizle()
	{
		base.gameObject.SetActive(false);
	}
}
