using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MensagemControle : MonoBehaviour {

	// Use this for initialization
	public GUIText gameOverTxt;
	public GUIText restartTxt;
	public GUIText venceuTxt;

	public bool gameOver;
	public bool restart;
	public bool venceu;

	void Start () {
		gameOver = false;
		restart = false;
		venceu = false;
		gameOverTxt.text = "Game Over!";
		restartTxt.text = "Pressione 'R' para recomeçar.";
		venceuTxt.text = "Parabéns. Você venceu!";
	}
	
	void Update () {
		if (gameOver) {
			restartTxt.text = "Pressione 'R' para recomeçar.";
			restart = true;
		}
		if (restart) {
			if(Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene ("Cena1");
				//Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	public void GameOver()
	{
		gameOverTxt.text = "Game Over!";
		gameOver = true;
	}

	public void Venceu()
	{
		venceuTxt.text = "Parabéns. Você venceu!";
		restartTxt.text = "Pressione 'R' para recomeçar.";
		restart = true;
	}
}
