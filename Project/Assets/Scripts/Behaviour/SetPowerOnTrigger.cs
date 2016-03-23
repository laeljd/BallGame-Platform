using UnityEngine;
using System.Collections;


namespace FATEC.TileGame.Behavours{
	/// <summary>
	/// Atribui um poder ao player
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class SetPowerOnTrigger : BaseBehaviour {
		
		[Tooltip("Tipo do poder que ira ganhar, 1-controle de pulo, 2-controle de gravidade, 3-controle de elasticidade")]
		public int typePower;

		void Start () {
			this.GetComponent<Collider2D>().isTrigger = true;
		}
		
		protected void OnTriggerEnter2D(Collider2D other) {
			if (other.CompareTag("Player")) {
				other.gameObject.GetComponent<PlayerController2D>().setPower(typePower);
				Destroy(gameObject);
			}
		}
	}
}