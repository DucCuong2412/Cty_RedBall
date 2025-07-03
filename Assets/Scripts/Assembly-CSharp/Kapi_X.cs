using UnityEngine;

public class Kapi_X : MonoBehaviour
{
	public int KacAnahtarGerek = 1;

	private int BulunanAnahtar;

	private float motorSpeed;

	private int JointTipi;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "MerkezNokta")
			{
				gameObject.transform.parent = null;
			}
		}
		SliderJoint2D component = base.gameObject.GetComponent<SliderJoint2D>();
		if ((bool)component)
		{
			motorSpeed = component.motor.motorSpeed;
			JointTipi = 1;
		}
		HingeJoint2D component2 = base.gameObject.GetComponent<HingeJoint2D>();
		if ((bool)component2)
		{
			motorSpeed = component2.motor.motorSpeed;
			JointTipi = 2;
		}
	}

	public void DonenAktifle()
	{
		base.gameObject.GetComponent<DonenCisim>().AktifYap();
	}

	public void AsansorYanAktif()
	{
		base.gameObject.GetComponent<AsansorYan>().AktifYap();
	}

	public void AsansorUstAktif()
	{
		base.gameObject.GetComponent<AsansorUst>().AktifYap();
	}

	public void KayanSagaGit()
	{
		base.gameObject.GetComponent<KayanBlok>().SagaGit();
	}

	public void KayanSolaGit()
	{
		base.gameObject.GetComponent<KayanBlok>().SolaGit();
	}

	public void KayanAsagiGit()
	{
		base.gameObject.GetComponent<KayanBlkAsagi>().AsagiGit();
	}

	public void KayanYukariGit()
	{
		base.gameObject.GetComponent<KayanBlkAsagi>().YukariGit();
	}

	public void Ac()
	{
		if (KacAnahtarGerek > 0)
		{
			BulunanAnahtar++;
			if (BulunanAnahtar < KacAnahtarGerek)
			{
				return;
			}
		}
		switch (JointTipi)
		{
		case 1:
		{
			SliderJoint2D component2 = base.gameObject.GetComponent<SliderJoint2D>();
			JointMotor2D motor = component2.motor;
			motor.motorSpeed = motorSpeed;
			component2.motor = motor;
			component2.useMotor = true;
			break;
		}
		case 2:
		{
			HingeJoint2D component = base.gameObject.GetComponent<HingeJoint2D>();
			JointMotor2D motor = component.motor;
			motor.motorSpeed = motorSpeed;
			component.motor = motor;
			component.useMotor = true;
			break;
		}
		default:
			Debug.Log("Slider Yok");
			break;
		}
	}

	public void Kapat()
	{
		if (KacAnahtarGerek > 0)
		{
			BulunanAnahtar++;
			if (BulunanAnahtar < KacAnahtarGerek)
			{
				return;
			}
		}
		switch (JointTipi)
		{
		case 1:
		{
			SliderJoint2D component2 = base.gameObject.GetComponent<SliderJoint2D>();
			JointMotor2D motor = component2.motor;
			motor.motorSpeed = 0f - motorSpeed;
			component2.motor = motor;
			component2.useMotor = true;
			break;
		}
		case 2:
		{
			HingeJoint2D component = base.gameObject.GetComponent<HingeJoint2D>();
			JointMotor2D motor = component.motor;
			motor.motorSpeed = 0f - motorSpeed;
			component.motor = motor;
			component.useMotor = true;
			break;
		}
		default:
			Debug.Log("Slider Yok");
			break;
		}
	}
}
