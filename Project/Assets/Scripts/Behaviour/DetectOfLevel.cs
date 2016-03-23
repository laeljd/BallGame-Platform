using UnityEngine;
using System.Collections;

namespace FATEC.TileGame.Behavours{
	/// Detecta se o objeto esta fora do nível.
	public class DetectOfLevel : BaseBehaviour
		{
		[Tooltip("A altura mínima do nível que o objeto pode se mover.")]
		public float minimumHeight;

		void Update (){
			if (this.transform.position.y < this.minimumHeight) {
				Application.LoadLevel("GameOver");
			}
		}
	}
}
