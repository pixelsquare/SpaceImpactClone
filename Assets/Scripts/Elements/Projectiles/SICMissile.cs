using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public class SICMissile : SICProjectiles {

		# region Game Element
		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_MISSILE; }
		}
		# endregion

		# region Projectiles
		public override void Initialize(Transform sender) {
			base.Initialize(sender);
			transform.position = sender.position;
		}

		public override void ProjectileMovement() {
			transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
		}

		public override bool ProjectileConstraint() {
			return transform.position.x > SICAreaBounds.MaxPosition.x;
		}

		public override ProjectileType GetProjectileType() {
			return ProjectileType.MISSILE;
		}

		# endregion
	}
}