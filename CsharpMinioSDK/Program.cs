using Amazon.S3;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;

namespace CsharpMinioSDK
{
	class Program
	{

		static void Main(string[] args)
		{
			List<Task> tasks = new List<Task>();
			//var minio = new MinioClient(endpoint, accessKey, secretKey).WithSSL();
			Connection connection = Connection.GetInstanceConnection();
			AmazonS3Client amazonS3Client = connection.GetAmazonS3Client();
			//Manipuler SychoniseFiles = new Manipuler();
			Task.Run(() => Manipuler.SychoniAsync(amazonS3Client, tasks)).GetAwaiter().GetResult();
		
				GestionTaskScheduler gs = new GestionTaskScheduler();
				IEnumerable<Task> scheduledTasksList = gs.GetScheduledTasksList();
				foreach (Task task in scheduledTasksList)
				{
					Console.WriteLine("{0,10} {1,20} ", task.Id, task.Status);
				}

				Console.ReadKey();
		}
	}

}


	/*for (int i = 1; i <= 10; i++)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
			{

				Console.WriteLine($"task numero {obj}");
			}), i);*/
		//GetScheduledTasks();

			//System.Threading.Tasks.TaskScheduler;
			//ThreadPool.QueueUserWorkItem(ThreadProc);
			//.RunSychoniAsync(amazonS3Client);
			//connection.SychoniFiles();

		   


	           



// Initialize the client with access credentials.

