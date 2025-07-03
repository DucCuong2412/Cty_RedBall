using UnityEngine;

public class ElleControler : MonoBehaviour
{
	private GameObject Elle;

	private GameObject Okla;

	public static int durum;

	private float araZaman;

	public void ElGizle()
	{
		Elle.SetActive(false);
	}

	private void Git(Vector3 konum)
	{
		base.transform.localPosition = konum;
	}

	private void Awake()
	{
		durum = 1;
		Elle = Object.Instantiate(Resources.Load("elle1", typeof(GameObject))) as GameObject;
		Elle.transform.parent = base.transform;
		ElGit(0);
		Okla = Object.Instantiate(Resources.Load("maketOKal", typeof(GameObject))) as GameObject;
		Okla.SetActive(false);
	}

	public void OkKoy()
	{
		CameraFollow component = Camera.main.GetComponent<CameraFollow>();
		Transform parent = component.MenuTransformVer(1);
		Okla.transform.parent = parent;
		Okla.transform.localPosition = new Vector3(-0.001866837f, -0.0004595795f, 0f);
		Okla.SetActive(true);
	}

	public void OkGizle()
	{
		Okla.SetActive(false);
	}

	public void ElAktar(Transform kime)
	{
		Elle.transform.parent = kime;
	}

	public void ElGit(int durum)
	{
		if (durum == 0)
		{
			Elle.transform.localPosition = new Vector3(-0.0005f, -0.005f, 0f);
		}
		if (durum == 1)
		{
			Vector3 localScale = Elle.transform.localScale;
			localScale.x *= -1f;
			Elle.transform.localScale = localScale;
			Elle.transform.localPosition = new Vector3(-0.004412267f, 0.001353105f, 0f);
		}
		if (durum == 2)
		{
			Vector3 localScale2 = Elle.transform.localScale;
			localScale2.x *= -1f;
			Elle.transform.localScale = localScale2;
			Elle.transform.localPosition = new Vector3(0.003176974f, -0.003889618f, 0f);
		}
		if (durum == 3)
		{
			Elle.transform.localPosition = new Vector3(0.005224169f, 0.002342571f, 0f);
		}
	}

	private void Start()
	{
		araZaman = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if (durum == 5)
		{
			float num = Time.realtimeSinceStartup - araZaman;
			if (num > 0.04f)
			{
				araZaman = Time.realtimeSinceStartup;
				Okla.transform.Rotate(0f, 0f, -10f);
			}
		}
	}
}
