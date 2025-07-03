using UnityEngine;

public class YosunRot : MonoBehaviour
{
	private float rotSpeed = 0.5f;

	private void FixedUpdate()
	{
		float z = Mathf.Sin(Time.time * rotSpeed) * 10f;
		base.transform.rotation = Quaternion.Euler(0f, 0f, z);
	}
}
