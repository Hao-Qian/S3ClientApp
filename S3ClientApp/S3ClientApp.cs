using System;
using System.Collections.Generic;
using System.Diagnostics;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace S3ClientApp
{
    internal class S3ClientApp
    {
        private static S3Config _s3Config;
        private static IList<String> _directories;
        private static IAmazonS3 _client;
        private static IDictionary<string, Action> _actions;
        private static IUploadFacade _uploadFacade;
        private static string _storedFilePath;
        private static bool _assignedValidMD5;

        private static void Main(string[] args)
        {
            /*
                args[0] --- file path to s3 client config file
             *  args[1] --- command eg listbuckets
             *  args[2] --- single file you would like to upload
             *  args[3] --- a flag to control if assigning valid or invalid md5checksum value to a put object request (this is just for testing)
             */
            if (args != null && args.Length >= 2)
            {
                LoadS3Config(args[0]);
                InitializeActions();
                _client = new AmazonS3Client(_s3Config.AccessKey, _s3Config.SecretKey, GetRegionEndpoint(_s3Config.EndpointUrl));
                _uploadFacade = new UploadFacade(_client, _directories, new TransferUtility(_client));
                _storedFilePath = args[2];
                _assignedValidMD5 = args[3].ToLower().Equals("true");
                _actions[args[1].ToLower()]();
            }

        }

        private static void InitializeActions()
        {
            _actions = new Dictionary<string, Action>()
            {
                {"listbuckets", ListBuckets},
                {"bulkupload", BulkUpload},
                {"putobject", PutObject},
                {"listobjects", ListObjects}
            };
        }

        private static void PutObject()
        {
            var request = new PutObjectRequest()
            {
                BucketName = _client.ListBuckets().Buckets[1].BucketName,
                FilePath = _storedFilePath,
                MD5Digest = MD5Utilities.GenerateMD5(_assignedValidMD5 ? _storedFilePath : @"..\..\Data\dummy.text")

            };
            try
            {
                var result = _client.PutObject(request);
                Console.WriteLine("uploaded " + _storedFilePath + " successfully to " + _client.ListBuckets().Buckets[1].BucketName);
                //Console.WriteLine("MD5Checksum : " + request.MD5Digest);
                Console.WriteLine("http status code: " + result.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine("Failed to upload: " + _storedFilePath + " caused by : " + ex.Message);
                Console.WriteLine("Exception error code is :" + ex.ErrorCode);
                Console.WriteLine("Exception error type is :" + ex.ErrorType);
            }
        }

        private static void ListBuckets()
        {
            var buckets = _client.ListBuckets();
            foreach (var bucket in buckets.Buckets)
            {
                Console.WriteLine(bucket.BucketName + " created on: " + bucket.CreationDate);
            }
        }

        private static void BulkUpload()
        {
            _uploadFacade.BulkUpload(_directories, _client.ListBuckets().Buckets[0].BucketName);
        }

        private static void ListObjects()
        {
            var listObjectRequest = new ListObjectsRequest()
            {
                BucketName = _client.ListBuckets().Buckets[0].BucketName
            };
            var lala = _client.ListObjects(listObjectRequest);
            foreach (var item in lala.S3Objects)
            {
                Console.WriteLine(item.Key + "  last modified time : " + item.LastModified);
            }
        }

        /*
         multiple s3 endpoints are supported but currenly only first one is used.
         */
        private static void LoadS3Config(String configPath)
        {
            var s3ConfigReader = new ClientConfigReader();
            var configs  = s3ConfigReader.LoadConfig(configPath);
            if (configs != null && configs.Count >= 1)
            {
                _s3Config = configs[0];
            }

            _directories = s3ConfigReader.LoadDataDirectories(configPath);
        }

        private static RegionEndpoint GetRegionEndpoint(String url)
        {
            var endpoint = RegionEndpoint.EUWest1;
            if (String.IsNullOrEmpty(url)&&url.ToLower().Equals("RegionEndpoint.USEast1"))
            {
                endpoint =  RegionEndpoint.USEast1;
            }
            return endpoint;
        }
        }

    internal interface IAmazonS3Wrapper
    {
    }
}