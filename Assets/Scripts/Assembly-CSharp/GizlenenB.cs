using UnityEngine;

public class GizlenenB : MonoBehaviour
{
	private GameObject lamba;

	private float tim;

	private bool Gidiyor;

	private bool Gitti;

	private Vector3[] lambas = new Vector3[3];

	private SpriteRenderer SR;

	private BoxCollider2D Box;

	public float GorunmeSuresi = 1f;

	public float GizlenmeSure = 2f;

	private void Start()
	{
		lambas[0] = new Vector3(-0.3473756f, -0.02342033f, 0f);
		lambas[1] = new Vector3(-0.003323555f, -0.02342033f, 0f);
		lambas[2] = new Vector3(0.3842688f, -0.02342033f, 0f);
		Box = GetComponent<BoxCollider2D>();
		SR = GetComponent<SpriteRenderer>();
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "lamba")
			{
				lamba = gameObject;
			}
		}
		lamba.SetActive(false);
		Invoke("Gizle", GorunmeSuresi);
	}

	private void Goster()
	{
		tim = 0f;
		Gidiyor = (Gitti = false);
		lamba.SetActive(false);
		lamba.transform.position = lambas[0];
		Box.enabled = true;
		SR.enabled = true;
		Invoke("Gizle", GorunmeSuresi);
	}

	private void Gizle()
	{
		lamba.SetActive(true);
		Gidiyor = true;
	}

	private void FixedUpdate()
	{
		if (Gitti || !Gidiyor)
		{
			return;
		}
		tim += Time.deltaTime * 5f;
		int num = Mathf.RoundToInt(tim);
		if (num == 0 || num == 1 || num == 2 || num == 3)
		{
			if (num == 3)
			{
				Gitti = true;
				Box.enabled = false;
				SR.enabled = false;
				Invoke("Goster", GizlenmeSure);
				lamba.SetActive(false);
			}
			else
			{
				lamba.transform.localPosition = lambas[num];
			}
		}
	}
}
