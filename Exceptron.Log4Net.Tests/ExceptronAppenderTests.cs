using System;
using Exceptron.Log4Net;
using log4net;
using log4net.Core;
using NUnit.Framework;
using log4net.Repository.Hierarchy;

namespace Exceptron.Log4Net.Tests
{
    [TestFixture]
    public class ExceptronAppenderTests
    {
        private ILog logger;
        private Exception simpleException = new Exception("Test Exception");
        [SetUp]
        public void TestAppender()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var ea = new ExceptronAppender();
            ea.UserId = "rob.chartier@gmail.com";
            ea.ApiKey = "6db93c30164443e1ab46d7eae2b41dbf";
            ea.ThrowExceptions = true;

            hierarchy.Root.AddAppender(ea);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

            logger = log4net.LogManager.GetLogger(typeof (ExceptronAppenderTests));
        }
        [Test]
        public void Info()
        {
            logger.Info("Info", simpleException);
        }
        [Test]
        public void Debug()
        {
            logger.Debug("Debug", simpleException);
        }
        [Test]
        public void Error()
        {
            logger.Error("Error", simpleException);
        }
        [Test]
        public void Fatal()
        {
            logger.Fatal("Fatal", simpleException);
        }
        [Test]
        public void Warn()
        {
            logger.Warn("Warn", simpleException);
        }


    }
}
