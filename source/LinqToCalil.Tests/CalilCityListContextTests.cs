﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil.Tests {

    /// <summary>
    /// <see cref="Karamem0.LinqToCalil.Calil.GetCityList"/> をテストします。
    /// </summary>
    [TestClass()]
    public class CalilCityListContextTests {

        /// <summary>
        /// 市町村リストを実行します。
        /// </summary>
        [TestMethod()]
        public void CityListAsEnumerableTest1() {
            var target = Calil.GetCityList();
            var actual = target.AsEnumerable();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item.ToString());
            }
        }

    }

}
