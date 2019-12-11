using Amazon.S3;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;

namespace CsharpMinioSDK
{
	class Program
	{
		private static List<Task> tasks = new List<Task>();

		public static void Main(string[] args)
		{

			Program p=new Program();
			Task.Run(p.Gestiontasks).GetAwaiter().GetResult();

		}
		
		public  async Task Gestiontasks()
		{
			var cts = new CancellationTokenSource();
			var ct = cts.Token;
			Connection connection = Connection.GetInstanceConnection();
			AmazonS3Client amazonS3Client = connection.GetAmazonS3Client();

			var listBucketResponse = await amazonS3Client.ListBucketsAsync();

			foreach (var bucket in listBucketResponse.Buckets)
			{
				Console.Out.WriteLine("bucket '" + bucket.BucketName + "' created at " + bucket.CreationDate);
			}

			Console.WriteLine("Input filepath exemple: D:\\\\testLocal\\\\");
			string dic = Console.ReadLine();    //"D:\\testLocal\\";
			Console.WriteLine("Input BucketName");
			string bucketName = Console.ReadLine();
			var task1 = Manipuler.SychoniAsync(amazonS3Client, dic, bucketName);
			
			tasks.Add(task1);

			Console.WriteLine("Input filepath exemple: D:\\\\testLocal\\\\");
			string dic2 = Console.ReadLine();    //"D:\\testLocal\\";
			Console.WriteLine("Input BucketName");
			string bucketName2 = Console.ReadLine();
			var task2 = Task.Run(() =>  Manipuler.SychoniAsync(amazonS3Client, dic2, bucketName2),ct);
		
			tasks.Add(task2);
			cts.Cancel();
			foreach (Task task in tasks)
			{
				Console.WriteLine("{0,10} {1,20} ", task.Id, task.Status);
			}
	
			Console.ReadKey();
		}
	}

}



