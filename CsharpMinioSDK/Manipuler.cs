using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;




namespace CsharpMinioSDK
{
	public class Manipuler
	{
		public Manipuler()
		{

		}

		public static async Task SychoniAsync(AmazonS3Client amazonS3Client, string dic, string bucketName)
		{
			// amazonS3Client.ExceptionEvent += OnAmazonS3Exception;
			var listBucketResponse = await amazonS3Client.ListBucketsAsync();

			if (listBucketResponse.Buckets.Count > 0)
			{

				var listObjectsResponse = await amazonS3Client.ListObjectsAsync(bucketName);
				DirectoryInfo TheFolder = new DirectoryInfo(dic);
				foreach (FileInfo FileLocal in TheFolder.GetFiles())
				{
					if (Manipuler.ExistsBucket(FileLocal.Name, listObjectsResponse, amazonS3Client) == false)
					{
						await GestionFiles.UpLoad(bucketName, amazonS3Client, dic, FileLocal.Name);
					}
				}


				foreach (S3Object obj in listObjectsResponse.S3Objects)
				{
					string pathTemps = dic + obj.Key;
					FileInfo fi = new FileInfo(pathTemps);

					if (File.Exists(pathTemps))
					{

						CompareMd5 Md5FileLocal = new CompareMd5();
						string Md5Local = Md5FileLocal.GetMD5HashFromFile(pathTemps);
						//string Digest = ETag != null ? obj.ETag.Replace("\"", "").ToLower() : "";

						GetObjectRequest request = new GetObjectRequest();
						request.BucketName = bucketName;
						request.Key = obj.Key;
						GetObjectResponse response = await amazonS3Client.GetObjectAsync(request);



						CompareMd5 compareMd5 = new CompareMd5();
						Boolean result = compareMd5.CompareObjFile(response, pathTemps);
						if (result == true)
						{

							await GestionFiles.synchronisation(obj, bucketName, amazonS3Client, pathTemps, fi);
						}

						else
						{
							await GestionFiles.supprime(obj, bucketName, amazonS3Client);
						}
					}



				}

			}
		}
		
		private static bool ExistsBucket(string fileKey, ListObjectsResponse listObjectsResponse, AmazonS3Client amazonS3Client)
		{

			foreach (S3Object obj in listObjectsResponse.S3Objects)
			{
				if (obj.Key == fileKey)
				{
					return true;
				}
				
			}
			return false;

		}

	}
}


