using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {

	public string APIUrl;
	public string loginPath;
	public string statusPath;
	public int successStatus;
	public InputField username;
	public InputField password;


	private GameController gameController;
	private bool loggedIn = false;
	private string userToken;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	public void ButtonLogin(){
		StartCoroutine(Login());
	}

	private IEnumerator Login(){
		Login login = new Login();
		login.username = username.text;
		login.password = Crypto.SHA256Hash(password.text);
		string loginJson = JsonUtility.ToJson(login);

		yield return StartCoroutine(LoginPost(loginJson));
		if(loggedIn){
			yield return StartCoroutine(UserStatus());
			gameController.PlayGame();
		}
	}

	private IEnumerator LoginPost(string bodyJsonString){
		string fullURL = APIUrl+loginPath;
		UnityWebRequest request = RequestMaker("POST", fullURL);
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		yield return request.SendWebRequest();
		ResponseHandler(request);
	}

	private IEnumerator UserStatus(){
		string fullURL = APIUrl+statusPath;
		UnityWebRequest request = RequestMaker("GET", fullURL);
		request.SetRequestHeader("X-Authorization", userToken);
		yield return request.SendWebRequest();
		ResponseHandler(request);
	}

	private UnityWebRequest RequestMaker( string type, string url){
		UnityWebRequest request = new UnityWebRequest(url, type);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		return request;
	}

	private void ResponseHandler(UnityWebRequest response){
		if(response.isNetworkError){
			NetworkErrorHandler(response);
		}
		else if(response.responseCode == successStatus){
			SuccessHandler(response.downloadHandler.text);
		}
		else{
			ErrorHandler(response.downloadHandler.text);
		}
	}

	private void SuccessHandler(string response){
		APIResponse apiResponse = JsonUtility.FromJson<APIResponse>(response);
		if(!loggedIn)
			LoginHandler(apiResponse);
		else
			StatusHandler(apiResponse);
	}
	private void ErrorHandler(string response){
		APIResponse apiResponse = JsonUtility.FromJson<APIResponse>(response);
		Debug.LogError("Ocorreram erros no request: "+ JsonUtility.ToJson(apiResponse));
	}
	private void NetworkErrorHandler(UnityWebRequest response){
		APIResponse error = JsonUtility.FromJson<APIResponse>(response.error);
		Debug.LogError("Ocorreram erros com a rede ao realizer o request: " + error.message);
	}
	private void LoginHandler(APIResponse response){
		userToken = response.token;
		loggedIn = true;
		Debug.Log("Usuario logado com sucesso: " + JsonUtility.ToJson(response));
	}

	private void StatusHandler(APIResponse response){
		User loggedUser = new User();
		float.TryParse(response.money, out loggedUser.money);
		loggedUser.nickname = response.nickname;
		gameController.SetUser(loggedUser);
		Debug.Log("Status do usuario adquirido com sucesso: " + JsonUtility.ToJson(response));
	}
}
