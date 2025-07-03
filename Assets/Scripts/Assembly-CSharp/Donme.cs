using UnityEngine;

public class Donme : MonoBehaviour
{
	public float Hiz = 1f;

	private void FixedUpdate()
	{
		base.transform.Rotate(0f, 0f, Hiz);
	}
}
