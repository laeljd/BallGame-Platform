using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Texture 		gameOver;
	public GUIStyle		styleButtons;
	public Font			font;
	public int 			sizeFont = 16;

	void Start(){
		Cursor.visible = true;
	}

	void OnGUI(){
		GUI.skin.font = this.font;
		styleButtons.fontSize = (Screen.height / sizeFont);
		GUI.skin.button = styleButtons;
		//o tamanho e' de acordo com a altura da tela
		var sizeH = (Screen.height / sizeFont) * (sizeFont/1.5f);
		var sizeV = (Screen.height / 10);
		//margem para posicionar os botoes em sequencia
		var MarginH = (sizeV + sizeV/10);
		//margem para posicionar abaixo do nome do jogo 
		var PaddingH = (sizeV);
		//posisao dos botoes
		var btn = new Rect( new Vector2( ((Screen.width / 2) - (sizeH / 2)) , ((Screen.height / 2) + MarginH*2 + PaddingH ) ), new Vector2( sizeH, sizeV ) );

		//GUI.skin.button = styleButtons;
		//GUI.DrawTexture (new Rect( new Vector2( ((Screen.width / 2) - (Screen.width / 2)) , ((Screen.height / 2) - (Screen.height / 2)) ), new Vector2( Screen.width, Screen.height ) ), gameOver);
		if(GUI.Button(btn, "Voltar para Menu")){
			Application.LoadLevel("Main");
		}
	}
}
