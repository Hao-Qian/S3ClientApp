using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3ClientApp;

namespace S3ClientAppTest
{
    [TestClass]
    public class ClientConfigReaderTest
    {
        const string RelativePath = @"..\..\S3ConfigTest.xml";

        [TestMethod]
        public void ConfigsAreLoadedCorrectly_WhenValidConfigFileSupplied()
        {
            var result = LoadConfigFile();
            const int expectedNumberOfConfig = 1;
            Assert.AreEqual(expectedNumberOfConfig, result.Count);
        }

        [TestMethod]
        public void ScretKeyCanBeGetCorrectly()
        {
            const string expectedSecretKey = "dummysecretkey";
            var result = LoadConfigFile();
            Assert.AreEqual(expectedSecretKey, result[0].SecretKey);  
        }

        [TestMethod]
        public void AccessKeyCanBeGetCorrectly()
        {
            const string expectedAccessKey = "dummyaccesskey";
            var result = LoadConfigFile();
            Assert.AreEqual(expectedAccessKey, result[0].AccessKey);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ThrowsFileNotFoundException_WhenS3ConfigFileNotFound()
        {
            const string inavlidFilePath = @"lalala.xml";
            new ClientConfigReader().LoadConfig(inavlidFilePath);
        }

        [TestMethod]
        public void DirectoryCanBeGetCorrectly()
        {
            const string expectedResult = @"C:\Project\temp\temp";
            var dirs = GetClientConfigReader().LoadDataDirectories(RelativePath);
            Assert.AreEqual(expectedResult, dirs[0]);
        }

        private IList<S3Config> LoadConfigFile()
        {

            return GetClientConfigReader().LoadConfig(RelativePath);
        }

        private ClientConfigReader GetClientConfigReader()
        {
            return new ClientConfigReader();
        }
    }
}
