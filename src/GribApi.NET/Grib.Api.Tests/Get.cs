﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Grib.Api.Tests
{
    [TestFixture]
    public class Get
    {
        [SetUp]
        public void Init()
        {

       //     Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location));
            bool run = true;
            while (run) ;
            string dllPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(GribFile)).Location);
            string path = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", dllPath + ";" + path, EnvironmentVariableTarget.Process);
            Assert.IsTrue(File.Exists("Grib.Api.Native.dll"));
        }

        [Test]
        public void TestGetGrib2 ()
        {
            GribFile file = new GribFile(Settings.REDUCED_LATLON_GRB2);
            var msg = file.First();

            double latitudeOfFirstGridPointInDegrees = msg["latitudeOfFirstGridPointInDegrees"].AsDouble();
            Assert.AreEqual(latitudeOfFirstGridPointInDegrees, 90);
            double longitudeOfFirstGridPointInDegrees = msg["longitudeOfFirstGridPointInDegrees"].AsDouble();
            Assert.AreEqual(longitudeOfFirstGridPointInDegrees, 0);
            double latitudeOfLastGridPointInDegrees = msg["latitudeOfLastGridPointInDegrees"].AsDouble();
            Assert.AreEqual(latitudeOfLastGridPointInDegrees, -90);
            double longitudeOfLastGridPointInDegrees = msg["longitudeOfLastGridPointInDegrees"].AsDouble();
            Assert.AreEqual(longitudeOfLastGridPointInDegrees, -359.64);

            double jDirectionIncrementInDegrees = msg["jDirectionIncrementInDegrees"].AsDouble();
            Assert.AreEqual(jDirectionIncrementInDegrees, 0.36);
            double iDirectionIncrementInDegrees = msg["iDirectionIncrementInDegrees"].AsDouble();
            Assert.AreEqual(iDirectionIncrementInDegrees, -1.0E+100);

            int numberOfPointsAlongAParallel = msg["numberOfPointsAlongAParallel"].AsInt();
            Assert.AreEqual(numberOfPointsAlongAParallel, -1);
            int numberOfPointsAlongAMeridian = msg["numberOfPointsAlongAMeridian"].AsInt();
            Assert.AreEqual(numberOfPointsAlongAMeridian, 501);

            string packingType = msg["packingType"].AsString();
            Assert.AreEqual("grid_simple", packingType);
        }
    }
}