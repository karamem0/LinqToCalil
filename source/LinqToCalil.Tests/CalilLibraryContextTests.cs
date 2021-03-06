﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil.Tests {

    /// <summary>
    /// <see cref="Karamem0.LinqToCalil.Calil.GetLibrary"/> をテストします。
    /// </summary>
    [TestClass()]
    public class CalilLibraryContextTests {

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
        /// 都道府県および市区町村を Where メソッドで指定して図書館検索を実行します。
        /// </summary>
        [TestMethod()]
        public void LibraryAsEnumerableTest1() {
            var target = Calil.GetLibrary(this.ApplicationKey);
            var actual = target
                .Where(x => x.Pref == "東京都")
                .Where(x => x.City == "大田区")
                .AsEnumerable();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// 都道府県および市区町村を <see cref="Karamem0.LinqToCalil.CalilLibraryParameter"/>
        /// のインスタンスで指定して図書館検索を実行します。
        /// </summary>
        [TestMethod()]
        public void LibraryAsEnumerableTest2() {
            var target = Calil.GetLibrary(
                this.ApplicationKey,
                new CalilLibraryParameter() {
                    Pref = "東京都",
                    City = "大田区",
                });
            var actual = target.AsEnumerable();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// 都道府県および市区町村をラムダ式で指定して図書館検索を実行します。
        /// </summary>
        [TestMethod()]
        public void LibraryAsEnumerableTest3() {
            var method = new Action(() => {
                var pref = "東京都";
                var city = "大田区";
                var target = Calil.GetLibrary(ApplicationKey);
                var actual = target
                    .Where(x => x.Pref == pref)
                    .Where(x => x.City == city)
                    .AsEnumerable();
                Assert.IsNotNull(actual);
                foreach (var item in actual) {
                    Debug.WriteLine(item.ToString());
                }
            });
            method.Invoke();
        }

    }

}
