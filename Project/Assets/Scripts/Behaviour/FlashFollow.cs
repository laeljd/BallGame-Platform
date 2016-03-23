using UnityEngine;
using System.Collections;

namespace FATEC.TileGame.Behavours{
	//segue o player 
	public class FlashFollow : MonoBehaviour{
		public Transform t;
	
		void Update () {
			var pos = t.localPosition;
			//posicao do flash em relacao a bola
			pos.x += -0.12f;
			pos.y += 0.15f;
			this.transform.position = pos;
		}
	}
}