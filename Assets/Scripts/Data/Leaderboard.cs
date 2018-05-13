using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Score{
	
	[Serializable]
	public class Leaderboard{
		
		public string m_leaderboardName; //Example: Stage 1

		//Set it to false for smaller times, etc
		[SerializeField]
		bool m_biggerIsHigher = true;

		[SerializeField]
		Score m_highestScore = null;

		[SerializeField]
		int m_maxNumberScores = 10;

		public Score HighestScore {get {return m_highestScore;}}

		public List<Score> Scores { get { return m_scoreList; }}

		//TODO change to stack and see if it is supported
		[SerializeField]
		List<Score> m_scoreList;

		public Leaderboard(string name = "", int maxNumberOfScores = 10, bool higherIsBigger = true)
		{
			m_leaderboardName = name;
			m_scoreList = new List<Score> ();
			m_highestScore = null;
		}

		public bool AddScore(Score score, out string output)
		{
			bool shouldBeAdded = false;
			output = String.Empty;


			CheckHighScore (score);

			if (m_scoreList.Count == m_maxNumberScores && m_maxNumberScores > 0)
			{
				output += "  Max size. Removing oldest entry.";
				m_scoreList.RemoveAt (0);
			}

			m_scoreList.Add (score);

			return shouldBeAdded;
		}

		public bool ShouldBeAdded(Score score){
			PrintList();
			//should be compare method...
			if(m_scoreList.Count == 0){
				Debug.Log(score.GetIntScore()+"");
				m_scoreList.Add(score);
				return true;
			}

			int lastIndex = m_scoreList.Count - 1;

			if(m_scoreList[lastIndex].GetIntScore() < score.GetIntScore()){
				m_scoreList.Add(score);
				m_scoreList.Sort();

				if (m_scoreList.Count > m_maxNumberScores)
				{
					m_scoreList.RemoveAt(lastIndex + 1);
				}

				PrintList();

				return true;
			}else{
				return false;
			}
		}

		void PrintList(){
			foreach(Score s in m_scoreList){
				Debug.Log("Score: "+s.GetScore());
			}
		}

		public void CheckHighScore(Score score)
		{
			if( (m_highestScore == null) ||
			   (score.GetIntScore() > m_highestScore.GetIntScore() && m_biggerIsHigher) ||
			   (score.GetIntScore() < m_highestScore.GetIntScore() && !m_biggerIsHigher))
			{
				m_highestScore = score;
			}
		
		}

	}

}