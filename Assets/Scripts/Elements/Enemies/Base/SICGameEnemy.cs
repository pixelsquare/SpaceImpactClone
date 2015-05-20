using UnityEngine;
using System.Collections;
using SpaceImpact.GameCore;

namespace SpaceImpact {

	public enum EnemyType {
		NONE = 0,
		ENEMY = 1,
		BOSS = 2
	}

	public class SICGameEnemy : SICGameUnit {
		// Public Variables	
		[SerializeField] private bool canFire;
		[SerializeField] private float fireRate;
		[SerializeField] private ProjectileType type;

		// Private Variables
		private bool isTargetted;
		private bool hasCollided;

		private float fireTime = 0f;

		// Static Variables

		public bool IsTargetted { get { return isTargetted; } }

		# region Game Element
		public override void OnEnable() {
			base.OnEnable();
			ResetElement();
		}

		public override void OnElementUpdate() {
			transform.Translate(-Vector3.right * MoveSpeed * Time.deltaTime);
			Fire();
		}

		public override void DisableElement() {
			base.DisableElement();
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_ENEMY; }
		}
		# endregion

		public void Fire() {
			fireTime += Time.deltaTime;
			if (fireTime >= fireRate) {
				FireProjectile(type, -Vector3.right, UnitType.SPACE_SHIP);
				fireTime = 0f;
			}
		}

		public void OnTriggerEnter2D(Collider2D col) {
			SICGameElement element = col.GetComponent<SICGameElement>();
			if (element != null) {
				SICSpaceShip ship = element.GetComponent<SICSpaceShip>();
				if (ship != null && !hasCollided) {
					SICGameManager.SharedInstance.GameMetrics.AddScore(ScorePoint);

					int subtractedHP = (GetEnemyType() == EnemyType.ENEMY) ? 999 : 1;
					SubtractHP(subtractedHP);
					ship.SubtractHP(1);
					SICGameManager.SharedInstance.ResetSpaceShip();

					hasCollided = true;
					ResetElement();
				}

				SICProjectiles projectile = col.GetComponent<SICProjectiles>();
				if (projectile != null) {

				}
			}
		}

		public void SetTargetted(bool tgt) {
			isTargetted = tgt;
		}

		public virtual EnemyType GetEnemyType() {
			return EnemyType.NONE;
		}

		public override UnitType GetUnitType() {
			return UnitType.ENEMY;
		}

		public override void ResetElement() {
			base.ResetElement();
			isTargetted = false;
			hasCollided = false;
			fireTime = 0f;
		}
	}
}