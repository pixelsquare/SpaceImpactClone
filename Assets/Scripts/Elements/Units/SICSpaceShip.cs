using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;
using System.Collections.Generic;
using SpaceImpact.GameCore;

namespace SpaceImpact {

	public class SICSpaceShip : SICGameUnit {
		// Public Variables	

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

		public override UnitType GetUnitType() {
			return UnitType.SPACE_SHIP;
		}

		# endregion

		public override void OnElementUpdate() {
			ShipMovement();
			ShipFiring();
			ClampShipToArea();

# if UNITY_EDITOR
			ProjectileTest();
# endif
		}

		private void ShipFiring() {
			if (Input.GetButtonDown("Fire")) {
				FireProjectile(defaultProjectile, Vector3.right, UnitType.ENEMY);
			}

			if (Input.GetButtonDown("Fire Special")) {
				if (specialCount <= 0)
					return;

				FireProjectile(specialProjectile, Vector3.right, UnitType.ENEMY);
				SubtractSpecialCount(1);
			}
		}

		private void ShipMovement() {
			Vector3 targetPos = new Vector3(SICAreaBounds.ThisT.position.x, SICAreaBounds.ThisT.position.y, 0f);
			horiz += Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime;
			vert += Input.GetAxisRaw("Vertical") * MoveSpeed * Time.deltaTime;

			// Clamp keys to screen bounds
			horiz = Mathf.Clamp(horiz, SICAreaBounds.PtLowerLeft.x + (MainTexture.bounds.size.x / 2), SICAreaBounds.PtLowerRight.x - (MainTexture.bounds.size.x / 2));
			vert = Mathf.Clamp(vert, SICAreaBounds.PtLowerLeft.y + (MainTexture.bounds.size.y / 2), SICAreaBounds.PtUpperLeft.y - (MainTexture.bounds.size.y / 2));

			transform.position = targetPos + new Vector3(horiz, vert, 0f);
		}

		private void ClampShipToArea() {
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, SICAreaBounds.MinPosition.x + (MainTexture.bounds.size.x / 2), SICAreaBounds.MaxPosition.x - (MainTexture.bounds.size.x / 2));
			pos.y = Mathf.Clamp(pos.y, SICAreaBounds.MinPosition.y + (MainTexture.bounds.size.y / 2), SICAreaBounds.MaxPosition.y - (MainTexture.bounds.size.y / 2));
			transform.position = pos;
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

			Vector3 targetPos = new Vector3(SICAreaBounds.ThisT.position.x, SICAreaBounds.ThisT.position.y, 0f);
			transform.position = targetPos + new Vector3(horiz, vert, 0f);
		}

		public void EnableInvulnerability(float duration = 1) {
			StartCoroutine(Invulnerability(duration));
		}

		private IEnumerator Invulnerability(float time) {
			SetInvulnerability(true);
			yield return new WaitForSeconds(time);
			SetInvulnerability(false);

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