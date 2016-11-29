using UnityEngine;
using System.Collections;
using System;

namespace Score{
	[Serializable]
	public class Score {

		public Score(int score, string description = "", string dateTime = "" )
		{
			m_score = score;
			m_description = description;
			m_dateTime = dateTime;
		}

		public int m_score;
		public string m_description;
		public string m_dateTime;

		public int GetScore()
		{
			return m_score;
		}

	}
}
