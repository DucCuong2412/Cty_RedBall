using UnityEngine;

public class FadeTitre : MonoBehaviour
{
	private Vector3 ilkYer;

	public float Aralik = 0.4f;

	public float Hiz = 1f;

	public float islemSuresi = 0.5f;

	private void Start()
	{
		ilkYer = base.transform.position;
		ilkYer.x -= Aralik / 2f;
		Object.Destroy(this, islemSuresi);
	}

	private void Update()
	{
		float x = ilkYer.x;
		float num = Mathf.PingPong(Time.time * Hiz, Aralik);
		x += num;
		base.transform.position = new Vector3(x, ilkYer.y, ilkYer.z);
	}
}
