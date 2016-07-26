using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace S3ClientApp
{
    public class UploadFacade: IUploadFacade
    {
        private readonly IAmazonS3 _client;
        private readonly IList<string> _directories;
        private readonly TransferUtility _transferUtility;
        public UploadFacade(IAmazonS3 client, IList<String> directories,TransferUtility transferUtility)
        {
            _client = client;
            _directories = directories;
            _transferUtility = transferUtility;
        }
        
        public void BulkUpload(IList<string> dirs, string bucketName)
        {
            foreach (var dir in _directories)
            {
                Console.WriteLine("To upload file in  " + dir);
                _transferUtility.UploadDirectory(dir, bucketName);  
            } 
        }
    }
}
