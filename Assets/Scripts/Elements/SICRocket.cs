using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public class SICRocket : SICGameElement {
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

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_ROCKET; }
		}

		public void Initialize(Transform sender) {
			transform.position = sender.position;
			time = startTime;

			minPosition = -Vector3.up;
			maxPosition = Vector3.up;

			target = SICGameUtility.GetNearestUntargettedEnemy(sender.position);
			if (target != null) {
				//Debug.Log("DOT " + Mathf.Sign(Vector3.Cross(transform.position, target.position).z));
				startTime = Vector3.Distance(transform.position, target.position);
				SICEnemy targetEnemy = target.GetComponent<SICEnemy>();
				targetEnemy.SetTargetted(true);
			}
		}

		public void Update() {
			RocketMovement();
		}

		public void OnTriggerEnter2D(Collider2D col) {
			if (col.gameObject.layer == SICLayerManager.EnemyLayer) {
				SICGameElement element = col.GetComponent<SICGameElement>();
				if (element == null) {
					return;
				}

				element.DisableElement();
				DisableElement();
			}
		}

		private void RocketMovement() {
			time += Time.deltaTime;
			float waveUp = Mathf.Lerp(minPosition.y, maxPosition.y, Mathf.PingPong(time, waveLength) / waveLength) * amplitude;

			if (target != null) {
				transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

				if (!target.gameObject.activeInHierarchy) {
					target = null;
				}
			}
			else {
				transform.Translate(new Vector3(MoveSpeed, waveUp, 0f) * Time.deltaTime);
			}

			RocketConstraint();
		}

		private void RocketConstraint() {
			if (transform.position.x > SICAreaBounds.MaxPosition.x) {
				DisableElement();
			}
		}

		public void SetTarget(Transform tgt) {
			target = tgt;
		}
	}
}