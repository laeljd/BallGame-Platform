using UnityEngine;
using System.Collections;

namespace FATEC.TileGame.Behavours{

	public class MainMenu : MonoBehaviour {

		public Sprite 		mainBackground, intructions1, intructions2;
		public GUIStyle		styleButtons;
		public Font			font;
		public int 			sizeFont = 16;
		public int 			sizeButton = 10;
		public float		Padding;
		private int 		currentState;//0-menu principal, 1-instructions,  2-creditos
		public SpriteRenderer spriteRenderer;
		public Button[] 	buttons;

		void Start(){
			currentState = 0;
			Cursor.visible = true;
			this.spriteRenderer = this.GetComponent<SpriteRenderer>();
		}

		void OnGUI(){
			GUI.skin.font = this.font;
			styleButtons.fontSize = (Screen.height / sizeFont);
			GUI.skin.button = styleButtons;
			//o tamanho e' de acordo com a altura da tela
			var sizeH = (Screen.height / sizeFont) * (sizeFont/2);
			var sizeV = (Screen.height / sizeButton);
			//margem para posicionar os botoes em sequencia
			var MarginH = (sizeV + sizeV/sizeButton);
			//margem para posicionar abaixo do nome do jogo 
			var PaddingH = (Screen.height * Padding);
			//posisao dos botoes

			var btn1 = new Rect( new Vector2( ((Screen.width / 2) - (sizeH / 2)) , ((Screen.height / 2) + MarginH + PaddingH ) ), new Vector2( sizeH, sizeV ) );
			var btn2 = new Rect( new Vector2( ((Screen.width / 2) - (sizeH / 2)) , ((Screen.height / 2) + MarginH * 2 + PaddingH ) ), new Vector2( sizeH, sizeV ) );

			if (currentState == 0){
				this.spriteRenderer.sprite = mainBackground;
				if(GUI.Button(btn1, "Jogar")){
				   Application.LoadLevel("Level1");
				}
				if(GUI.Button(btn2, "Instruçoes")){
					currentState = 1;
				}
			}
			if(currentState == 1){
				this.spriteRenderer.sprite = intructions1;
				if(GUI.Button(btn1, "Poderes")){
					currentState = 2;
				}
				if(GUI.Button(btn2, "Voltar")){
					currentState = 0;
				}
			}
			if(currentState == 2){
				this.spriteRenderer.sprite = intructions2;
				if(GUI.Button(btn1, "Basico")){
					currentState = 1;
				}
				if(GUI.Button(btn2, "Voltar")){
					currentState = 0;
				}
			}
		}
	}
}

