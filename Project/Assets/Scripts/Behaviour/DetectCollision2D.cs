using UnityEngine;
using System.Collections;

namespace FATEC.TileGame.Behavours{
	//[RequireComponent(typeof(Collider2D))]
	public class DetectCollision2D : BaseBehaviour {

		protected void OnCollisionEnter2D(Collision2D collision){
			/// <summary>
			/// colisao com o espinho
			/// </summary>
			if (collision.collider.CompareTag("Sparks")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
}