using UnityEngine;

public class UnlemModify : MonoBehaviour
{
	public float Mesafe = 1f;

	public float Carpan = 1f;

	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void Update()
	{
		Vector3 position = startPos;
		position.y += Carpan * Mathf.Sin(Time.time * Mesafe);
		base.transform.position = position;
	}
}
