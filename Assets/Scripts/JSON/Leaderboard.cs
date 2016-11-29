using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Score{
	
	[Serializable]
	public class Leaderboard {
		
		public string m_leaderboardName; //Example: Stage 1

		Score m_highestScore = null;
		public Score HighestScore {get {return m_highestScore;}}

		//TODO change to stack and see if it is supported
		public List<Score> m_scoreList;

		public Leaderboard()
		{
		}

		public Leaderboard(string name)
		{
			m_leaderboardName = name;
		}

		public void AddScore(Score score)
		{
			m_scoreList.Add (score);
		}

	}

}