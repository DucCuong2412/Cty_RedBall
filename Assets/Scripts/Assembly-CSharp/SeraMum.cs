using UnityEngine;

public class SeraMum : MonoBehaviour
{
	private void FixedUpdate()
	{
		float num = Mathf.PingPong(Time.time, 0.3f);
		num += 0.7f;
		base.transform.localScale = new Vector3(1f, num, 1f);
	}
}
