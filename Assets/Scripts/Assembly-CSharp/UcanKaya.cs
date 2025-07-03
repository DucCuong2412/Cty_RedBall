using UnityEngine;

public class UcanKaya : MonoBehaviour
{
	private float Agirlik;

	private void Start()
	{
		Agirlik = GetComponent<Rigidbody2D>().mass * 0.1f;
	}

	private void FixedUpdate()
	{
		Vector3 position = base.transform.position;
		position.y += Mathf.Sin(Time.time) * Time.deltaTime * Agirlik;
		base.transform.position = position;
	}
}
