using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.Helpers.Tests
{
    [TestClass()]
    public class RetrievingListTests
    {
        [TestMethod()]
        public void RetrievingListTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new RetrievingList<int>(null));
        }

        [TestMethod()]
        public void AddTest()
        {
            var list = new RetrievingList<Cellar>();
            list.Add(new Cellar(StorageLightning.Darkness, new StorageTemperature(-12, 122, 22)));
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod()]
        public void RetrieveFirstOrDefaultTest()
        {
            var list = new RetrievingList<Cellar>();
            var cellar1 = new Cellar(StorageLightning.Darkness, new StorageTemperature(-12, 122, 22));
            var cellar2 = new Cellar(StorageLightning.Daylight, new StorageTemperature(-12, 122, 22));
            list.Add(cellar1);
            list.Add(cellar2);

            var result = list.RetrieveFirstOrDefault(c => c.Lightning == StorageLightning.Darkness);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod()]
        public void RetrieveTest()
        {
            var list = new RetrievingList<Cellar>();
            var cellar1 = new Cellar(StorageLightning.Darkness, new StorageTemperature(-12, 122, 22));
            var cellar2 = new Cellar(StorageLightning.Daylight, new StorageTemperature(-12, 122, 22));
            list.Add(cellar1);
            list.Add(cellar2);
            var t = new StorageTemperature(-12, 122, 22);
            var result = list.Retrieve(c => c.Temperature.Equals(t));
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod()]
        public void RetrieveAllTest()
        {
            var list = new RetrievingList<Cellar>();
            var cellar1 = new Cellar(StorageLightning.Darkness, new StorageTemperature(-12, 122, 22));
            var cellar2 = new Cellar(StorageLightning.Daylight, new StorageTemperature(-12, 122, 22));
            list.Add(cellar1);
            list.Add(cellar2);
            var all = list.RetrieveAll();
            Assert.IsNotNull(all);
            Assert.AreEqual(2, all.Count());
            Assert.AreEqual(0, list.Count);
        }
    }
}