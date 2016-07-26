using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3ClientApp
{
    public interface IUploadFacade
    {
        void BulkUpload(IList<string> dirs, String bucketName);
    }
}
