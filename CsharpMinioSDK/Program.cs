using System;
using Amazon.S3;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CsharpMinioSDK
{
	class Program
	{

		static void Main(string[] args)
		{

			//var minio = new MinioClient(endpoint, accessKey, secretKey).WithSSL();
			Connection connection = Connection.GetInstanceConnection();
			AmazonS3Client amazonS3Client = connection.GetAmazonS3Client();
			Manipuler SychoniseFiles = new Manipuler();
			SychoniseFiles.RunSychoniAsync(amazonS3Client);
			//connection.SychoniFiles();

			string path = @"d:\testLocal";
			string[] files = Directory.GetFiles(path);
			
			foreach (string dir in files)
			{
				FileInfo info = new FileInfo(@dir);
				Console.WriteLine(info.LastWriteTime);
			}
			Console.ReadKey();
		}
	}
}




class Program
{
	
}

// Initialize the client with access credentials.

