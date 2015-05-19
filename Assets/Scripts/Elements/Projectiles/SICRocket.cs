using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public class SICRocket : SICProjectiles {
		// Public Variables
		[SerializeField] private float waveLength = 1f;
		[SerializeField] private float amplitude = 5f;
		[SerializeField] private float startTime;

		// Private Variables	
		private float time;
		private Vector3 minPosition;
		private Vector3 maxPosition;

		private Transform target;

		// Static Variables

		# region Game Element
		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_ROCKET; }
		}
		# endregion

		# region Projectile

		public override void Initialize(Transform sender) {
			base.Initialize(sender);
			transform.position = sender.position;
			time = startTime;
			minPosition = -Vector3.up;
			maxPosition = Vector3.up;

			SetTarget(SICGameUtility.GetNearestUntargettedEnemy(sender.position));

			if (target != null) {
				startTime = Vector3.Distance(transform.position, target.position);
			}
		}

		public override void ProjectileMovement() {
			time += Time.deltaTime;
			float waveUp = Mathf.Lerp(minPosition.y, maxPosition.y, Mathf.PingPong(time, waveLength) / waveLength) * amplitude;

			if (target == null) {
				// Get the nearest target
				Transform nextTarget = SICGameUtility.GetNearestUntargettedEnemy(transform.position);
				if (nextTarget != null) {
					// Determine its X direction
					float dirX = Mathf.Sign(nextTarget.position.x - transform.position.x);

					if (dirX > 0) {
						SetTarget(nextTarget);
					}
				}

				transform.Translate(new Vector3(MoveSpeed, waveUp, 0f) * Time.deltaTime);
			}
			else {
				transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

				if (!target.gameObject.activeInHierarchy) {
					target = null;
				}
			}
		}

		public override bool ProjectileConstraint() {
			return transform.position.x > SICAreaBounds.MaxPosition.x;
		}

		public override ProjectileType GetProjectileType() {
			return ProjectileType.ROCKET;
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
			}
		}

		public void SetTarget(Transform tgt) {
			if (tgt == null)
				return;

			Transform tmpTarget = tgt;
			SICEnemy enemyTarget = tmpTarget.GetComponent<SICEnemy>();

			if (enemyTarget == null)
				return;

			enemyTarget.SetTargetted(true);
			target = tmpTarget;
		}
	}
}