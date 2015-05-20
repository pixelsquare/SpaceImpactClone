﻿using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public enum ParticleType {
		NONE = 0,
		UNIT_EXPLOSION = 1
	}

	public class SICGameParticle : SICGameElement {
		// Public Variables	

		// Private Variables	

		// Static Variables

		# region Game Element
		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_PARTICLE; }
		}
		# endregion

		public virtual ParticleType GetParticleType() {
			return ParticleType.NONE;
		}
	}
}