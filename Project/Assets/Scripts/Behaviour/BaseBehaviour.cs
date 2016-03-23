using UnityEngine;
using System.Collections;


namespace FATEC.TileGame.Behavours{
public abstract class BaseBehaviour : MonoBehaviour{

		// o new serve para sobreescrever as coisas do pai, presisa ja que o nome da variavei ja existe no monobehavor
		public new Transform transform;

		//o virtual e para dizer que isso pode ser sebreescrito pois os filhos irao usar um diferente
		protected virtual void Awake() {
			this.transform = this.GetComponent<Transform> ();
		}
}
}