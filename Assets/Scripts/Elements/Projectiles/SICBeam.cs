using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;
using SpaceImpact.GameCore;

namespace SpaceImpact {

	public class SICBeam : SICProjectiles {
		// Public Variables	
		[SerializeField] private float beamHeight = 50f;

		// Private Variables

		// Static Variables

		# region Game Element
		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_BEAM; }
		}
		# endregion

		# region Projectiles

		public override void Initialize(Transform sender) {
			base.Initialize(sender);
			transform.position = sender.position;

			Vector3 scale = transform.localScale;
			scale.y = beamHeight;
			transform.localScale = scale;
		}

		public override void ProjectileMovement() {
			transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
		}

		public override bool ProjectileConstraint() {
			return transform.position.x > SICAreaBounds.MaxPosition.x;
		}

		public override ProjectileType GetProjectileType() {
			return ProjectileType.BEAM;
		}

		# endregion

		public void OnTriggerStay2D(Collider2D col) {
			if (col2D == null)
				return;

			if (col.gameObject.layer == SICLayerManager.EnemyLayer) {
				SICEnemy enemyElement = col.GetComponent<SICEnemy>();

				if (enemyElement == null)
					return;

				enemyElement.SubtractHP(Damage);
				SubtractDurability(1);
				SICGameManager.SharedInstance.GameMetrics.AddScore(enemyElement.ScorePoint);
			}
		}
	}
}