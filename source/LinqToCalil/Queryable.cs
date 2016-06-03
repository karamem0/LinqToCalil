using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// データ ソースに対するクエリを評価する機能を提供します。
    /// </summary>
    /// <typeparam name="T">データ ソースの型。</typeparam>
    internal class Queryable<T> : IQueryable<T> {

        /// <summary>
        /// 式ツリーが実行されたときに返される要素の型を取得します。
        /// </summary>
        public Type ElementType {
            get { return typeof(T); }
        }

        /// <summary>
        /// 現在のインスタンスに関連付けられた式ツリーを取得します。
        /// </summary>
        public Expression Expression { get; private set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられたクエリ プロバイダーを取得します。
        /// </summary>
        public IQueryProvider Provider { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.Queryable{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <param name="provider">クエリ プロバイダーを示す <see cref="System.Linq.IQueryProvider"/>。</param>
        public Queryable(Expression expression, IQueryProvider provider) {
            this.Expression = expression;
            this.Provider = provider;
        }

        /// <summary>
        /// 式ツリーの実行結果を反復処理する厳密に型指定された列挙子を返します。
        /// </summary>
        /// <returns>実行結果を反復処理する <see cref="System.Collections.Generic.IEnumerator{T}"/>。</returns>
        public IEnumerator<T> GetEnumerator() {
            var enumerable = this.Provider.Execute<IEnumerable<T>>(this.Expression);
            if (enumerable == null) {
                throw new InvalidOperationException();
            }
            return enumerable.GetEnumerator();
        }

        /// <summary>
        /// 式ツリーの実行結果を反復処理する列挙子を返します。
        /// </summary>
        /// <returns>実行結果を反復処理する <see cref="System.Collections.IEnumerator"/>。</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

    }

}
