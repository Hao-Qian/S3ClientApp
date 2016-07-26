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

        private static void Main(string[] args)
        {
            if (args != null && args.Length >= 2)
            {
                LoadS3Config(args[0]);
                InitializeActions();
                _client = new AmazonS3Client(_s3Config.AccessKey, _s3Config.SecretKey, GetRegionEndpoint(_s3Config.EndpointUrl));
                _uploadFacade = new UploadFacade(_client, _directories, new TransferUtility(_client));
                _actions[args[1].ToLower()]();
            }

        }

        private static void InitializeActions()
        {
            _actions = new Dictionary<string, Action>()
            {
                {"listbuckets", ListBuckets},
                {"bulkupload", BulkUpload},
                {"listobjects", ListObjects}
            };
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
                BucketName = ""
            };
            var list = _client.ListObjects(listObjectRequest);
            foreach (var item in list.S3Objects)
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
            foreach (var dir in _directories)
            {
                Console.WriteLine("process.." + dir);
            }
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