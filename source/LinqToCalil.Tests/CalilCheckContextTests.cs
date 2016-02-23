using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Karamem0.LinqToCalil.Tests {

    /// <summary>
    /// <see cref="Karamem0.LinqToCalil.Calil.GetCheck"/> をテストします。
    /// </summary>
    [TestClass()]
    public class CalilCheckContextTests {

        /// <summary>
        /// アプリケーション キーを取得または設定します。
        /// </summary>
        public string ApplicationKey { get; set; }

        /// <summary>
        /// テストを初期化します。
        /// </summary>
        [TestInitialize()]
        public void TestInitialize() {
            this.ApplicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
        }

        /// <summary>
        /// OrElse 演算子を使用して蔵書検索を実行します。
        /// </summary>
        [TestMethod()]
        public void CheckAsEnumerableTest1() {
            var target = Calil.GetCheck(this.ApplicationKey);
            var actual = target
                .Where(x => x.SystemId == "Tokyo_Ota")
                .Where(x => x.Isbn == "403217010A" || x.Isbn == "4032171009")
                .Polling(r => {
                    Debug.WriteLine("Polling:" + DateTime.Now.ToString());
                    foreach (var item in r) {
                        Debug.WriteLine(item);
                    }
                    return true;
                })
                .AsEnumerable();
            Assert.IsNotNull(actual);
            var result = actual.ToList();
            Assert.IsNotNull(result);
            Debug.WriteLine("Completed:" + DateTime.Now.ToString());
            foreach (var item in result) {
                Debug.WriteLine(item);
            }
        }

        /// <summary>
        /// カンマ区切りで指定して蔵書検索を実行します。
        /// </summary>
        [TestMethod()]
        public void CheckAsEnumerableTest2() {
            var target = Calil.GetCheck(this.ApplicationKey);
            var actual = target
                .Where(x => x.SystemId == "Tokyo_Ota")
                .Where(x => x.Isbn == "403217010X,4032171009")
                .Polling(r => {
                    Debug.WriteLine("Polling:" + DateTime.Now.ToString());
                    foreach (var item in r) {
                        Debug.WriteLine(item);
                    }
                    return true;
                })
                .AsEnumerable();
            Assert.IsNotNull(actual);
            var result = actual.ToList();
            Assert.IsNotNull(result);
            Debug.WriteLine("Completed:" + DateTime.Now.ToString());
            foreach (var item in result) {
                Debug.WriteLine(item);
            }
        }

    }

}
