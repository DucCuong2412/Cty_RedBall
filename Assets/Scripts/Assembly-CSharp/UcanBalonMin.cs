using UnityEngine;

public class UcanBalonMin : MonoBehaviour
{
	private bool bitti;

	private float Agirlik;

	private void Start()
	{
		Agirlik = GetComponent<Rigidbody2D>().mass * 0.1f;
	}

	private void FixedUpdate()
	{
		if (!bitti)
		{
			Vector3 position = base.transform.position;
			position.y += Mathf.Sin(Time.time) * Time.deltaTime * Agirlik;
			base.transform.position = position;
		}
	}

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (!bitti && kim.gameObject.tag == "Player")
		{
			base.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			bitti = true;
		}
	}
}
