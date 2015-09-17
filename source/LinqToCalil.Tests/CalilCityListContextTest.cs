using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinqToCalil.Tests {

    /// <summary>
    /// <see cref="LinqToCalil.Calil.GetCityList"/> をテストします。
    /// </summary>
    [TestClass()]
    public class CalilCityListContextTest {

        /// <summary>
        /// 市町村リストを実行します。
        /// </summary>
        [TestMethod()]
        public void CityListAsEnumerable() {
            var target = Calil.GetCityList();
            var actual = target.AsEnumerable();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item.ToString());
            }
        }

    }

}
