using System.Threading;
using System.Threading.Tasks;
using Amazon.S3.Model;

namespace S3ClientApp
{
    interface IAmazonS3Wrapper
    {
        //
        // Summary:
        //     Returns a list of all buckets owned by the authenticated sender of the request.
        //
        // Returns:
        //     The response from the ListBuckets service method, as returned by S3.
        ListBucketsResponse ListBuckets();
        //
        // Summary:
        //     Returns a list of all buckets owned by the authenticated sender of the request.
        //
        // Parameters:
        //   request:
        //     Container for the necessary parameters to execute the ListBuckets service
        //     method.
        //
        // Returns:
        //     The response from the ListBuckets service method, as returned by S3.
        ListBucketsResponse ListBuckets(ListBucketsRequest request);
        //
        // Summary:
        //     Returns a list of all buckets owned by the authenticated sender of the request.
        //
        // Parameters:
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Returns:
        //     The response from the ListBuckets service method, as returned by S3.
        Task<ListBucketsResponse> ListBucketsAsync(CancellationToken cancellationToken);
        //
        // Summary:
        //     Initiates the asynchronous execution of the ListBuckets operation.
        //
        // Parameters:
        //   request:
        //     Container for the necessary parameters to execute the ListBuckets operation.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Returns:
        //     The task object representing the asynchronous operation.
        Task<ListBucketsResponse> ListBucketsAsync(ListBucketsRequest request, CancellationToken cancellationToken);
    }
}
