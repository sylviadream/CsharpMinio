using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
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
			//Task.Run(() => SychoniAsync(amazonS3Client)).GetAwaiter().GetResult();
		}

		public static async Task SychoniAsync(AmazonS3Client amazonS3Client, List<Task> tasks)
		{


			// amazonS3Client.ExceptionEvent += OnAmazonS3Exception;
			
			var listBucketResponse = await amazonS3Client.ListBucketsAsync();
		
			foreach (var bucket in listBucketResponse.Buckets)
			{
				Console.Out.WriteLine("bucket '" + bucket.BucketName + "' created at " + bucket.CreationDate);
			}
			Console.WriteLine("Input filepath exemple: D:\\\\testLocal\\\\");
			string dic = Console.ReadLine();    //"D:\\testLocal\\";	


			if (listBucketResponse.Buckets.Count > 0)
			{
				var bucketName = listBucketResponse.Buckets[0].BucketName;

				var listObjectsResponse = await amazonS3Client.ListObjectsAsync(bucketName);
				DirectoryInfo TheFolder = new DirectoryInfo(dic);
				foreach (FileInfo FileLocal in TheFolder.GetFiles())
				{
					if (Manipuler.ExistsBucket(FileLocal.Name, listObjectsResponse, amazonS3Client) == false)
					{
						await GestionFiles.UpLoad(bucketName,amazonS3Client, dic, FileLocal.Name);
					}
				}


				foreach (S3Object obj in listObjectsResponse.S3Objects)
				{
					string pathTemps = dic + obj.Key;
					FileInfo fi = new FileInfo(pathTemps);

					if (File.Exists(pathTemps))
					{
					    if (obj.Size != fi.Length)
						{
						           
								await GestionFiles.synchronisation(obj, bucketName, amazonS3Client, pathTemps, fi);
						}
					}
					else
				    {
					    await GestionFiles.supprime(obj, bucketName,amazonS3Client);
					};
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


