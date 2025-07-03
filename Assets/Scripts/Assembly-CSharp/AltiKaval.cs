using UnityEngine;

public class AltiKaval : MonoBehaviour
{
	public Color Renk1;

	public Color Renk2;

	public Vector3 OrtaNokta;

	[HideInInspector]
	public bool NoktaVar;

	public void NoktaEkle()
	{
		OrtaNokta = base.transform.position;
		NoktaVar = true;
	}

	public void RenkLerp()
	{
		Mesh sharedMesh = GetComponent<MeshFilter>().sharedMesh;
		Vector3[] vertices = sharedMesh.vertices;
		Color[] array = new Color[vertices.Length];
		int i = 0;
		float num = 0f;
		float num2 = 100f;
		for (; i < vertices.Length; i++)
		{
			Vector3 a = base.transform.TransformPoint(new Vector3(vertices[i].x, vertices[i].y, vertices[i].z));
			float num3 = Vector3.Distance(a, OrtaNokta);
			if (num < num3)
			{
				num = num3;
			}
			if (num2 > num3)
			{
				num2 = num3;
			}
		}
		for (i = 0; i < vertices.Length; i++)
		{
			Vector3 a = base.transform.TransformPoint(new Vector3(vertices[i].x, vertices[i].y, vertices[i].z));
			float num4 = Vector3.Distance(a, OrtaNokta);
			float t = (num4 - num2) / (num - num2);
			Color color = Color.Lerp(Renk1, Renk2, t);
			array[i] = color;
		}
		sharedMesh.colors = array;
	}

	public void SagSolDegis()
	{
		Mesh sharedMesh = GetComponent<MeshFilter>().sharedMesh;
		Vector3[] vertices = sharedMesh.vertices;
		Color[] array = new Color[vertices.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			if (base.transform.TransformPoint(new Vector3(vertices[i].x, vertices[i].y, vertices[i].z)).x < OrtaNokta.x)
			{
				array[i] = Renk2;
			}
			else
			{
				array[i] = Renk1;
			}
		}
		sharedMesh.colors = array;
	}

	public void RenkDegis()
	{
		Mesh sharedMesh = GetComponent<MeshFilter>().sharedMesh;
		Vector3[] vertices = sharedMesh.vertices;
		Color[] array = new Color[vertices.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			if (base.transform.TransformPoint(new Vector3(vertices[i].x, vertices[i].y, vertices[i].z)).y < OrtaNokta.y)
			{
				array[i] = Renk2;
			}
			else
			{
				array[i] = Renk1;
			}
		}
		sharedMesh.colors = array;
	}
}
