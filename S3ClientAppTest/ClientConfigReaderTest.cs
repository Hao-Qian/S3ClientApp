using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3ClientApp;

namespace S3ClientAppTest
{
    [TestClass]
    public class ClientConfigReaderTest
    {
        [TestMethod]
        public void ConfigsAreLoadedCorrectly_WhenValidConfigFileSupplied()
        {
            const string relativePath = @"..\..\S3ConfigTest.xml";
            var clientConfigReader = new ClientConfigReader();
            var result = clientConfigReader.LoadConfig(relativePath);
            const int expectedNumberOfConfig = 1;
            Assert.AreEqual(expectedNumberOfConfig, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ThrowsFileNotFoundException_WhenS3ConfigFileNotFound()
        {
            const string inavlidFilePath = @"lalala.xml";
            new ClientConfigReader().LoadConfig(inavlidFilePath);
        }
    }
}
