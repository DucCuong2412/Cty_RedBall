using UnityEngine;

public class DusmanDead : MonoBehaviour
{
	public enum BocekTuru
	{
		BocekUcanDiken = 1,
		Bocek1 = 2,
		BocekDikenli = 3,
		BocekKanatli = 4,
		BocekSuluk = 5,
		SualtiKirpiB = 6,
		SualtiDisBalik = 7,
		RobotLazerci = 8,
		RobotPervBombaci = 9,
		RobotPervaneli1 = 10,
		RobotRed = 11,
		RobotElektrik = 12,
		SualtiTekGoz = 13,
		UfoDolasan = 14,
		MarsliUzun = 15,
		Tentacle = 16,
		Uzzayli = 17,
		UfoGenel = 18
	}

	public BocekTuru BocekScripti = BocekTuru.Bocek1;

	public void Oldur()
	{
		switch (BocekScripti)
		{
		case BocekTuru.Bocek1:
		{
			Bocek1 component4 = GetComponent<Bocek1>();
			if (component4 != null)
			{
				component4.Oldur();
			}
			break;
		}
		case BocekTuru.BocekUcanDiken:
		{
			BocekUcanDiken component12 = GetComponent<BocekUcanDiken>();
			if (component12 != null)
			{
				component12.Oldur();
			}
			break;
		}
		case BocekTuru.BocekDikenli:
		{
			BocekDikenli component16 = GetComponent<BocekDikenli>();
			if (component16 != null)
			{
				component16.Oldur();
			}
			break;
		}
		case BocekTuru.BocekKanatli:
		{
			BocekKanatli component8 = GetComponent<BocekKanatli>();
			if (component8 != null)
			{
				component8.Oldur();
			}
			break;
		}
		case BocekTuru.BocekSuluk:
		{
			BocekSuluk component18 = GetComponent<BocekSuluk>();
			if (component18 != null)
			{
				component18.Oldur();
			}
			break;
		}
		case BocekTuru.SualtiKirpiB:
		{
			SualtiKirpiB component14 = GetComponent<SualtiKirpiB>();
			if (component14 != null)
			{
				component14.Oldur();
			}
			break;
		}
		case BocekTuru.SualtiDisBalik:
		{
			SualtiDisBalik component10 = GetComponent<SualtiDisBalik>();
			if (component10 != null)
			{
				component10.Oldur();
			}
			break;
		}
		case BocekTuru.RobotLazerci:
		{
			RobotLazerci component6 = GetComponent<RobotLazerci>();
			if (component6 != null)
			{
				component6.Oldur();
			}
			break;
		}
		case BocekTuru.RobotPervBombaci:
		{
			RobotPervBombaci component2 = GetComponent<RobotPervBombaci>();
			if (component2 != null)
			{
				component2.Oldur();
			}
			break;
		}
		case BocekTuru.RobotPervaneli1:
		{
			RobotPervaneli1 component17 = GetComponent<RobotPervaneli1>();
			if (component17 != null)
			{
				component17.Oldur();
			}
			break;
		}
		case BocekTuru.RobotRed:
		{
			RobotRed component15 = GetComponent<RobotRed>();
			if (component15 != null)
			{
				component15.Oldur();
			}
			break;
		}
		case BocekTuru.RobotElektrik:
		{
			RobotElektrik component13 = GetComponent<RobotElektrik>();
			if (component13 != null)
			{
				component13.Oldur();
			}
			break;
		}
		case BocekTuru.SualtiTekGoz:
		{
			SualtiTekGoz component11 = GetComponent<SualtiTekGoz>();
			if (component11 != null)
			{
				component11.Oldur();
			}
			break;
		}
		case BocekTuru.UfoDolasan:
		{
			UfoDolasan component9 = GetComponent<UfoDolasan>();
			if (component9 != null)
			{
				component9.Oldur();
			}
			break;
		}
		case BocekTuru.MarsliUzun:
		{
			MarsliUzun component7 = GetComponent<MarsliUzun>();
			if (component7 != null)
			{
				component7.Oldur();
			}
			break;
		}
		case BocekTuru.Tentacle:
		{
			Tentacle component5 = GetComponent<Tentacle>();
			if (component5 != null)
			{
				component5.Oldur();
			}
			break;
		}
		case BocekTuru.Uzzayli:
		{
			Uzayli component3 = GetComponent<Uzayli>();
			if (component3 != null)
			{
				component3.Oldur();
			}
			break;
		}
		case BocekTuru.UfoGenel:
		{
			Ufo component = GetComponent<Ufo>();
			if (component != null)
			{
				component.Oldur();
			}
			break;
		}
		}
	}
}
