using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;



namespace CsharpMinioSDK
{
	class GestionFiles 
	{
		
		public static async Task UpLoad(string bucketName, AmazonS3Client amazonS3Client, string path, string filename)
		{
			string pathTemps = path + filename;
			PutObjectRequest requestUpdate = new PutObjectRequest();
			var fileStream = new FileStream(pathTemps, FileMode.Open, FileAccess.Read);
			requestUpdate.InputStream = fileStream;
			requestUpdate.BucketName = bucketName;
			requestUpdate.Key = filename;
			requestUpdate.CannedACL = S3CannedACL.PublicRead;
			//Task.Run(() => Manipuler.amazonS3Client.PutObjectAsync(requestUpdate)).GetAwaiter().GetResult();
			await amazonS3Client.PutObjectAsync(requestUpdate);

		}


		public static async Task supprime(S3Object obj, string bucketName, AmazonS3Client amazonS3Client)
		{
			DeleteObjectRequest requestDelete = new DeleteObjectRequest
			{
				BucketName = bucketName,
				Key = obj.Key,
			};
			await amazonS3Client.DeleteObjectAsync(requestDelete);
		}

		public static async Task synchronisation(S3Object obj, string bucketName, AmazonS3Client amazonS3Client, string pathTemps, FileInfo fi)
		{
	
			DeleteObjectRequest requestDelete = new DeleteObjectRequest
			{
				BucketName = bucketName,
				Key = obj.Key,
			};
			await amazonS3Client.DeleteObjectAsync(requestDelete);

			PutObjectRequest requestUpdate = new PutObjectRequest();
			var fileStream = new FileStream(pathTemps, FileMode.Open, FileAccess.Read);
			requestUpdate.InputStream = fileStream;
			requestUpdate.BucketName = bucketName;
			requestUpdate.Key = fi.Name;
			requestUpdate.CannedACL = S3CannedACL.PublicRead;
			//Task.Run(() => Manipuler.amazonS3Client.PutObjectAsync(requestUpdate)).GetAwaiter().GetResult();
			await amazonS3Client.PutObjectAsync(requestUpdate);
		}

	
	}
}
