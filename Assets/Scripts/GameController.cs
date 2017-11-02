using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public enum State  {
		Login,
		Game,
		Pause,
	};

	private State state;
	

	IEnumerator GameState() {
		Debug.Log("Game: Enter");
		while(state == State.Game) {
			yield return 0;
		}
		Debug.Log("Game: Exited");
		NextState();
	}

	IEnumerator LoginState() {
		Debug.Log("Login: Enter");
		while(state == State.Login) {
			yield return 0;
		}
		Debug.Log("Login: Exited");
		NextState();
	}

	IEnumerator PauseState() {
		Debug.Log("Pause: Enter");
		while(state == State.Pause) {
			yield return 0;
		}
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
