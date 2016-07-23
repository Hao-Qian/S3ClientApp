using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3ClientApp
{
    /*
     more s3 options could be included in this VO 
     */
    public class S3Config
    {
        public string EndpointUrl { set; get; }
        public string SecretKey { set; get; }
        public string AccessKey { set; get; }
    }
}
