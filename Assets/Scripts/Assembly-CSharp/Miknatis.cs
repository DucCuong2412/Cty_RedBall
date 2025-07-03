using UnityEngine;

public class Miknatis : MonoBehaviour
{
	private GameObject Go;

	private float Genislik = 3f;

	private float UzunlukCarpan = 100f;

	private Transform Merkez;

	public float DaireCapi = 2f;

	public float CEKIM = 1f;

	public bool Aktif = true;

	private void Start()
	{
		Go = Object.Instantiate(Resources.Load("Nokta", typeof(GameObject))) as GameObject;
		Go.GetComponent<SpriteRenderer>().color = Color.red;
		Go.SetActive(true);
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		if ((bool)base.transform.parent)
		{
			gameObject.transform.parent = base.transform.parent;
		}
		else
		{
			gameObject.transform.parent = null;
		}
		Merkez = gameObject.transform;
	}

	private void Update()
	{
		CizgiCiz(Merkez.position, base.transform.position);
	}

	private void CizgiCiz(Vector3 from, Vector3 to)
	{
		Vector3 vector = to - from;
		Vector3 position = from + vector / 2f;
		position.z = 0f;
		Go.transform.position = position;
		Go.transform.up = vector.normalized;
		Go.transform.localScale = new Vector3(Genislik, vector.magnitude * UzunlukCarpan, Genislik);
		Go.transform.parent = base.transform;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, DaireCapi);
	}

	private void FixedUpdate()
	{
		if (!Aktif)
		{
			return;
		}
		LayerMask layerMask = 1 << LayerMask.NameToLayer("haraketli");
		Vector3 position = base.transform.position;
		position.y -= 0.82f;
		Collider2D[] array = Physics2D.OverlapCircleAll(position, DaireCapi, layerMask);
		Collider2D[] array2 = array;
		Vector2 vector = default(Vector2);
		foreach (Collider2D collider2D in array2)
		{
			Rigidbody2D component = collider2D.gameObject.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				vector.x = collider2D.gameObject.transform.position.x - position.x;
				vector.y = collider2D.gameObject.transform.position.y - position.y;
				if (vector.magnitude > 0.55f)
				{
					component.velocity = vector.normalized * (0f - CEKIM);
				}
			}
		}
	}
}
