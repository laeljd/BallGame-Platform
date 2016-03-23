using UnityEngine;
using System.Collections;

namespace FATEC.TileGame.Behavours{
	[RequireComponent(typeof(Rigidbody2D))]
	public class VerticalMoviment2D : BaseBehaviour {
		//representa as direcoes da direcao
		public enum MovimentDirection{
			Down,
			Up
		}
		
		public MovimentDirection direction = MovimentDirection.Down;
		public float duration = 5.0f;
		
		private new Rigidbody2D rigidbody;
		[Tooltip("unidades por segundos")]
		public float movimentVelocity = 1.0f;
		
		protected override void Awake(){
			base.Awake ();
			this.rigidbody = this.GetComponent <Rigidbody2D> ();
		}
		
		protected void Start(){
			this.StartCoroutine(this.ChangeDirection());
		}
		
		//retorna uma corotina ienumerator
		protected IEnumerator ChangeDirection(){
			//so pode usar while true em uma corotine ou uma tread separada
			while (true) {
				var velocity = 	this.rigidbody.velocity;
				
				velocity.y = (this.direction == MovimentDirection.Down ? -this.movimentVelocity : this.movimentVelocity);

				this.rigidbody.velocity = velocity;
				
				yield return new WaitForSeconds(this.duration);
				
				this.direction = (this.direction == MovimentDirection.Down ? MovimentDirection.Up : MovimentDirection.Down);
			}
		}
	}
}
