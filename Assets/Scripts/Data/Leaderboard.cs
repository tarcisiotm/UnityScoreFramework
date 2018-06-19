using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Score{
	
	[Serializable]
	public class Leaderboard{
		
		public string m_leaderboardName; //Example: Stage 1

		//Set it to false for smaller times, etc
		[SerializeField]
		bool m_biggerIsHigher = true;

		[SerializeField]
		SerigyScore m_highestScore = null;

		[SerializeField]
		int m_maxNumberScores = 5;

		public Score HighestScore {get {return m_highestScore;}}

		public List<SerigyScore> Scores { get { return m_scoreList; }}

		//TODO change to stack and see if it is supported
		[SerializeField]
		List<SerigyScore> m_scoreList;

		public Leaderboard(string name = "", int maxNumberOfScores = 5, bool higherIsBigger = true)
		{
			m_leaderboardName = name;
			m_scoreList = new List<SerigyScore> ();
			m_highestScore = null;
		}

		public bool AddScore(SerigyScore score, out string output)
		{
			bool shouldBeAdded = ShouldBeAdded(score);
			output = String.Empty;

			CheckHighestScore (score);

			//if (m_scoreList.Count == m_maxNumberScores && m_maxNumberScores > 0)
			//{
			//	output += "  Max size. Removing oldest entry.";
			//	m_scoreList.RemoveAt (0);
			//}

			//m_scoreList.Add (score);

			return shouldBeAdded;
		}

		public bool ShouldBeAdded(SerigyScore score, bool p_add = true){
			PrintList();
			//should be compare method...
			if(m_scoreList.Count == 0){
				Debug.Log(score.GetIntScore()+"");
				if (p_add)
				{
					m_scoreList.Add(score);
				}
				return true;
			}
            
			int lastIndex = m_scoreList.Count - 1;

			if(m_scoreList.Count < m_maxNumberScores || m_scoreList[lastIndex].GetIntScore() < score.GetIntScore()){
				if (p_add)
				{
					m_scoreList.Add(score);
					m_scoreList.Sort();
					m_scoreList.Reverse();

					if (m_scoreList.Count > m_maxNumberScores)
					{
						m_scoreList.RemoveAt(lastIndex + 1);
					}
				}

				PrintList();

				return true;
			}else{
				return false;
			}
		}

		void PrintList(){
			foreach(SerigyScore s in m_scoreList){
				Debug.Log("Score: "+s.GetScore());
			}
		}

		public void CheckHighestScore(SerigyScore score)
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