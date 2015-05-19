using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;
using System.Collections.Generic;

namespace SpaceImpact.GameCore {

	public class SICGameManager : MonoBehaviour {
		// Public Variables	
		[SerializeField] private Camera gameCamera;
		[SerializeField] private SICGameMetrics gameMetrics;
		[SerializeField] private STAGETYPE stage;
		[SerializeField] private List<SICGameStage> refStages;

		// Private Variables	
		private GameObject spaceShipObj;
		private SICSpaceShip spaceShip;

		private GameObject stageObj;
		private SICGameStage curStage;

		private SICCameraMover cameraMover;

		// Static Variables
		private static SICGameManager instance;

		public static SICGameManager SharedInstance { get { return instance; } }

		public SICSpaceShip SpaceShip { get { return spaceShip; } }

		public Camera GameCamera { get { return gameCamera; } } 

		public SICGameMetrics GameMetrics { get { return gameMetrics; } }

		public List<SICGameStage> RefStages { get { return refStages; } }

		public SICGameStage CurrentStage { get { return curStage; } } 

		private void Awake() {
			instance = this;
			cameraMover = gameCamera.GetComponent<SICCameraMover>();
		}

		private void Start() {
			gameMetrics.SetLife(SICGameSettings.DEFAULT_LIVES);
			gameMetrics.SetScore(SICGameSettings.DEFAULT_SCORE);
			gameMetrics.SetSpecial(SICGameSettings.DEFAULT_SPECIAL);

			SetStage(stage);
		}

# if UNITY_EDITOR
		public void Update() {
			if (Input.GetKeyDown(KeyCode.L)) {
				SetStage(STAGETYPE.STAGE_1);
			}
		}
# endif

		public void LoadStage(STAGETYPE stageType) {
			stageObj = SICObjectPoolManager.SharedInstance.GetObject(GetStage(stageType).OBJECT_ID);
			if (stageObj != null) {
				curStage = stageObj.GetComponent<SICGameStage>();
				curStage.EnableElement();

				cameraMover.Initialize(curStage.PointA, curStage.PointB);
			}

			spaceShipObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_SPACE_SHIP);
			if (spaceShipObj != null) {
				spaceShip = spaceShipObj.GetComponent<SICSpaceShip>();
				spaceShip.SetHP(gameMetrics.GetLives());
				spaceShip.SetSpecial(gameMetrics.GetSpecial());
				spaceShip.EnableElement();
			}
		}

		public SICGameStage GetStage(STAGETYPE stageType) {
			return refStages.Find(a => a.GetStageType() == stageType);
		}

		public void SetStage(STAGETYPE stageType) {
			stage = stageType;

			if (stageType == STAGETYPE.NONE)
				return;

			LoadStage(stage);
		}
	}
}