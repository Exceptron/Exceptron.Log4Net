using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exceptron.Log4Net.Client;
using log4net;
using log4net.Core;
using NUnit.Framework;
using log4net.Repository.Hierarchy;

namespace Exceptron.Log4Net.Tests
{
    [TestFixture]
    public class ExceptronAppenderTests
    {
        [SetUp]
        public void TestAppender()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            Exceptron.Log4Net.Client.ExceptronAppender ea = new ExceptronAppender();
            ea.ApiKey = "6db93c30164443e1ab46d7eae2b41dbf";

            hierarchy.Root.AddAppender(ea);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

        }
        [Test]
        public void Info()
        {
            log4net.LogManager.GetLogger(typeof(ExceptronAppenderTests)).Info("Info");
        }
        [Test]
        public void Debug()
        {
            log4net.LogManager.GetLogger(typeof(ExceptronAppenderTests)).Debug("Debug");
        }
        [Test]
        public void Error()
        {
            log4net.LogManager.GetLogger(typeof(ExceptronAppenderTests)).Error("Error");
        }
        [Test]
        public void Fatal()
        {
            log4net.LogManager.GetLogger(typeof(ExceptronAppenderTests)).Fatal("Fatal");
        }
        [Test]
        public void Warn()
        {
            log4net.LogManager.GetLogger(typeof(ExceptronAppenderTests)).Warn("Warn");
        }


    }
}
