using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSpecialPower : MonoBehaviour {

	public static PlayerSpecialPower instance;

	public Image specialPowerBar;
	public float currentPowerAmount;
	public float minValue;
	public float maxValue;
	public int specialPowerCount;
	public int powerLevel;

	public float[] powerLevels;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy (this.gameObject);
		}
	}

	void Start()
	{
		minValue = 0f;
		powerLevel = 1;
		if(powerLevel == 1)
			maxValue = 0.250f;
		else if (powerLevel == 2) {
			maxValue = 0.375f;
		} 
		else {
			maxValue = 0.5f;
		}
		currentPowerAmount = 0f;
		RefreshHUD ();
	}

	public void SetPowerForce(float power)
	{
		power /= 1000;
		currentPowerAmount += power;
		if (currentPowerAmount >= maxValue) {
			currentPowerAmount = maxValue;
		}
		RefreshHUD ();
		specialPowerCount = (int)(currentPowerAmount / 0.125f);
	}

	public void DecreasePower()
	{
		specialPowerCount--;
		if (specialPowerCount <= 0)
			specialPowerCount = 0;
		currentPowerAmount -= 0.125f;
		RefreshHUD ();
	}

	void RefreshHUD()
	{
		specialPowerBar.fillAmount = currentPowerAmount;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			SetPowerForce (15);
		}
	}
}
