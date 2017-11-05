using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	public GameObject login;
	public GameObject game;

	public enum State  {
		Login,
		Play,
		Pause,
	};

	private State state;
	
	IEnumerator LoginState() {
		Debug.Log("Login: Enter");
		login.SetActive(true);
		while(state == State.Login) {
			yield return 0;
		}
		login.SetActive(false);
		Debug.Log("Login: Exited");
		NextState();
	}


	IEnumerator PlayState() {
		Debug.Log("Play: Enter");
		game.SetActive(true);
		while(state == State.Play) {
			yield return 0;
		}
		Debug.Log("Play: Exited");
		NextState();
	}

	IEnumerator PauseState() {
		Debug.Log("Pause: Enter");
		Time.timeScale = 0;
		AudioController.StopAllBuildingAudio();
		while(state == State.Pause) {
			yield return 0;
		}
		Time.timeScale = 1;
		AudioController.PlayAllBuildingAudio();
		Debug.Log("Pause: Exited");
		NextState();
	}

	
	void NextState() {
		string methodName = state.ToString() + "State";
		System.Reflection.MethodInfo info = 
			GetType().GetMethod(methodName,
								System.Reflection.BindingFlags.NonPublic |
								System.Reflection.BindingFlags.Instance);
			StartCoroutine((IEnumerator)info.Invoke(this,null));
	}

	public void changeState(State state){
		this.state = state;
	}

	public State getState(){
		return this.state;
	}

	void Start () {
		NextState();
	}
}
