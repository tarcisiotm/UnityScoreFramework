//Based on https://www.codeproject.com/articles/23769/simple-string-encryption-and-decryption-with-sourc 30/12/2016 - 14h40
//License: https://www.codeproject.com/info/cpol10.aspx
//Reference: https://msdn.microsoft.com/en-us/library/system.security.cryptography.rijndaelmanaged.aspx 30/12/2016 - 15h00

using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System;
using System.IO;

namespace Score
{
	public static class XOREncrypt {


		
		/// <summary>
		/// Encrypt the given string using the specified key.
		/// </summary>
		/// <param name="strToEncrypt">The string to be encrypted.</param>
		/// <param name="strKey">The encryption key.</param>
		/// <returns>The encrypted string.</returns>
		public static string Encrypt(string strToEncrypt, string strKey)
		{
			try
			{
				TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
				MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
				byte[] byteHash, byteBuff;
				string strTempKey = strKey;
				byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
				objHashMD5 = null;
				objDESCrypto.Key = byteHash;
				objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
				byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
				return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
					TransformFinalBlock(byteBuff, 0, byteBuff.Length));
			}
			catch (Exception ex)
			{
				Debug.LogError("Encrypt - Wrong Input. " + ex.Message);
				return "Wrong Input. " + ex.Message;
			}
		}

		/// <summary>
		/// Decrypt the given string using the specified key.
		/// </summary>
		/// <param name="strEncrypted">The string to be decrypted.</param>
		/// <param name="strKey">The decryption key.</param>
		/// <returns>The decrypted string.</returns>
		public static string Decrypt(string strEncrypted, string strKey)
		{
			try
			{
				TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
				MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
				byte[] byteHash, byteBuff;
				string strTempKey = strKey;
				byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
				objHashMD5 = null;
				objDESCrypto.Key = byteHash;
				objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
				byteBuff = Convert.FromBase64String(strEncrypted);
				string strDecrypted = ASCIIEncoding.ASCII.GetString
					(objDESCrypto.CreateDecryptor().TransformFinalBlock
						(byteBuff, 0, byteBuff.Length));
				objDESCrypto = null;
				return strDecrypted;
			}
			catch (Exception ex)
			{
				Debug.LogError("Decrypt - Wrong Input. " + ex.Message);
				return "Wrong Input. " + ex.Message;
			}
		}

	
				public static void Test()
				{
					try
					{
						string original = "Here is some data to encrypt!";

						using (RijndaelManaged myRijndael = new RijndaelManaged())
						{
							Debug.Log(myRijndael.Key.Length + "  " + myRijndael.IV.Length + "    " + myRijndael.BlockSize);
							myRijndael.GenerateKey();
							myRijndael.GenerateIV();

							byte[] encrypted = EncryptStringToBytes(original);

							string roundtrip = DecryptStringFromBytes(encrypted);

							Debug.Log("Original: " + original + "Round Trip: " + roundtrip);
						}

					}
					catch (Exception e)
					{
						Console.WriteLine("Error: {0}", e.Message);
					}
					
				}

		static byte[] m_key = new byte[] { 178, 255, 247, 76, 240, 218, 229, 85, 147, 153, 134, 177, 27, 189, 30, 130,183, 240, 146, 125, 63, 77, 120, 22, 137, 234, 71, 189, 146, 2, 229, 49};

		static byte[] m_iv = new byte[] { 64, 234,  24, 163, 116, 244, 215, 219, 173,  24, 183,  92,  80,  35,  75,  35 };
			
		public static byte[] EncryptStringToBytes (string plainText)
		{
			//byte[] key = new byte[] { 178, 255, 247, 76, 240, 218, 229, 85, 147, 153, 134, 177, 27, 189, 30, 130,183, 240, 146, 125, 63, 77, 120, 22, 137, 234, 71, 189, 146, 2, 229, 49};

			//byte[] iv = new byte[] { 64, 234,  24, 163, 116, 244, 215, 219, 173,  24, 183,  92,  80,  35,  75,  35 };

			//TODO Scramble key with unique ids from device

			CheckNullArguments (Encoding.UTF8.GetBytes(plainText), m_key, m_iv);

			byte[] encrypted;

			using (RijndaelManaged rijAlg = new RijndaelManaged ()) {
				rijAlg.Key = m_key;
				rijAlg.IV = m_iv;

				ICryptoTransform encryptor = rijAlg.CreateEncryptor (rijAlg.Key, rijAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream ()) {
					using (CryptoStream csEncrypt = new CryptoStream (msEncrypt, encryptor, CryptoStreamMode.Write)) {
						using (StreamWriter swEncrypt = new StreamWriter (csEncrypt)) {
							swEncrypt.Write (plainText);
						}
						encrypted = msEncrypt.ToArray ();
					}
				}
			}

			return encrypted;
		}

		public static string DecryptStringFromBytes (byte[] cipherText)
		{
			CheckNullArguments (cipherText, m_key, m_iv);

			string output = null;

			using (RijndaelManaged rijAlg = new RijndaelManaged ()) {
				rijAlg.Key = m_key;
				rijAlg.IV = m_iv;

				ICryptoTransform decryptor = rijAlg.CreateDecryptor (rijAlg.Key, rijAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream (cipherText)) {
					using (CryptoStream csDecrypt = new CryptoStream (msDecrypt, decryptor, CryptoStreamMode.Read)) {
						using (StreamReader srDecrypt = new StreamReader (csDecrypt)) {
							output = srDecrypt.ReadToEnd ();
						}
					}
				}
			}
			return output;
		}

		static void CheckNullArguments (byte[] cipherText, byte[] Key, byte[] IV)
		{
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException ("cipherText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException ("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException ("IV");
		}

	}
}
