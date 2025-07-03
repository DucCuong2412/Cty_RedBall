using UnityEngine;

public class UnlemAnim : MonoBehaviour
{
	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void Update()
	{
		Vector3 position = startPos;
		position.y += 0.3f * Mathf.Sin(Time.time * 4f);
		base.transform.position = position;
	}
}
