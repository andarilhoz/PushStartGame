using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {

	[Header("Propriedades da API")]
	public string APIUrl;
	public string loginPath;
	public string statusPath;

	[Tooltip("Quantidade de tentativas de conexao")]
	public int maxAttemptsToConnect = 2;

	[Header("Campos de Input")]
	public InputField username;
	public InputField password;

	
	
	private LoginErrorFeedbackController errorFeedback;
	private GameController gameController;
	private bool loggedIn = false;
	private string userToken;
	private int attemptToConect = 0;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		errorFeedback = GameObject.Find("ErrorFeedback").GetComponent<LoginErrorFeedbackController>();
	}

	public void ButtonLogin(){
		attemptToConect = 0;
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
			NetworkErrorHandler();
		}
		else if(response.responseCode == 200 || response.responseCode == 201){
			SuccessHandler(response.downloadHandler.text);
		}
		else{
			ErrorHandler(response.downloadHandler.text);
		}
	}

	private void SuccessHandler(string response){
		if(!loggedIn)
			LoginHandler(response);
		else
			StatusHandler(response);
	}
	private void ErrorHandler(string response){
		APIError apiError = JsonUtility.FromJson<APIError>(response);
		errorFeedback.SetText("Ocorreram erros no request: "+ apiError.message);
	}
	private void NetworkErrorHandler(){
		attemptToConect++;
		Debug.Log("Tentativa de conexão "+ attemptToConect +" falhou, tentando até a atentativa " + maxAttemptsToConnect );
		if(attemptToConect >= maxAttemptsToConnect)
			errorFeedback.SetText("Ocorreram erros com a rede ao realizer o request");
		else
			StartCoroutine(Login());
	}
	private void LoginHandler(string response){
		APILogin apiResponseLogin = JsonUtility.FromJson<APILogin>(response);
		userToken = apiResponseLogin.token;
		loggedIn = true;
		Debug.Log("Usuario logado com sucesso: " + JsonUtility.ToJson(apiResponseLogin));
	}

	private void StatusHandler(string response){
		APIStatus apiResponseStatus = JsonUtility.FromJson<APIStatus>(response);
		User loggedUser = new User();
		loggedUser.money = apiResponseStatus.money;
		loggedUser.nickname = apiResponseStatus.nickname;
		gameController.SetUser(loggedUser);
		Debug.Log("Status do usuario adquirido com sucesso: " + JsonUtility.ToJson(apiResponseStatus));
	}
}
