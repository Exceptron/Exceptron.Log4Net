using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

        public ExceptronAppenderTests()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var ea = new ExceptronAppender();
            ea.UserId = "rob.chartier@gmail.com";
            ea.ApiKey = "6db93c30164443e1ab46d7eae2b41dbf";
            ea.ThrowExceptions = true;

            hierarchy.Root.AddAppender(ea);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

        }

        [SetUp]
        public void TestAppender()
        {

            logger = log4net.LogManager.GetLogger(typeof (ExceptronAppenderTests));
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        private static void ThrowsException()
        {
            throw new Exception("Test Exception");
        }
        [Test]
        public void Info()
        {
            try
            {
                ThrowsException();
            }
            catch (Exception e)
            {
                logger.Info("Info", e);
            }
        }
        [Test]
        public void Debug()
        {
            try
            {
                ThrowsException();
            }
            catch (Exception e)
            {
                logger.Debug("Debug", e);
            } 
        }
        [Test]
        public void Error()
        {
            try
            {
                ThrowsException();
            }
            catch (Exception e)
            {
                logger.Error("Error", e);
            } 
        }
        [Test]
        public void Fatal()
        {
            try
            {
                ThrowsException();
            }
            catch (Exception e)
            {
                logger.Fatal("Fatal", e);
            } 
        }
        [Test]
        public void Warn()
        {
            try
            {
                ThrowsException();
            }
            catch (Exception e)
            {
                logger.Warn("Warn", e);
            } 
        }


    }
}
