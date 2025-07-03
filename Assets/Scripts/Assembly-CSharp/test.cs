using UnityEngine;

public class test : MonoBehaviour
{
	private void Start()
	{
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		MonoBehaviour.print("L:" + component.sprite.bounds.extents.x);
	}
}
