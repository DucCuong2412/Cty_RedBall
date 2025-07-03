using UnityEngine;

public class ParticleLayer : MonoBehaviour
{
	public string LayerName = "Oyun";

	public int LayerOrder;

	public bool RenkgaRenk;

	private void Start()
	{
		if (LayerName != string.Empty)
		{
			GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = LayerName;
		}
		if (LayerOrder != 0)
		{
			GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = LayerOrder;
		}
	}

	private void FixedUpdate()
	{
		if (RenkgaRenk)
		{
			Color white = Color.white;
			bool flag = Random.value > 0.5f;
			float value = Random.value;
			int num = Random.Range(0, 3);
			white[num] = value;
			white[(num + (flag ? 1 : 2)) % 3] = 1f;
			white[(num + ((!flag) ? 1 : 2)) % 3] = 0f;
			ParticleSystem component = GetComponent<ParticleSystem>();
			ParticleSystem.MainModule main = component.main;
			main.startColor = white;
		}
	}
}
