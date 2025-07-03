using UnityEngine;

public class Buton : MonoBehaviour
{
	private bool Basti;

	private SliderJoint2D slider;

	private void Start()
	{
		slider = GetComponent<SliderJoint2D>();
	}

	private void FixedUpdate()
	{
		if (slider.jointTranslation < 0f)
		{
			Basti = false;
		}
		if (!Basti && slider.jointTranslation > 0.1f)
		{
			Basti = true;
			base.transform.parent.GetComponent<ButonKime>().Basti();
			Invoke("GeriGeldi", 1f);
		}
	}

	private void GeriGeldi()
	{
		Basti = false;
		base.transform.parent.GetComponent<ButonKime>().RenkSil();
	}
}
