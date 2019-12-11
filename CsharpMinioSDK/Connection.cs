using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using System.IO;
using Amazon.S3.Model;

namespace CsharpMinioSDK
{



	public sealed class Connection
	{
		private static AmazonS3Client _amazonS3Client = null;
		public static Connection connectionInstance = null;
		private const string accessKey = "IIH7HW7X2KDMU1BC1629";
		private const string secretKey = "GhIbXcQKZNAv+J21il+GOjvgT2Z2o+Lq2DfnX2ip";


		private Connection()
		{
			//Task.Run(MainAsync).GetAwaiter().GetResult();
			var config = new AmazonS3Config
			{
				RegionEndpoint = RegionEndpoint.USEast1, // 
				ServiceURL = "http://localhost:9000", // 
				ForcePathStyle = true // true
			};
			_amazonS3Client = new AmazonS3Client(accessKey, secretKey, config);

		}

		public AmazonS3Client GetAmazonS3Client()
		{

			return _amazonS3Client;
		}

		public static Connection GetInstanceConnection()
		{

			if (connectionInstance == null)
			{
				connectionInstance = new Connection();
			}
			return connectionInstance;
		}
	}

}

