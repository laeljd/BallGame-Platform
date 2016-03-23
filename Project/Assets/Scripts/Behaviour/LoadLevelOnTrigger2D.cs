using UnityEngine;
using System.Collections;


namespace FATEC.TileGame.Behavours{
	/// <summary>
	/// Carrega um nível quando ocorre um gatilho.
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class LoadLevelOnTrigger2D : BaseBehaviour {

		[Tooltip("Nome do nível para carga.")]
		public string levelName;
		[Tooltip("Nome da tag para permitir que o nível cargue.")]
		public string tagName;

		void Start () {
			this.GetComponent<Collider2D>().isTrigger = true;
		}
		
		protected void OnTriggerEnter2D(Collider2D collider) {
			if (collider.CompareTag(this.tagName)) {
				Application.LoadLevel(this.levelName);
			}
		}
	}
}