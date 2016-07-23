using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace S3ClientApp
{
    internal class S3ClientApp
    {
        private static S3Config _s3Config;
        private static IAmazonS3Wrapper _clientWrapper;
        private static IDictionary<string, Action> _actions;
   
        private static void Main(string[] args)
        {
            if (args != null && args.Length >= 2)
            {
                LoadS3Config(args[0]);
                var client = new AmazonS3Client(_s3Config.AccessKey, _s3Config.SecretKey, GetRegionEndpoint(_s3Config.EndpointUrl));
                _clientWrapper = new AmazonS3ClientWrapper(client);
                InitializeActions();
                _actions[args[1].ToLower()]();
            }

        }

        private static void InitializeActions()
        {
            _actions = new Dictionary<string, Action>()
            {
                {"listbuckets", ListBuckets}
            };
        }

        private static void ListBuckets()
        {

            var buckets = _clientWrapper.ListBuckets();
            foreach (var bucket in buckets.Buckets)
            {
                Console.WriteLine(bucket.BucketName + " created on: " + bucket.CreationDate);
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
}