using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 式ツリーを URI に変換するための機能を提供します。
    /// </summary>
    public class UriQueryParser {

        /// <summary>
        /// 解析する式ツリーを取得します。
        /// </summary>
        public Expression Expression { get; private set; }

        /// <summary>
        /// <see cref="LinqToCalil.UriQueryParser"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        public UriQueryParser(Expression expression) {
            this.Expression = expression;
        }

        /// <summary>
        /// 式ツリーを解析して URI に変換します。
        /// </summary>
        /// <param name="uri">ベース URI 文字列を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Uri"/>。</returns>
        public Uri Parse(string uri) {
            return new Uri(string.Format("{0}?{1}", uri,
                string.Join("&", this.SplitAndAlso(this.Expression)
                    .Select(x => this.CreateKeyValuePair((BinaryExpression)x))
                    .Select(x => string.Format("{0}={1}", x.Key, Uri.EscapeUriString(x.Value)))
                )
            ));
        }

        /// <summary>
        /// 指定した式ツリーに含まれる AND 演算のノード型の式ツリーを分割して、等価比較のノード型の式ツリーを反復処理する列挙子を返します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>
        /// 等価比較のノード型の式ツリーを反復処理する
        /// <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        private IEnumerable<Expression> SplitAndAlso(Expression expression) {
            if (expression.NodeType == ExpressionType.Lambda) {
                return this.SplitAndAlso(((LambdaExpression)expression).Body);
            }
            if (expression.NodeType == ExpressionType.AndAlso) {
                return Enumerable.Concat(
                    this.SplitAndAlso(((BinaryExpression)expression).Left),
                    this.SplitAndAlso(((BinaryExpression)expression).Right)
                );
            }
            if (expression.NodeType == ExpressionType.OrElse) {
                return Enumerable.Repeat(this.JoinOrElse(expression), 1);
            }
            if (expression.NodeType == ExpressionType.Equal) {
                return Enumerable.Repeat(expression, 1);
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// 指定した式ツリーに含まれる OR 演算のノード型の式ツリーを分割して、等価比較のノード型の式ツリーを反復処理する列挙子を返します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>
        /// 等価比較のノード型の式ツリーを反復処理する
        /// <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        private IEnumerable<Expression> SplitOrElse(Expression expression) {
            if (expression.NodeType == ExpressionType.OrElse) {
                return Enumerable.Concat(
                    this.SplitOrElse(((BinaryExpression)expression).Left),
                    this.SplitOrElse(((BinaryExpression)expression).Right)
                );
            }
            if (expression.NodeType == ExpressionType.Equal) {
                return Enumerable.Repeat(expression, 1);
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// 指定した式ツリーに含まれる OR 演算のノード型の式ツリーを分割して、等価比較のノード型の式ツリーを作成します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>等価比較のノード型の式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</returns>
        private Expression JoinOrElse(Expression expression) {
            var items = this.SplitOrElse(expression);
            var properties = items.Select(x => this.FindPropertyInfo((BinaryExpression)x));
            if (properties.GroupBy(x => x.Name).Count() == 1) {
                var property = properties.First();
                var attr = property.GetCustomAttribute<UriQueryAttribute>();
                return Expression.Equal(
                    Expression.Property(Expression.Constant(
                        Activator.CreateInstance(property.DeclaringType)), property),
                    Expression.Constant(string.Join(",", items
                        .Select(x => this.FindValue((BinaryExpression)x, property))
                        .Select(x => string.Format(string.Format("{{0:{0}}}", attr.Format), x)))));
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// 指定した等価比較の式ツリーからキーと値のペアを返します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>キーと値のペアを示す <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/>。</returns>
        private KeyValuePair<string, string> CreateKeyValuePair(BinaryExpression expression) {
            var property = this.FindPropertyInfo(expression);
            var value = this.FindValue(expression, property);
            var attr = property.GetCustomAttribute<UriQueryAttribute>();
            return (attr == null) ?
                new KeyValuePair<string, string>(property.Name, value.ToString()) :
                new KeyValuePair<string, string>(attr.Key, string.Format(string.Format("{{0:{0}}}", attr.Format), value));
        }

        /// <summary>
        /// 指定した等価比較の式ツリーから左オペランドのプロパティのメタ データを返します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>プロパティのメタ データを示す <see cref="System.Reflection.PropertyInfo"/>。</returns>
        private PropertyInfo FindPropertyInfo(BinaryExpression expression) {
            if (expression.Left.NodeType == ExpressionType.MemberAccess) {
                return (PropertyInfo)((MemberExpression)expression.Left).Member;
            }
            if (expression.Left.NodeType == ExpressionType.Convert) {
                return (PropertyInfo)((MemberExpression)((UnaryExpression)expression.Left).Operand).Member;
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// 指定した等価比較の式ツリーから右オペランドの値を返します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <param name="property">プロパティのメタ データ示す <see cref="System.Reflection.PropertyInfo"/>。</param>
        /// <returns>値を示す <see cref="System.Object"/>。</returns>
        private object FindValue(BinaryExpression expression, PropertyInfo property) {
            var typeInfo = property.PropertyType.GetNullableType().GetTypeInfo();
            if (typeInfo.IsEnum == true) {
                if (expression.Right.NodeType == ExpressionType.Constant) {
                    var value = Enum.ToObject(typeInfo.DeclaringType, ((ConstantExpression)expression.Right).Value);
                    var attr = typeInfo.GetDeclaredField(value.ToString()).GetCustomAttribute<EnumMemberAttribute>();
                    return (attr == null) ? value : attr.Value;
                }
                if (expression.Right.NodeType == ExpressionType.Convert) {
                    var value = ((ConstantExpression)((UnaryExpression)expression.Right).Operand).Value;
                    var attr = typeInfo.GetDeclaredField(value.ToString()).GetCustomAttribute<EnumMemberAttribute>();
                    return (attr == null) ? value : attr.Value;
                }
            } else {
                if (expression.Right.NodeType == ExpressionType.Constant) {
                    return ((ConstantExpression)expression.Right).Value;
                }
                if (expression.Right.NodeType == ExpressionType.MemberAccess) {
                    var lambda = Expression.Lambda((MemberExpression)expression.Right);
                    return lambda.Compile().DynamicInvoke();
                }
            }
            throw new InvalidOperationException();
        }

    }

}
