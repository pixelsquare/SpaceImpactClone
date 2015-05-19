using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICGameUnit : SICGameElement {
		// Public Variables	
		[SerializeField] private SpriteRenderer mainTexture;
		[SerializeField] private int healthPoints = 1;
		[SerializeField] private int scorePoint = 0;

		// Private Variables	
		private SpriteRenderer originalTexture;
		private int originalHealthPoints;
		public int originalScorePoint;

		// Static Variables


		public SpriteRenderer MainTexture { get { return mainTexture; } }

		# region Game Element
		public override void Awake() {
			base.Awake();
			originalTexture = mainTexture;
			originalHealthPoints = healthPoints;
			originalScorePoint = scorePoint;
		}

		public override void OnEnable() {
			base.OnEnable();
			mainTexture = originalTexture;
			healthPoints = originalHealthPoints;
			scorePoint = originalScorePoint;
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_UNIT; }
		}
		# endregion

		public int HealthPoints { get { return healthPoints; } }

		public int ScorePoint { get { return scorePoint; } }

		public override void DisableElement() {
			base.DisableElement();
			ShowExplosionFX();
		}

		public void AddHP(int hp) {
			this.healthPoints += hp;
			SetHP(this.healthPoints);
		}

		public void SubtractHP(int hp) {
			this.healthPoints -= hp;
			SetHP(this.healthPoints);

			if (healthPoints <= 0)
				DisableElement();
		}

		public virtual void SetHP(int hp) {
			healthPoints = hp;
		}

		public void AddScorePoint(int score) {
			this.scorePoint += score;
			SetScorePoint(this.scorePoint);
		}

		public void SubtractScorePoint(int score) {
			this.scorePoint -= score;
			SetScorePoint(this.scorePoint);
		}

		public virtual void SetScorePoint(int score) {
			scorePoint = score;
		}
	}
}