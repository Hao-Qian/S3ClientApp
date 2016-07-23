using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3ClientApp
{
    /*
     This is a wrapper class for amazons3. It makes mocking possible for unit test 
     * and also it's easier to add logging for operations
     */
    internal class AmazonS3ClientWrapper : IAmazonS3Wrapper
    {
        
        private readonly IAmazonS3 _client;

        public AmazonS3ClientWrapper(IAmazonS3 client)
        {
            _client = client;
        }

        public ListBucketsResponse ListBuckets()
        {
           return  _client.ListBuckets();
        }

        public ListBucketsResponse ListBuckets(ListBucketsRequest request)
        {
            return _client.ListBuckets(request);
        }

        public Task<ListBucketsResponse> ListBucketsAsync(CancellationToken cancellationToken)
        {
            return _client.ListBucketsAsync(cancellationToken);
        }

        public Task<ListBucketsResponse> ListBucketsAsync(ListBucketsRequest request, CancellationToken cancellationToken)
        {
            return _client.ListBucketsAsync(request, cancellationToken);
        }
    }
}
