using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 内部パラメーターを式ツリーに変換するための機能を提供する基底クラスを表します。
    /// </summary>
    /// <typeparam name="T">パラメーターの型。</typeparam>
    public abstract class CalilExpressionBuilder<T>
        where T : ICalilParameter {

        /// <summary>
        /// パラメーターの型を取得します。
        /// </summary>
        protected Type ElementType {
            get { return typeof(T); }
        }

        /// <summary>
        /// 指定した式ツリーと内部パラメーターを使用して新しい式ツリーを作成します。
        /// </summary>
        /// <param name="expression">変換対象の式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>変換された式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</returns>
        public abstract Expression Create(Expression expression);

    }

}
