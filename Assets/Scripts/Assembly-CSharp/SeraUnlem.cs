using UnityEngine;

public class SeraUnlem : MonoBehaviour
{
	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void Update()
	{
		Vector3 position = startPos;
		position.y += 0.1f * Mathf.Sin(Time.time * 0.7f);
		base.transform.position = position;
	}
}
