using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance; //Instância pública e estática dele pŕopŕio.

	[Header("Player and Enemy Properties")]
	public PlayerHealth player;
	public Transform enemyTarget;

	[SerializeField] float delayOnPlayerDeath = 1f; //Delay para restart do game.

	[Header("UI Properties")]
	[SerializeField] Text scoreText;
	[SerializeField] Text gameoverText;

	[Header("Player Selection Properties")]
	[SerializeField] GameObject enemySpawners;
	[SerializeField] Animator cameraAnimator;

	[Header("Ally Properties")]
	[SerializeField] AllyManager allyManager;

	int score = 0;


	void Awake()
	{
		if (Instance == null)
			Instance = this;
		
		else if (Instance!=null)
			Destroy (this);
	}

	public void PlayerChosen(PlayerHealth selected)
	{
		player = selected;
		enemyTarget = player.transform;

		if (scoreText != null)
			scoreText.text = "Score: 0";
	
		if (enemySpawners != null)
			enemySpawners.SetActive (true);
	
		if (cameraAnimator != null)
			cameraAnimator.SetTrigger ("Start");
	}

	public void PlayerDied()
	{
		enemyTarget = null;
		if (gameoverText != null)
			gameoverText.enabled = true;
	}

	public void PlayerDeathCompete()
	{
		Invoke ("ReloadScene", delayOnPlayerDeath);
	}

	public void AddScore(int points)
	{
		score += points;
		if (scoreText != null)
			scoreText.text = "Score: " + score;
	}

	public void SummonAlly()
	{
		if (allyManager == null)
			return;
		
		Ally ally = allyManager.SummonedAlly ();
		if (ally != null)
		{
			enemyTarget = ally.transform;
			Invoke ("UnSummonAlly", ally.duration);
		}
	}

	void UnSummonAlly()
	{
		enemyTarget = player.transform;
		allyManager.UnSummonAlly ();
	}

	void ReloadScene()
	{
		Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.buildIndex);
	}

}
