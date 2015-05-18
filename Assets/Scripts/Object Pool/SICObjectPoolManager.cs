using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SpaceImpact {

	public class SICObjectPoolManager : MonoBehaviour {
		// Public Variables	
		[SerializeField] private SICGameElement[] refObject;

		// Private Variables
		private static SICObjectPoolManager instance;
		private List<ObjectPooled> objParents;

		// Static Variables
		public const string PARENT_NAME_FORMAT = "{0} Parent";
		public const string OBJECT_NAME_FORMAT = "{0} {1}";

		public static SICObjectPoolManager SharedInstance { get { return instance; } }

		public class ObjectPooled {
			public Transform refObjParent;
			public SICGameElement refObj;
			public List<GameObject> objectList;

			public void AddObject(GameObject obj) {
				if (objectList.Contains(obj))
					return;

				obj.transform.parent = refObjParent.transform;
				objectList.Add(obj);
			}

			public GameObject GetObject() {
				for (int i = 0; i < objectList.Count; i++) {
					if (!objectList[i].activeInHierarchy) {
						return objectList[i];
					}
				}

				GameObject obj = (GameObject)UnityEngine.Object.Instantiate(refObj.gameObject);
				obj.name = string.Format(OBJECT_NAME_FORMAT, refObj.gameObject.name, objectList.Count);
				obj.SetActive(false);
				AddObject(obj);
				return obj;
			}
		}

		public void Awake() {
			instance = this;

			objParents = new List<ObjectPooled>();

			for (int i = 0; i < refObject.Length; i++) {
				GameObject objParent = new GameObject(string.Format(PARENT_NAME_FORMAT, refObject[i].gameObject.name));
				objParent.transform.parent = transform;

				ObjectPooled pooledObj = new ObjectPooled();
				pooledObj.refObjParent = objParent.transform;
				pooledObj.refObj = refObject[i];
				pooledObj.objectList = new List<GameObject>();
				pooledObj.GetObject();
				objParents.Add(pooledObj);
			}
		}

		public void Start() {
			SICGameElement[] allElements = GameObject.FindObjectsOfType<SICGameElement>();

			for (int i = 0; i < allElements.Length; i++) {
				for (int j = 0; j < objParents.Count; j++) {
					if (allElements[i].OBJECT_ID == objParents[j].refObj.OBJECT_ID) {
						objParents[j].AddObject(allElements[i].gameObject);
					}
				}
			}
		}

		public GameObject GetObject(string id) {
			for (int i = 0; i < objParents.Count; i++) {
				if (objParents[i].refObj.OBJECT_ID == id) {
					return objParents[i].GetObject();
				}
			}

			return null;
		}

		public ObjectPooled GetParent(string id) {
			return objParents.Find(parent => parent.refObj.OBJECT_ID == id);
		}
	}
}