﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SpaceImpact {

	public class SICGameUtility {
		public static List<GameObject> GetAllVisibleEnemies() {
			return SICObjectPoolManager.SharedInstance.GetParent(SICObjectPoolName.OBJECT_ENEMY).objectList.FindAll(a => a.activeInHierarchy);
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

		// TODO: FIX Finding proper target
		public static Transform GetNearestUntargettedEnemy(Vector3 from) {
			Transform result = null;

			if (GetAllVisibleEnemies().Count <= 0)
				return null;

			if (GetNearestEnemies(from).Count <= 0)
				return null;

			for (int i = 0; i < GetNearestEnemies(from).Count; i++) {
				GameObject enemyObj = GetNearestEnemies(from)[i];
				SICGameEnemy enemy = enemyObj.GetComponent<SICGameEnemy>();
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

		public static List<GameObject> GetAllProjectilesOfType(ProjectileType type) {
			return SICObjectPoolManager.SharedInstance.GetParent(SICObjectPoolName.OBJECT_PROJECTILE, type).objectList.FindAll(a => a.activeInHierarchy);
		}

		public static void SetAllElementsRecursively(Transform root, bool enable) {
			foreach (Transform obj in root) {
				SICGameElement element = obj.GetComponent<SICGameElement>();
				if (element != null) {
					if (enable)
						element.EnableElement();
					else
						element.DisableElement();
				}

				SetAllElementsRecursively(obj, enable);
			}
		}
	}
}