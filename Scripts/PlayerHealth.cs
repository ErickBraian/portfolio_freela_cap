using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	[SerializeField] public Image healthBar;

	[SerializeField] public float minValue;
	[SerializeField] public float maxValue;

	[HideInInspector] public float maxHealth = 100f;
	[HideInInspector] public float currentHealth;

	float calculateHealth;
	float outputHealth;

	[SerializeField] public GameObject playerDeathFX;

	EnemyDamage enemyDamage;

	[SerializeField] public Color32 startColor;
	[SerializeField] public Color32 endColor;

	AudioSource playerAS;

	void Awake ()
	{
		currentHealth = maxHealth;
		healthBar.fillAmount = currentHealth;
		SetHealth ();
		playerAS = GetComponent<AudioSource> ();
	}

	public void addDamage (float damage)
	{
		currentHealth -= damage;
		healthBar.fillAmount = currentHealth / maxHealth;
		print (healthBar.fillAmount);
		print (currentHealth);
		if (currentHealth <= 0) {
			currentHealth = 0;
			makeDead ();
		}
		else
		{
			SetHealth ();
		}

		playerAS.Play ();
	}
		
	public void makeDead ()
	{
		//Instancia o efeito de morte
		Instantiate(playerDeathFX, transform.position,Quaternion.Euler(new Vector3(-90,0,0)));
		Destroy (gameObject);
	}

	public void SetHealth()
	{
		calculateHealth = currentHealth / maxHealth;
		outputHealth = calculateHealth * (maxValue - minValue) + minValue;

		healthBar.fillAmount = Mathf.Clamp (outputHealth, minValue, maxValue);

		healthBar.color = Color.Lerp (endColor, startColor, calculateHealth);
	}

	public void addHealth(float health)
	{
		currentHealth += health;
		healthBar.fillAmount = currentHealth / maxValue;
		if (healthBar.fillAmount >= maxValue)
			healthBar.fillAmount = maxValue;

		if (currentHealth >= maxHealth)
			currentHealth = maxHealth;
	 
		else
			SetHealth ();
	}
}
