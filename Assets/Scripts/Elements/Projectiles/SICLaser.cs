using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;
using SpaceImpact.GameCore;

namespace SpaceImpact {

	public class SICLaser : SICProjectiles {
		// Public Variables	
		[SerializeField] private float rayDuration = 0.2f;
		[SerializeField] private int instances = 1;

		// Private Variables
		private float time;
		private Transform origin;
		private int instanceCount;

		// Static Variables

		# region Game Element
		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_LASER; }
		}
		# endregion

		# region Projectiles

		public override void Initialize(Transform sender) {
			base.Initialize(sender);
			time = 0f;
			origin = sender;

			Vector3 scale = transform.localScale;
			scale.x = 0f;
			transform.localScale = scale;

			instanceCount = 0;
			InvokeRepeating("DestroyElementsOnHit", 0.1f, (rayDuration / instances));
		}

		public override void ProjectileMovement() {
			float laserLength = (origin.position.x < 0) ?
				(SICAreaBounds.MaxPosition.x + Mathf.Abs(origin.position.x)) * 10f :
				(SICAreaBounds.MaxPosition.x - Mathf.Abs(origin.position.x)) * 10f;

			Vector3 scale = transform.localScale;
			//scale.x = laserLength;
			scale.x = Mathf.Lerp(0f, laserLength, time * MoveSpeed);
			transform.localScale = scale;

			transform.position = origin.position;

# if UNITY_EDITOR
			Debug.DrawLine(origin.position, new Vector3(SICAreaBounds.MaxPosition.x, origin.position.y));
			//Debug.DrawLine(origin.position, new Vector3((transform.localScale.x / 10f), origin.position.y), Color.red);
# endif
		}

		public override bool ProjectileConstraint() {
			time += Time.deltaTime;
			return time > rayDuration;
		}

		public override ProjectileType GetProjectileType() {
			return ProjectileType.LASER;
		}

		# endregion

		public void DestroyElementsOnHit() {
			instanceCount++;
			if (instanceCount > instances) {
				CancelInvoke("DestroyElementsOnHit");
				return;
			}

			// Moving Laser
			//RaycastHit2D[] objHit = Physics2D.RaycastAll(origin.position, Vector3.right, (transform.localScale.x / 10f), 1 << SICLayerManager.EnemyLayer);

			float laserLength = (origin.position.x < 0) ?
				(SICAreaBounds.MaxPosition.x + Mathf.Abs(origin.position.x)) * 10f :
				(SICAreaBounds.MaxPosition.x - Mathf.Abs(origin.position.x)) * 10f;

			RaycastHit2D[] objHit = Physics2D.RaycastAll(origin.position, Vector3.right, laserLength, 1 << SICLayerManager.EnemyLayer);

			if (objHit == null || objHit.Length <= 0)
				return;

			for (int i = 0; i < objHit.Length; i++) {
				SICEnemy enemyElement = objHit[i].transform.GetComponent<SICEnemy>();

				if (enemyElement == null)
					continue;

				enemyElement.SubtractHP(Damage);
				SICGameManager.SharedInstance.GameMetrics.AddScore(enemyElement.ScorePoint);
			}
		}
	}
}