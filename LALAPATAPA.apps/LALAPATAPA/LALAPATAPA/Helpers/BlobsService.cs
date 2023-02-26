using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

namespace LALAPATAPA.Helpers
{
    public static class BlobsService
    {
        public static async Task<bool> ProcessAsync(byte[] data, string filename)
        {
            try
            {
                var content = new MemoryStream(data, false);
                string message;
                CloudStorageAccount storageAccount;
                CloudBlobContainer cloudBlobContainer;
                string storageConnectionString = Config.BlobsService;

                // Check whether the connection string can be parsed.
                if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
                {
                    // If the connection string is valid, proceed with operations against Blob storage here.
                    message = "Blobs Ready";
                }
                else
                {
                    // Otherwise, let the user know that they need to define the environment variable.
                    message = "A connection string has not been defined in the system environment variables. " +
                        "Add an environment variable named 'storageconnectionstring' with your storage " +
                        "connection string as a value.";
                }

                // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'balittanah' and append a GUID value to it to make the name unique.
                cloudBlobContainer = cloudBlobClient.GetContainerReference(Config.BlobsContainer);

                // Get a reference to the blob address, then upload the file to the blob.
                // Use the value of filename for the blob name.

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);
                await cloudBlockBlob.UploadFromStreamAsync(content);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps_BlobsService_ProcessAsync");
                return false;
            }
        }
    }
}
