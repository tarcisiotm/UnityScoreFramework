using UnityEngine;
using System.Collections;
using System.IO;

namespace Score{
	public class IOManager : MonoBehaviour {

		public IOManager m_instance;
		readonly string m_pathToFile = "/Scores";

		void Awake (){
			
			//TODO Proper Singleton
			if (m_instance == null)
			{
				m_instance = this;
			}

		}
	
		public bool WriteToFile (string nameOfFile, string output, bool overwrite = false){ 
				/*
				output = obj.ToJsonPrettyPrintString ();
				print ("output: " + output);
				string fileName = Application.persistentDataPath + "/" + nameOfFile;

				try {
					using (StreamWriter fileWriter = overwrite ? File.CreateText(fileName) : File.AppendText(fileName)) {
						fileWriter.Write (output);
						return true;
					}
				} catch (System.Exception ex) {
					output = "Exceção de IO na escrita: " + ex.Message;
					return false;
				}
				*/
				return false;
			}
			
	}
}
