using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;
using System.Collections.Generic;
using SpaceImpact.GameCore;

namespace SpaceImpact {

	public class SICSpaceShip : SICGameUnit {
		// Public Variables	
		[SerializeField] private Transform shipNozzle;
		[SerializeField] private List<SICProjectiles> refProjectiles;

		// Private Variables
		private ProjectileType defaultProjectile;
		private ProjectileType specialProjectile;

		private int specialCount;

		private float horiz;
		private float vert;

		// Static Variables

		# region Game Element
		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_SPACE_SHIP; }
		}
		# endregion

		# region Game Unit

		public override void OnEnable() {
			base.OnEnable();
			defaultProjectile = ProjectileType.MISSILE;
		}

		public override void SetHP(int hp) {
			base.SetHP(hp);
			SICGameManager.SharedInstance.GameMetrics.SetLife(hp);
		}

		public override void SetScorePoint(int score) {
			base.SetScorePoint(score);
			SICGameManager.SharedInstance.GameMetrics.SetScore(score);
		}

		# endregion

		public void Update() {
			ShipMovement();

			if (Input.GetButtonDown("Fire")) {
				FireProjectile(defaultProjectile);
			}

			if (Input.GetButtonDown("Fire1")) {
				if (specialCount <= 0)
					return;

				FireProjectile(specialProjectile);
				SubtractSpecialCount(1);
			}

			ProjectileTest();
		}

		private void ShipMovement() {
			Vector3 targetPos = new Vector3(SICAreaBounds.ThisT.position.x, SICAreaBounds.ThisT.position.y, 0f);
			if (Input.GetButton("Horizontal")) {
				horiz += Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
			}

			if (Input.GetButton("Vertical")) {
				vert += Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
			}

			transform.position = targetPos + new Vector3(horiz, vert, 0f);

			ShipConstraint();
		}

		private void ShipConstraint() {
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, SICAreaBounds.MinPosition.x + (MainTexture.bounds.size.x / 2), SICAreaBounds.MaxPosition.x - (MainTexture.bounds.size.x / 2));
			pos.y = Mathf.Clamp(pos.y, SICAreaBounds.MinPosition.y + (MainTexture.bounds.size.y / 2), SICAreaBounds.MaxPosition.y - (MainTexture.bounds.size.y / 2));
			transform.position = pos;
		}

		private void FireProjectile(ProjectileType type) {
			GameObject projectileObj = SICObjectPoolManager.SharedInstance.GetObject(GetRefProjectile(type).OBJECT_ID);
			if (projectileObj != null) {
				SICProjectiles projectile = projectileObj.GetComponent<SICProjectiles>();
				projectile.Initialize(shipNozzle);
				projectile.EnableElement();
			}
		}

		public void SetSpecial(ProjectileType special) {
			if (specialProjectile == special) {
				AddSpecialCount(GetSpecialDefaultCount(special));
				UpdateSpecial(special);
				return;
			}

			SetSpecialCount(GetSpecialDefaultCount(special));
			UpdateSpecial(special);
		}

		public void UpdateSpecial(ProjectileType type) {
			specialProjectile = type;
			SICGameManager.SharedInstance.GameMetrics.SetSpecial(this.specialProjectile);
		}

		public SICProjectiles GetRefProjectile(ProjectileType type) {
			return refProjectiles.Find(a => a.GetProjectileType() == type);
		}

		public int GetSpecialDefaultCount(ProjectileType type) {
			int result = 0;

			if (type == ProjectileType.LASER)
				result = SICGameSettings.DEFAULT_LASER_COUNT;
			if (type == ProjectileType.ROCKET)
				result = SICGameSettings.DEFAULT_ROCKET_COUNT;
			if (type == ProjectileType.BEAM)
				result = SICGameSettings.DEFAULT_BEAM_COUNT;

			return result;
		}

		public void AddSpecialCount(int count) {
			this.specialCount += count;
			SetSpecialCount(this.specialCount);
		}

		public void SubtractSpecialCount(int count) {
			this.specialCount -= count;
			SetSpecialCount(this.specialCount);
		}

		public void SetSpecialCount(int count) {
			this.specialCount = count;
			SICGameManager.SharedInstance.GameMetrics.SetSpecialCount(this.specialCount);
		}

		public void SetPosition(Vector3 pos) {
			horiz = pos.x;
			vert = pos.y;

			transform.position = pos;
		}

		private void ProjectileTest() {
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				SetSpecial(ProjectileType.LASER);
			}

			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				SetSpecial(ProjectileType.BEAM);
			}

			if (Input.GetKeyDown(KeyCode.Alpha3)) {
				SetSpecial(ProjectileType.ROCKET);
			}
		}
	}
}