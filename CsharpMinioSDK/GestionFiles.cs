using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsharpMinioSDK
{
	class GestionFiles : IGestionFile
	{
		public void ajouter()
		{
		}


		public static async Task supprime(string keyName)
		{
			/*try
			{
				var deleteObjectRequest = new DeleteObjectRequest
				{
					BucketName = bucketName,
					Key = keyName
				};

				Console.WriteLine("Deleting an object");
				await client.DeleteObjectAsync(deleteObjectRequest);
			}
			catch (AmazonS3Exception e)
			{
				Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
			}*/
		}

		public void synchronisation()
		{
			throw new NotImplementedException();
		}

		public void supprime()
		{
			throw new NotImplementedException();
		}
	}
}
