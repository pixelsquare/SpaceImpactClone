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

		protected Transform owner;

		// Static Variables

		public SpriteRenderer MainTexture { get { return mainTexture; } }

		public int Durability { get { return durability; } }

		public int Damage { get { return damage; } }

		public Transform Owner { get { return owner; } }

		# region Game Element
		public override void Awake() {
			base.Awake();
			originalTexture = mainTexture;
			originalDurability = durability;
			originalDamage = damage;
		}

		public override void OnEnable() {
			base.OnEnable();
			mainTexture = originalTexture;
			durability = originalDurability;
			damage = originalDamage;
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_PROJECTILE; }
		}
		# endregion

		public void Update() {
			ProjectileMovement();

			if (ProjectileConstraint()) {
				DisableElement();
			}
		}

		public virtual void Initialize(Transform sender) {
			this.owner = sender;
		}

		public virtual void ProjectileMovement() { }

		public virtual bool ProjectileConstraint() {
			return false;
		}

		public virtual ProjectileType GetProjectileType() {
			return ProjectileType.MISSILE;
		}

		public void OnTriggerEnter2D(Collider2D col) {
			if (col2D == null)
				return;

			if (col.gameObject.layer == SICLayerManager.EnemyLayer) {
				SICEnemy enemyElement = col.GetComponent<SICEnemy>();

				if (enemyElement == null)
					return;

				enemyElement.SubtractHP(damage);
				SubtractDurability(1);
				SICGameManager.SharedInstance.GameMetrics.AddScore(enemyElement.ScorePoint);
			}

			if (col.gameObject.layer == SICLayerManager.ProjectileLayer) {
				SICProjectiles projectileElement = col.GetComponent<SICProjectiles>();

				if (projectileElement == null)
					return;

				if (projectileElement.Owner == owner)
					return;

				projectileElement.ShowExplosionFX();
				//projectileElement.DisableElement();
				//DisableElement();
				//durability--;
				projectileElement.SubtractDurability(1);
				SubtractDurability(1);
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
	}
}