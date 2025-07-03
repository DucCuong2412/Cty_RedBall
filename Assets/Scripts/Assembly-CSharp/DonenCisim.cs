using UnityEngine;

public class DonenCisim : MonoBehaviour
{
	public bool Aktif;

	public int DonmeHizi = 10;

	private void FixedUpdate()
	{
		if (Aktif)
		{
			if (base.transform.eulerAngles.z < 110f && base.transform.eulerAngles.z > 100f)
			{
				Aktif = false;
			}
			base.transform.Rotate(new Vector3(0f, 0f, DonmeHizi));
		}
	}

	public void AktifYap()
	{
		if (!Aktif)
		{
			Aktif = true;
		}
	}
}
