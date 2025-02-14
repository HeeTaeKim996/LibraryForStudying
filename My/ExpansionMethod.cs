using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using hellWorld;

namespace My
{
    public static class ExpansionMethod
    {
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            #region 공부정리
            /*
            ○ this
            - this는 this가 위치에 따라 역할이 달라짐
            - 클래스 내부에 위치한 경우, this는 클래스의 인스턴스를 지칭
            - 매서드 내부(매개변수)에 위치한 경우, this는 해당 매서드를 구현하는 객체를 지칭. (Ex. a.MethodA(this..) 에서 this는 a를 지칭

            ○ Static
            - 매개변수에 this를 사용하는 매서드를 확장 매서드라 함
            - 확장매서드라 불리는 이유는, this를 사용하여 a.Method(this..); 의 형태가, a.GetType == Class A라 했을 때, A의 매서드를 사용하는 형태와 동일하기 때문,
              실제로는 확장매서드가 this의 대상이 되는 객체의 클래스의 자체 매서드가 아님에도, 자체매서드의 사용 형태와 동일하기 때문
             ※ 확장매서드의 이러한 성질에 의해, 확장매서드를 담고 있는 클래스는, Generic이 아닌, 정적 클래스여야 함(앞에 static이 있는)
             - 만약 static이 없는 generic 클래스이고, 확장매서드가 내부에 정의됐다면, 클래스 A의 인스턴스를 a라 했을 때, 그리고 확장 매서드를 구현하는 객체를 b라 했을 때, 
               인스턴스 a를 사용하여 확장매서드를 구현하는 형태는, a.b.확장매서드(this..) 의 형태가 되기 때문에, 이를 방지하고자, 
               매개변수에 this를 담은 확장매서드를 담은 클래스는 앞에 static이 위치한, 인스탠스화가 불가능한 정적 클래스여야함

            ○ INumerable<T>
            - 위 this의 대상이 되는 매서드 구현 객체는, INumerable<T>를 인터페이스로 구현 가능해야 함. int, List .. 등은 INumerable<T>를 인터페이스로 구현하기 때문에, 
              구현 객체가 될 수 있음
            */
            #endregion
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T t in source)
            {
                if (predicate(t))
                {
                    yield return t;
                }
            }
        }
        public static IEnumerable<TResult> MySelect<TSource, TResult>(IEnumerable<TSource>source, Func<TSource, TResult> function)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if(function == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach(TSource t in source)
            {
                yield return function(t);
            }
        }
        public static void MyForeach<T>(this List<T> source, Action<T> function)
        {
            #region 공부정리
            /*
            위에 Action이 왔는데, 
             - 반환값이 있는 Delegate는, Func<매개변수1,매개변수2...,매개변수n, 리턴변수> ; 를 사용
             - 반환값이 없는 Delegate는, Action<매개변수1,매개변수2,...,매개변수n>; 를 사용
            */
            #endregion
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            foreach (T t in source)
            {
                function(t);
            }
        }
        public static T MySum<T>(this IEnumerable<T> source) where T : struct, INumber<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            T sum = T.Zero;

            foreach (T t in source)
            {
                sum += t;
            }
            return sum;
        }
        public static int MyCount<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int count = 0;

            foreach (T t in source)
            {
                count++;
            }

            return count;
        }
        public static float MyAverage<T>(this IEnumerable<T> source) where T : struct, INumber<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int count = 0;
            T sum = T.Zero;

            foreach (T t in source)
            {
                count++;
                sum += t;
            }

            return Convert.ToSingle(sum) / count;
        }
        public static IEnumerable<T> MyOrderBy<T>(this IEnumerable<T> source, Func<T, T> function) where T : struct, INumber<T>
        {
            // 개선이 많이 필요한 매서드인듯
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            List<T> resultList = new List<T>();

            foreach (T t in source)
            {
                T shiftT = function(t);
                bool checkBool = false;

                for (int i = 0; i < resultList.Count; i++)
                {
                    if (shiftT <= function(resultList[i]))
                    {
                        resultList.Insert(i, t);
                        checkBool = true;
                        break;
                    }
                }

                if (!checkBool)
                {
                    resultList.Add(t);
                }
            }
            return resultList;
        }
        public static T MyMax<T>(this IEnumerable<T> source) where T : struct, IComparable<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            T resultT = source.First();

            foreach (T t in source)
            {
                if (resultT.CompareTo(t) < 0)
                {
                    resultT = t;
                }
            }

            return resultT;
        }
        public static T MyNear<T>(this IEnumerable<T> source, T pivotValue) where T : struct, INumber<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            T returnT = source.First();
            T distance = returnT - pivotValue;
            T absDistance = T.Abs(distance);

            foreach (T t in source)
            {
                distance = t - pivotValue;
                T absNewDistance = T.Abs(distance);
                if (absDistance > absNewDistance)
                {
                    returnT = t;
                    absDistance = absNewDistance;
                }
            }

            return returnT;
        }
        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> source1, IEnumerable<T> source2) where T : struct
        {
            foreach (T t in source1)
            {
                yield return t;
            }
            foreach (T t in source2)
            {
                yield return t;
            }
        }
        public static IEnumerable<IGrouping<TCoin, TSource>> MyGroupBy<TCoin, TSource>(this IEnumerable<TSource> source, Func<TSource, TCoin> function)
        {
            #region 공부정리
            /*
            ○ IGrouping
             - 매서드의 리턴값이 IENumerable<IGrouping<Tkey, TSource> 인데, 이렇게 사용하면 리턴값은 TKey는 Dictionary의 Key와 같은 역할이고, TSource는 IEnumerable<TSource>형식으로 리턴함
             - Dictionary는 C 초창기?에 나온 컬렉션이고, IENumerable<IGrouping.. 은 System.Collections 에서 정의됨
             - 매서드의 리턴값이 IENumerable<IGrouping.. 일시, 매서드 내 yield return new ClasA 형식으로 리턴해야하며, ClassA는 Interface IGrouping 을 상속한 클래스여야 함
            */
            #endregion
            Dictionary<TCoin, List<TSource>> dictionary = new Dictionary<TCoin, List<TSource>>();

            foreach (TSource t in source)
            {
                TCoin tCoin = function(t);
                if (!dictionary.ContainsKey(tCoin))
                {
                    dictionary[tCoin] = new List<TSource>();
                }
                dictionary[tCoin].Add(t);
            }

            foreach (var kvp in dictionary)
            {
                yield return new MyGrouping<TCoin, TSource>(kvp.Key, kvp.Value);
            }
        }
    }

}
