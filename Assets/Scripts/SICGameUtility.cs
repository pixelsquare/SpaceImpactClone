using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SpaceImpact {

	public class SICGameUtility {
		public static List<GameObject> GetAllEnemies() {
			return SICObjectPoolManager.SharedInstance.GetParent(SICObjectPoolName.OBJECT_ENEMY).objectList;
		}

		public static List<GameObject> GetAllVisibleEnemies() {
			List<GameObject> result = new List<GameObject>();
			for (int i = 0; i < GetAllEnemies().Count; i++) {
				GameObject obj = GetAllEnemies()[i];
				if (!obj.activeInHierarchy)
					continue;

				result.Add(obj);
			}

			return result;
		}

		public static Transform GetRandomEnemy() {
			Transform result = null;

			if (GetAllVisibleEnemies().Count <= 0)
				return null;

			int maxCount = GetAllVisibleEnemies().Count;
			float[] weights = new float[maxCount];
			float curWeight = 0f;

			for (int i = 0; i < maxCount; i++) {
				curWeight += (1f / maxCount);
				weights[i] = curWeight;
			}

			float rand = UnityEngine.Random.Range(0f, 100f) / 100f;

			int indx = 0;
			for (int i = 0; i < weights.Length; i++) {
				if (rand < weights[i]) {
					indx = i;
					break;
				}
			}

			result = GetAllVisibleEnemies()[indx].transform;
			return result;
		}

		public static Transform GetNearestEnemy(Vector3 from) {
			Transform result = null;

			if (GetAllVisibleEnemies().Count <= 0)
				return null;

			float lowestDistance = 999999f;
			for (int i = 0; i < GetAllVisibleEnemies().Count; i++) {
				GameObject obj = GetAllVisibleEnemies()[i];
				float distance = Vector3.Distance(from, obj.transform.position);

				if (distance > lowestDistance) {
					continue;
				}

				result = obj.transform;
				lowestDistance = distance;
			}

			return result;
		}

		public static Transform GetNearestUntargettedEnemy(Vector3 from) {
			Transform result = null;

			if (GetAllVisibleEnemies().Count <= 0)
				return null;

			if (GetNearestEnemies(from).Count <= 0)
				return null;

			for (int i = 0; i < GetNearestEnemies(from).Count; i++) {
				GameObject enemyObj = GetNearestEnemies(from)[i];
				SICEnemy enemy = enemyObj.GetComponent<SICEnemy>();
				if (enemy == null)
					continue;

				if (enemy.IsTargetted)
					continue;

				result = enemyObj.transform;
				break;
			}

			return result;
		}

		public static List<GameObject> GetNearestEnemies(Vector3 from) {
			List<GameObject> result = new List<GameObject>();

			if (GetAllVisibleEnemies().Count <= 0)
				return null;

			result.AddRange(GetAllVisibleEnemies());
			result.Sort(delegate(GameObject a, GameObject b) {
				float distA = Vector3.Distance(from, a.transform.position);
				float distB = Vector3.Distance(from, b.transform.position);
				return distA.CompareTo(distB);
			});

			return result;
		}

		public static Vector3 GetRelativePosition(Transform origin, Vector3 position) {
			Vector3 distance = position - origin.position;
			Vector3 relativePosition = Vector3.zero;
			relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
			relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
			relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

			return relativePosition;
		}
	}
}