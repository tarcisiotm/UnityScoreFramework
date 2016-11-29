using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Score{
	
	[Serializable]
	public class Scores {

		public Score m_highestScore;
		public List<Leaderboard> m_leaderboards;
		public List<Score> m_scores;

	}

}
