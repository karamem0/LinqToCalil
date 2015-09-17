using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 式ツリーからクエリを作成または実行します。
    /// </summary>
    internal abstract class QueryProvider : IQueryProvider {

        /// <summary>
        /// 指定した式ツリーから厳密に型指定されたクエリを作成します。
        /// </summary>
        /// <typeparam name="TElement">クエリのデータ ソースの型。</typeparam>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>作成された <see cref="T:System.Linq.IQueryable`1"/>。</returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) {
            return new Queryable<TElement>(expression, this);
        }

        /// <summary>
        /// 指定した式ツリーからクエリを作成します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>作成された <see cref="System.Linq.IQueryable"/>。</returns>
        public IQueryable CreateQuery(Expression expression) {
            return (IQueryable)Activator.CreateInstance(
                typeof(Queryable<>).MakeGenericType(expression.Type.GetEnumerableType() ?? expression.Type),
                new object[] { expression, this });
        }

        /// <summary>
        /// 指定した式ツリーで表される厳密に型指定されたクエリを実行します。
        /// </summary>
        /// <typeparam name="TResult">実行結果の型。</typeparam>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>実行結果を示す値。</returns>
        public TResult Execute<TResult>(Expression expression) {
            return (TResult)this.Execute(expression);
        }

        /// <summary>
        /// 指定した式ツリーで表されるクエリを実行します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>実行結果を示す <see cref="System.Object"/>。</returns>
        public abstract object Execute(Expression expression);

    }

}
