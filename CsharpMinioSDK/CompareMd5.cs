using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsharpMinioSDK
{
	class CompareMd5
	{
		public string GetMD5HashFromFile(string fileName)
		{
			try
			{
				FileStream file = new FileStream(fileName, FileMode.Open);
				System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
				byte[] retVal = md5.ComputeHash(file);

				file.Close();

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < retVal.Length; i++)
				{
					sb.Append(retVal[i].ToString("x2"));
				}
				return sb.ToString();
			}



			catch (Exception ex)
			{
				throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
			}

		}

		public string GetMD5HashFromObj(GetObjectResponse response)
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] retVal = md5.ComputeHash(response.ResponseStream);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < retVal.Length; i++)
			{
				sb.Append(retVal[i].ToString("x2"));
			}
			return sb.ToString();
		}

		public Boolean CompareObjFile(GetObjectResponse response, string fileName)
		{
			if (this.GetMD5HashFromFile(fileName) == this.GetMD5HashFromObj(response))
			{
				return true;
			}
			else
			{
				return false;
			}


		}
	}


}
