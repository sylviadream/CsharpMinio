using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CsharpMinioSDK
{
	public class Manipuler
	{
		public Manipuler()
		{

		}

		public void RunSychoniAsync(AmazonS3Client amazonS3Client)
		{
			Task.Run(() => SychoniAsync(amazonS3Client)).GetAwaiter().GetResult();
		}

		public async Task SychoniAsync(AmazonS3Client amazonS3Client)
		{


			// amazonS3Client.ExceptionEvent += OnAmazonS3Exception;

			var listBucketResponse = await amazonS3Client.ListBucketsAsync();

			foreach (var bucket in listBucketResponse.Buckets)
			{
				Console.Out.WriteLine("bucket '" + bucket.BucketName + "' created at " + bucket.CreationDate);
			}
			if (listBucketResponse.Buckets.Count > 0)
			{
				var bucketName = listBucketResponse.Buckets[0].BucketName;

				var listObjectsResponse = await amazonS3Client.ListObjectsAsync(bucketName);

				string dic = "D:\\testLocal\\";
				//Console.WriteLine("Input filepath");
				//string dic = Console.ReadLine();


				foreach (S3Object obj in listObjectsResponse.S3Objects)
				{
					string pathTemps = dic + obj.Key;
					if (File.Exists(pathTemps))
					{


						FileInfo fi = new FileInfo(pathTemps);

						if (obj.Size != fi.Length)
						{
							Console.WriteLine(obj.Size.ToString() + "///" + fi.Length.ToString());
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
							await amazonS3Client.PutObjectAsync(requestUpdate);

						}
					}
					else
					{

						DeleteObjectRequest request = new DeleteObjectRequest
						{
							BucketName = bucketName,
							Key = obj.Key,
						};

						await amazonS3Client.DeleteObjectAsync(request);
					};




					Console.Out.WriteLine(obj.ToString());
				}
				Console.ReadKey();
			}


		}
		public static Boolean CompaereFile(double sizeServer, double sizeLocal)
		{
			if (sizeServer == sizeLocal)
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


