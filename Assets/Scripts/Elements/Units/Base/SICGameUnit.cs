using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SpaceImpact {

	public enum UnitType {
		NONE = 0,
		SPACE_SHIP = 1,
		ENEMY = 2
	}

	public class SICGameUnit : SICGameElement {
		// Public Variables	
		[SerializeField] private SpriteRenderer mainTexture;
		[SerializeField] private int healthPoints = 1;
		[SerializeField] private int scorePoint = 0;
		[SerializeField] private bool invulnerable;
		[SerializeField] private Transform shipNozzle;
		[SerializeField] private List<SICProjectiles> refProjectiles;

		// Private Variables	
		private SpriteRenderer originalTexture;
		private int originalHealthPoints;
		private int originalScorePoint;
		private bool originalInvulnerable;

		// Static Variables

		public SpriteRenderer MainTexture { get { return mainTexture; } }

		public bool IsInvulnerable { get { return invulnerable; } }

		# region Game Element
		public override void Awake() {
			base.Awake();
			originalTexture = mainTexture;
			originalHealthPoints = healthPoints;
			originalScorePoint = scorePoint;
			originalInvulnerable = invulnerable;
		}

		public override void OnEnable() {
			base.OnEnable();
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_UNIT; }
		}
		# endregion

		public void FireProjectile(ProjectileType type, Vector3 direction, UnitType targetType) {
			if (shipNozzle == null || type == ProjectileType.NONE)
				return;

			GameObject projectileObj = SICObjectPoolManager.SharedInstance.GetObject(GetRefProjectile(type).OBJECT_ID, type);
			if (projectileObj != null) {
				SICProjectiles projectile = projectileObj.GetComponent<SICProjectiles>();
				projectile.EnableElement();
				projectile.SetTargetType(targetType);
				projectile.SetDirection(direction);
				projectile.Initialize(transform, shipNozzle);
			}
		}

		public SICProjectiles GetRefProjectile(ProjectileType type) {
			return refProjectiles.Find(a => a.GetProjectileType() == type);
		}

		public virtual UnitType GetUnitType() {
			return UnitType.NONE;
		}

		public int HealthPoints { get { return healthPoints; } }

		public int ScorePoint { get { return scorePoint; } }

		public override void DisableElement() {
			base.DisableElement();
			ShowExplosionFX();
		}

		public void AddHP(int hp) {
			this.healthPoints += hp;
			SetHP(this.healthPoints);
		}

		public void SubtractHP(int hp) {
			if (invulnerable)
				return;

			this.healthPoints -= hp;
			SetHP(this.healthPoints);

			if (healthPoints <= 0)
				DisableElement();
		}

		public virtual void SetHP(int hp) {
			healthPoints = hp;
		}

		public void AddScorePoint(int score) {
			this.scorePoint += score;
			SetScorePoint(this.scorePoint);
		}

		public void SubtractScorePoint(int score) {
			this.scorePoint -= score;
			SetScorePoint(this.scorePoint);
		}

		public virtual void SetScorePoint(int score) {
			scorePoint = score;
		}

		public void SetInvulnerability(bool invulnerable) {
			this.invulnerable = invulnerable;
		}

		public override void  ResetElement() {
 			base.ResetElement();
			mainTexture = originalTexture;
			healthPoints = originalHealthPoints;
			scorePoint = originalScorePoint;
			invulnerable = originalInvulnerable;
		}
	}
}