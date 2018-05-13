using UnityEngine;
using System.Collections;
using System;

namespace Score{
	[Serializable]
	public class Score {
        
		public Score(int score,string dateTime = "" )
		{
			Debug.Log("Score: "+score.ToString());
			m_score = score;
			m_dateTime = dateTime;
		}
        
		public int m_score;
		public string m_dateTime;
        
		public object GetScore(){
			return m_score;
		}
        
		public int GetIntScore()
		{
			return (int)m_score;
		}

	}
}
