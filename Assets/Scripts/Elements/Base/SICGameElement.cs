using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public abstract class SICGameElement : MonoBehaviour {
		// Public Variables	
		[SerializeField] protected SpriteRenderer mainTexture;
		[SerializeField] private float moveSpeed = 5f;
		[SerializeField] private int healthPoints = 1;

		// Private Variables
		protected Rigidbody2D rBody2D;

		public abstract string OBJECT_ID { get; }

		public float MoveSpeed { get { return moveSpeed; } }

		public int HealthPoints { get { return healthPoints; } }

		public SpriteRenderer SpRenderer { get { return mainTexture; } }

		public virtual void Awake() {
			rBody2D = GetComponent<Rigidbody2D>();
		}

		public virtual void Start() {
			InitializeBody();
		}

		public void InitializeBody() {
			if (rBody2D == null)
				return;

			rBody2D.fixedAngle = true;
			rBody2D.isKinematic = true;
		}

		public void AddSpeed(float spd) {
			moveSpeed += spd;
		}

		public void SubtractSpeed(float spd) {
			moveSpeed -= spd;
		}

		public void SetSpeed(float spd) {
			moveSpeed = spd;
		}

		public void AddHP(int hp) {
			healthPoints += hp;
		}

		public void SubtractHP(int hp) {
			healthPoints -= hp;
		}

		public void SetHP(int hp) {
			healthPoints = hp;
		}

		public void EnableElement() {
			gameObject.SetActive(true);
		}

		public virtual void DisableElement() {
			gameObject.SetActive(false);
		}

		public void SpawnExplotion() {
			GameObject explodeObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.PARTICLE_EXPLODE);
			if (explodeObj != null) {
				explodeObj.transform.position = transform.position;
				SICGameElement explode = explodeObj.GetComponent<SICGameElement>();
				explode.EnableElement();
			}
		}
	}
}