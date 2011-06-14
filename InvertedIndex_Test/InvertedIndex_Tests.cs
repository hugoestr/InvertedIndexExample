using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Moq;

using InvertedIndexExample;

namespace InvertedIndex_Test
{
    [TestFixture]
    public class InvertedIndex_Tests
    {
        InvertedIndex i;

        [SetUp]
        public void SetUp()
        {
            i = new InvertedIndex();
        }

        [Test]
        public void Should_Add_Records_To_Index()
        {
            var key = "dogs";
            var value = 23;

            i.Add(key, value);

            Assert.That(i.IndexSize, Is.EqualTo(1));
        }

        [Test]
        public void Should_Remove_Record_From_Index()
        {
            var key = "dogs";
            var value = 23;

            i.Add(key, value);
            i.Remove(key);

            Assert.That(i.IndexSize, Is.EqualTo(0));
        }

        [Test]
        public void Should_Search_For_Terms()
        {
            i.Add("dogs", 12);
            i.Add("dogs", 56);

            var result = i.Search("dogs");

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void Should_Remove_Index_By_Value()
        {
            i.Add("dogs", 12);
            i.Add("dogs", 56);

            i.Remove(56);

            var result = i.Search("dogs");

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Should_Break_Multiple_Terms()
        {
            i.Add("dogs", 12);
            i.Add("cats", 56);

            var result = i.Search("dogs cats");

            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}
