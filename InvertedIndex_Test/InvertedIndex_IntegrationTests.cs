using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NUnit.Framework;
using Moq;

using InvertedIndexExample;

namespace InvertedIndex_Test
{
    [TestFixture]
    class InvertedIndex_IntegrationTests
    {
        InvertedIndex i;


        [SetUp]
        public void SetUp()
        {
            i = new InvertedIndex();
        }

        [Test]
        public void Should_Save_Index()
        {
            i.Add("dogs", 12);
            i.Add("cats", 56);

            var file = "index.bin";

            if (File.Exists(file))
                File.Delete(file);

            i.Save(file);

            var result = File.Exists(file);

            Assert.That(result);
        }

        [Test]
        public void Should_Load_Index()
        {
            var file = "index.bin";

            if (File.Exists(file))
                File.Delete(file);

            i.Add("dogs", 12);
            i.Add("cats", 56);

            i.Save(file);
            i = null;

            i = new InvertedIndex();
            i.Load(file);

            var result = i.Search("cats dogs");

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void Should_Load_Index_At_Constructor_If_Path_Provided()
        {
            var file = "testLoad.bin";
            i = new InvertedIndex(file);

            Assert.That(i.IndexSize, Is.EqualTo(2));
        }
    }
}

