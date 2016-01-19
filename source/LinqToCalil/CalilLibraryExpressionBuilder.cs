using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 図書館検索の内部パラメーターを式ツリーに変換します。
    /// </summary>
    public class CalilLibraryExpressionBuilder : CalilExpressionBuilder<CalilLibraryParameter> {

        /// <summary>
        /// アプリケーション キーを取得します。
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 実行結果の書式を取得します。
        /// </summary>
        public Format Format { get; set; }

        /// <summary>
        /// 指定した式ツリーと内部パラメーターを使用して新しい式ツリーを作成します。
        /// </summary>
        /// <param name="expression">変換対象の式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>変換された式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</returns>
        public override Expression Create(Expression expression) {
            var typeInfo = this.ElementType.GetTypeInfo();
            var instance = Activator.CreateInstance(this.ElementType);
            return Expression.AndAlso(
                expression,
                Expression.AndAlso(
                    Expression.Equal(
                        Expression.Property(
                            Expression.Constant(instance), typeInfo.GetDeclaredProperty("AppKey")),
                        Expression.Constant(this.AppKey)),
                    Expression.Equal(
                        Expression.Property(
                            Expression.Constant(instance), typeInfo.GetDeclaredProperty("Format")),
                            Expression.Convert(
                                Expression.Constant(this.Format), typeof(Nullable<Format>)))));
        }

    }

}
