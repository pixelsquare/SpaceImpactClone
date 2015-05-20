using UnityEngine;
using System.Collections;
using SpaceImpact.GameCore;

namespace SpaceImpact {

	public enum ProjectileType {
		NONE = 0,
		MISSILE = 1,
		LASER = 2,
		ROCKET = 3,
		BEAM = 4
	}

	public class SICProjectiles : SICGameElement {
		// Public Variables	
		[SerializeField] private SpriteRenderer mainTexture;
		[SerializeField] private int durability = 1;
		[SerializeField] private int damage = 1;

		// Private Variables
		private SpriteRenderer originalTexture;
		private int originalDurability;
		private int originalDamage;

		private Vector3 direction;
		private UnitType targetType;

		protected Transform owner;
		protected Transform sender;

		// Static Variables

		public SpriteRenderer MainTexture { get { return mainTexture; } }

		public int Durability { get { return durability; } }

		public int Damage { get { return damage; } }

		public Vector3 Direction { get { return direction; } }

		public UnitType TargetType { get { return targetType; } }

		public Transform Owner { get { return owner; } }

		public Transform Sender { get { return sender; } } 

		# region Game Element
		public override void Awake() {
			base.Awake();
			originalTexture = mainTexture;
			originalDurability = durability;
			originalDamage = damage;
			direction = Vector3.right;
		}

		public override void OnEnable() {
			base.OnEnable();
			ResetElement();
		}

		public override void ResetElement() {
			base.ResetElement();
			mainTexture = originalTexture;
			durability = originalDurability;
			damage = originalDamage;
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_PROJECTILE; }
		}
		# endregion

		public virtual void Initialize(Transform owner, Transform sender) {
			this.owner = owner;
			this.sender = sender;
		}

		public virtual ProjectileType GetProjectileType() {
			return ProjectileType.MISSILE;
		}

		public virtual void OnTriggerEnter2D(Collider2D col) {
			if (col2D == null)
				return;

			// Both Enemy and Boss
			SICGameUnit unit = col.GetComponent<SICGameUnit>();
			if (unit != null) {
				if (unit.GetUnitType() == targetType) {
					if (targetType == UnitType.ENEMY) {
						SICGameEnemy enemy = col.GetComponent<SICGameEnemy>();
						if (enemy != null) {
							SubtractDurability(1);
							SICGameManager.SharedInstance.GameMetrics.AddScore(enemy.ScorePoint);
							enemy.SubtractHP(damage);
						}
					}

					if (targetType == UnitType.SPACE_SHIP) {
						unit.SubtractHP(1);
						SICGameManager.SharedInstance.ResetSpaceShip();
					}
				}
			}
		}

		public void AddDamage(int dmg) {
			damage += dmg;
		}

		public void SubtractDamage(int dmg) {
			damage -= dmg;
		}

		public void SetDamage(int dmg) {
			damage = dmg;
		}

		public void AddDurability(int dur) {
			durability += dur;
		}

		public void SubtractDurability(int dur) {
			durability -= dur;

			if (durability <= 0)
				DisableElement();
		}

		public void SetDurability(int dur) {
			durability = dur;
		}

		public void SetDirection(Vector3 dir) {
			direction = dir;
		}

		public void SetTargetType(UnitType type) {
			targetType = type;
		}
	}
}