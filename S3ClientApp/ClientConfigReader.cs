using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace S3ClientApp
{
    /*
        load s3 configs from a xml file.
     */
    public class ClientConfigReader
    {
        public List<S3Config> LoadConfig(String filePath)
        {
            var doc = XDocument.Load(filePath);
            var s3Configs = doc.Root.Elements("Endpoints").Elements("Endpoint")
                              .Select(x => new S3Config
                              {
                                  EndpointUrl = (string)x.Element("Url"),
                                  SecretKey = (string)x.Element("ScretKey"),
                                  AccessKey = (string)x.Element("AccessKey")
                              })
                              .ToList();

            return s3Configs;
        }

        public List<String> LoadDataDirectories(String filePath)
        {
            var doc = XDocument.Load(filePath);
            var query = doc.Root.Elements("UploadDirectories").Elements("Directory");
            return query.Select(element => element.Value).ToList();
        }
    }
}
