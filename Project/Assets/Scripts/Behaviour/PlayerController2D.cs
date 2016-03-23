using UnityEngine;
using System.Collections;

//name space nome da instituicao, nome do projeto, pasta
namespace FATEC.TileGame.Behavours
{
	/// <summary>
	/// acoes do player
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Animator))]
	public class PlayerController2D : BaseBehaviour
	{

		private new Rigidbody2D rigidbody2D;

		//configuracoes
		[Tooltip("tipo do level em que player se encontra, 1-normal, 2-aquatico")]
		public int typeLevelConfig ;
		[Tooltip("velocidade maxima de movimento do player")]
		public float maxMovimentSpeedConfig ;
		[Tooltip("Força de pulo do player")]
		public float jumpForceConfig;
		[Tooltip("Velocidade de aceleraçao do Player")]
		public float acelerationMovimentConfig;
		[Tooltip("Velocidade de desaceleraçao do Player")]
		public float decelerationMovimentConfig;
		[Tooltip("Tempo que leva para o poder do pulor chegar ao maximo")]
		public float strengthjumpTimerConfig;
		[Tooltip("Força do poder do pulo")]
		public float strengthjumpConfig;
		[Tooltip("Força da gravidade no level")]
		public float gravityConfig;
		[Tooltip("Margem da colisao com um quina para a bola pular se 1 pula mesmo na pontinha, se 0 nao pula se bater na quina")]
		public float marginEdgeColision;
		[Tooltip("Deformaçao elastica da bola, o quando ela quica")]
		public float elasticDeformation;

		private float currentSpeedH;
		private float currentSpeedV;
		private float strengthjumpTimer;
		private float strengthjump;
		private int currentConfig;
		private int currentGravity;
		private int currentPower;

		//tags
		private string groundTag = "Ground";
		//bandeiras
		private bool inAir = true;
		private bool inWater = false;
		private bool controlJump = false;
		private bool controlGravity = false;
		private bool controlElasticity = false;

		protected override void Awake () {
			base.Awake ();
			this.rigidbody2D = this.GetComponent<Rigidbody2D> ();
		}

		public void Start () {
			//configuracoes iniciais de acordo com o level
			setConfig (1);
		}


		private void OnCollisionEnter2D (Collision2D collision) {
			/// <summary>
			/// colisao com o chao
			/// </summary>
			if (collision.collider.CompareTag (this.groundTag)) {
				for (var collisionIndex = 0; collisionIndex < collision.contacts.Length; collisionIndex++) {
					//margem para o contato com a base da bola, 0 nunca ocorre , 1 mesmo tocando no continho maximo da quina funciona
					if (collision.contacts [collisionIndex].normal.y >= marginEdgeColision) {
						this.inAir = false;
						//break;
					}
					if (collision.contacts [collisionIndex].normal.x != 0) {
						//quicar quando bater na parede ,
						//inverter velocidade
						this.currentSpeedH = Mathf.Abs(this.currentSpeedH) * collision.contacts[collisionIndex].normal.x * elasticDeformation;
					}
					if(this.controlGravity || this.typeLevelConfig == 2){
						//quicar quando bater no chao ou teto,
						//inverter velocidade
						var velocity = this.rigidbody2D.velocity;
						velocity.y = Mathf.Abs(this.currentSpeedV) * collision.contacts[collisionIndex].normal.y * elasticDeformation;
						this.rigidbody2D.velocity = velocity;
					}
				}
			}
		}
		/// <summary>
		/// nao colisao com o chao
		/// </summary>
		protected void OnCollisionExit2D (Collision2D collision) {
			this.inAir = (collision.collider.CompareTag (this.groundTag));
		}

		private void Update () {
			//pegar a velecidade do objeto pois vamos alterar seu valor, depois atualiza-lo
			var velocity = this.rigidbody2D.velocity;
	
			//alterar as configuracoes caso tenha pegado um poder
			if (this.controlJump) {
				setConfig (2);
			}
			else if (this.controlGravity) {
				setConfig (3);
			}
			//so ativa a configuracao quando esta segurando o espaco, entao nao e aqui que configura o poder da elasticidade e sim na acao da tecla
			else {
				//configuracao padrao
				setConfig (1);
			}



			/// <summary>
			/// movimento horizontal
			/// </summary>
			//pegar o input direcional Horizontal 
			if (Input.GetAxis ("Horizontal") != 0) {
				//incrementar a velocidade de acordo com a direcao do eixo precionado
				this.currentSpeedH += Mathf.Sign (Input.GetAxis ("Horizontal")) * this.acelerationMovimentConfig;	
			}
			else {
				//desacelerar quando nao estiver clicando 
				if (Mathf.Abs (this.currentSpeedH) >= this.decelerationMovimentConfig) {
					this.currentSpeedH -= Mathf.Sign (this.currentSpeedH) * this.decelerationMovimentConfig;	
				}
				else {
					this.currentSpeedH = 0;
				}
			}

			//poder da gravidade
			if (this.controlGravity) {
				/// <summary>
				/// movimento vertical
				/// </summary>
				/// //apenas quando controla a gravidade
				if (Input.GetAxis ("Vertical") != 0) {
					//incrementar a velocidade de acordo com a direcao do eixo precionado
					velocity.y += Mathf.Sign (Input.GetAxis ("Vertical")) * this.acelerationMovimentConfig;	
				}
				else {
					//desacelerar quando nao estiver clicando 
					if (Mathf.Abs (velocity.y) > this.decelerationMovimentConfig) {
						velocity.y -= Mathf.Sign (velocity.y) * this.decelerationMovimentConfig;
					}
					else {
						velocity.y = 0;
					}
				}
			}

			//quando estiver no level da agua
			if (this.inWater) {
				if (Input.GetButton ("Jump")) {
					setGravity (this.gravityConfig);
				}
				else {
					setGravity (-this.gravityConfig);
				}
			}

			//poder controle da elasticidade
			if (this.controlElasticity) {
				if (Input.GetButton ("Jump")) {
					if (!this.inAir) {
						//apenas altera algumas configuracoes
						setConfig (1);
						setConfig (4);
					}
				}
				else {
					if (!this.inAir) {
						//pula
						velocity.y = this.jumpForceConfig;
						this.inAir = true;
					}
				}
			}

			//se estiver no chao
			if (!this.inAir && !this.controlGravity && !this.inWater && !this.controlElasticity) {
				//poder controle de pulo
				if (this.controlJump) {
					//se estiver pressionando o butao de pulo
					if (Input.GetButton ("Jump")) {
						this.strengthjumpTimer += Time.deltaTime;
						this.strengthjump = this.strengthjumpConfig * this.strengthjumpTimer;
						if (this.strengthjumpTimer >= this.strengthjumpTimerConfig) {
							//pula com forca
							velocity.y = this.jumpForceConfig + (this.jumpForceConfig * this.strengthjump);
							this.strengthjumpTimer = 0;
							this.strengthjump = 0;
							this.inAir = true;
						}
					}
					else {
						//se nao estiver precionando
						//pula com parte da forca
						velocity.y = this.jumpForceConfig + (this.jumpForceConfig * this.strengthjump);
						this.inAir = true;
					}
				}
				else {
					//se nao tem poder
					//pula normal
					velocity.y = this.jumpForceConfig;
					this.inAir = true;
				}
			}
			else {
				//se estiver no ar
				//zera os contadores
				this.strengthjumpTimer = 0;
				this.strengthjump = 0;
				if(this.rigidbody2D.velocity.y == 0){
					//caso bugar o player e ele nao pular sozinho apertar o botao de pulo para desbugar
					if (Input.GetButtonDown("Jump")){
						this.inAir = false;
					}
				}
			}

			//limite de velocidade horizontal
			if (Mathf.Abs (this.currentSpeedH) > this.maxMovimentSpeedConfig) {
				this.currentSpeedH = Mathf.Sign(this.currentSpeedH) * this.maxMovimentSpeedConfig;
			}
			//limite de velocidade vertical
			if (Mathf.Abs (velocity.y) > this.jumpForceConfig && !this.controlJump) {
				velocity.y = Mathf.Sign(velocity.y) * this.jumpForceConfig;
			}

			//atualizacao das velocidades
			velocity.x = this.currentSpeedH;
			this.currentSpeedV =  velocity.y;
			this.rigidbody2D.velocity = velocity;

			//botao para restart do level, caso fique preso em algum bug
			if (Input.GetKey(KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
			//botao para restart do level, caso fique preso em algum bug
			if (Input.GetKey(KeyCode.L)) {
				if (Application.loadedLevelName == "Level1"){
					Application.LoadLevel("Level2");
				}
				if (Application.loadedLevelName == "Level2"){
					Application.LoadLevel("Level3");
				}
				if (Application.loadedLevelName == "Level3"){
					Application.LoadLevel("Level4");
				}
				if (Application.loadedLevelName == "Level4"){
					Application.LoadLevel("Vitory");
				}
			}
			

		}

		//configuracoes
		public void setConfig (int set)	{
			if (this.currentConfig != set) {
				this.currentConfig = set;
			}
			else {
				return;
			}

			switch (set) {
			case 1:
				//configuracao padrao
				this.maxMovimentSpeedConfig = 5.0f;
				this.jumpForceConfig = 10.0f;
				this.acelerationMovimentConfig = 0.2f;
				this.decelerationMovimentConfig = 0.1f;
				this.strengthjumpTimerConfig = 1f;
				this.strengthjumpConfig = 0.5f;
				this.gravityConfig = 2f;
				setGravity (this.gravityConfig);
				this.inWater = false;
				this.marginEdgeColision = 1f;
				this.elasticDeformation = 1f;
				if(this.typeLevelConfig == 2){
					//level aquatico
					this.maxMovimentSpeedConfig = 2.5f;
					this.acelerationMovimentConfig = 0.1f;
					this.decelerationMovimentConfig = 0.1f;
					this.elasticDeformation = 0.5f;
					this.gravityConfig = 1.5f;
					this.inWater = true;
					setGravity (-this.gravityConfig);
				}
				break;
			case 2:
				//controle do pulo
				this.decelerationMovimentConfig = 0.01f;
				setGravity (this.gravityConfig);
				break;
			case 3:
				//controle de gravidade
				this.maxMovimentSpeedConfig = this.jumpForceConfig = 10.0f;
				this.acelerationMovimentConfig = 0.1f;
				this.decelerationMovimentConfig = 0.04f;
				setGravity (0);
				break;
			case 4:
				//controle de elasticidade
				this.maxMovimentSpeedConfig = 2.0f;
				this.acelerationMovimentConfig = 1f;
				this.decelerationMovimentConfig = 1f;
				this.elasticDeformation = 0.5f;
				setGravity (this.gravityConfig);
				break;
			}
		}
		//configuracao da gravidade
		public void setGravity (float set)	{
			this.rigidbody2D.gravityScale = set;
		}

		//poderes
		public void setPower (int set)	{
			if (this.currentPower != set) {
				this.currentPower = set;
			} else {
				return;
			}
			
			switch (set) {
			case 1:
				//controle do pulo
				this.controlJump = true;
				this.controlElasticity = this.controlGravity = !this.controlJump;
				setConfig (1);
				setConfig (2);
				break;
			case 2:
				//controle de gravidade
				this.controlGravity = true;
				this.controlElasticity = this.controlJump = !this.controlGravity;
				setConfig (1);
				setConfig (3);
				break;
			case 3:
				//controle de elasticidade
				this.controlElasticity = true;
				this.controlGravity = this.controlJump = !this.controlElasticity;
				break;
			default:
				this.controlJump = this.controlGravity = this.controlElasticity = false;
				setConfig (this.typeLevelConfig);
				break;
			}
		}
	}
}