using UnityEngine;

public class GizlenenBomba : MonoBehaviour
{
	public int KayolmaSuresi = 5;

	private void Start()
	{
		Invoke("Kaybet", KayolmaSuresi);
	}

	private void Kaybet()
	{
		base.gameObject.SetActive(false);
		Object.Destroy(this);
	}
}
