using System;
using System.Linq;
using System.Numerics;
using System.Collections;

namespace My
{
    public class MyGrouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        // 클래스에 인터페이스 상속 및 클래스 내에 public ClasName으로 변수명 다시 정의하는 것은 추후 공부할 것 같으므로, 이 내용은 공부 생략함
        public TKey Key { get; }
        public IEnumerable<TElement> Elements { get; }

        public MyGrouping(TKey key, IEnumerable<TElement> elements)
        {
            Key = key;
            Elements = elements;
        }
        public IEnumerator<TElement> GetEnumerator() => Elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        // 위 매서드가 무슨 역할인지는 모르겠고.. 일단 IGrouping을 구현하기 위해, 매서드를 상속해야 해서 입력함
    }

    public class SortMe
    {
        /// <summary>
        /// 두 실수의 최대공약수를 반환하는 함수
        /// </summary>
        /// <typeparam name="T">IComparable, IConvertible을 만족하는 실수</typeparam>
        /// <param name="firstInt">첫번째 비교인자</param>
        /// <param name="secondInt">두번째 비교인자</param>
        /// <returns>최대공약수를 반환</returns>
        static T GetGCD<T>(T firstInt, T secondInt) where T : struct, INumber<T>
        {
            T zero = T.Zero;
            if(firstInt < zero)
            {
                firstInt = -firstInt;
            }
            if(secondInt < zero)
            {
                secondInt = -secondInt;
            }

            if(firstInt < secondInt)
            {
                (firstInt, secondInt) = (secondInt, firstInt);
            }


            if(secondInt == zero)
            {
                return firstInt;
            }

            T remainder;

            do
            {
                remainder = firstInt % secondInt;
                firstInt = secondInt;
                secondInt = remainder;
            } while (remainder != zero);

            return firstInt;
            #region Reference
            /* 참조
             * pri.ba.0 - 유클리드 호제법 (Euclidian Algorithm)
             * 유클리드 호제법을 사용하여, A = 몫 * B + R; A = B; B = R; 을 반복하여, R = 0이 될 때에 A = 몫 & B + R 에서의 B가 최대공약수  
             * BigInter.GreatestCommonDivosor(BigInter, BigInter) 함수를 참조하여 만듬 - a b 가 음수일 시 양으로 변환. a == 0 || a == b 일시 Max(a,b) 처리
             */
            #endregion
        }
        /// <summary>
        /// 두 실수의 최소공배수를 반환하는 함수
        /// </summary>
        /// <typeparam name="T">struct, IComparable, IConvertible을 만족하는 실수</typeparam>
        /// <param name="firstInt">첫버째 인수</param>
        /// <param name="secondInt">두번째 인수</param>
        /// <returns>두 실수의 최소공배수를 반환</returns>
        static T GetLCM<T>(T firstInt, T secondInt) where T : struct, INumber<T>
        {
            T returT = firstInt / GetGCD(firstInt, secondInt) * secondInt;
            if(returT < T.Zero)
            {
                returT = -returT;
            }
            return returT;
            #region Reference
            /*
             * 참조 - pri-ba-0.유클리드 호제법 - EuclidianAlgorithm
             * 참조 - pri-ba-1. GCD - LCM 관계 
             * LCM 을 구하는 기본 제공 매서드는 없다함. abs( a * b ) / BiggerInt.GreatestCommonDivisor(a,b); 로 구한다함
             * 그래서 한 수가 0이면, LCM은 0으로 처리하는듯
             * */
            #endregion
        }
        /// <summary>
        /// 최솟값을 반환하는 함수
        /// </summary>
        /// <typeparam name="T">struct, IComparable<T>를 만족하는 실수</T></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>최솟값을 반환</returns>
        static T Min<T>(T first, T second) where T : struct, IComparable<T> // where 우측에 <T> 를 입력하면(Generic Inteface), 1) T타입과 동일한 타입의 객체와만 비교하도록 강제하며, 2) 컴파일러가 타입을 미리 알기 때문에 성능 면에서 유리. 3) 잘못된 타입과 비교하는 실수 방지
        {
            return first.CompareTo(second) <= 0 ? first : second;
            // A.CompareTo(B) : 1) A > B : 1반환, 2) A == B : 0반환, 3) A < B : -1 반환
        }
        /// <summary>
        /// 최대값을 반환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>최대값을 반환</returns>
        static T Max<T>(T first, T second) where T : struct, IComparable<T>
        {
            return first.CompareTo(second) >= 0 ? first : second;
        }
        /// <summary>
        /// 절대값을 반환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        static T Abs<T>(T value) where T : struct, INumber<T>
        {
            return value < T.Zero ? -value : value;
        }
        public static void MySort_SimpleComparisionSort<T>(T[] source, Func<T, T, int> function) where T : IComparable<T>
        {
            #region 공부정리
            /*
            □
            이 매서드처럼(원매서드 : Array.Sort 인 정적매서드), 매개변수가 참조형식인 클래스인 경우에, 해당 매개변수를 수정하면, 그 매개변수 값이 바뀐다.
            이를 활용하여, 정적매서드를 호출하는 매서드가 외부 클래스에 위치하더라도, 정적매서드로 변경된 클래스는 영구적으로 매서드를 호출한 외부클래스에서도 매개변수 클래스 내의 값들이 변경된다.
            => 이를 활용하면, EnemyMovement 클래스를 여러 계층으로 상속 처리하는 위험을 감수하지 않더라도, void를 반환하는 정적매서드에서 struct 이 아닌 class를 변경하는 것으로, 개별 EnemyMovement 클래스를 간소화하는 것이 가능할듯
            □
            이 정렬법은 단순하지만, 연산효율이 좋지는 않은 연산법
             */
            #endregion
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            for (int i = 0; i < source.Length; i++)
            {
                for (int j = i + 1; j < source.Length; j++)
                {
                    if (function(source[i], source[j]) > 0)
                    {
                        (source[i], source[j]) = (source[j], source[i]);
                    }
                }
            }
        }

        #region QuickSortMethods
        public static void MySort_QuickSort<T>(T[] source, Func<T, T, int> function) where T : IComparable<T>
        {
            #region 공부정리
            /*
            □ Sort 연산법 중 하나인 QuickSort
             - 우선 임의의 피벗 요소 [코드에서는 파티션에서 가장 우측에 위치한 요소-for(..)처리하기 편하니] 를 고르고, 그 값보다 작으면 좌측, 크면 우측, 이 가운데에 피벗 요소를 배치
             - 이렇게 나뉘 왼쪽, 오른쪽 파티션에 동일한 작업을 반복하여 정렬
            */
            #endregion
            QuickSort(source, 0, source.Length - 1, function);

        }
        public static void QuickSort<T>(T[] array, int left, int right, Func<T, T, int> function) where T : IComparable<T>
        {
            if (left < right)
            {
                int pivotIndex = QuickSort_Partition(array, left, right, function);
                QuickSort(array, left, pivotIndex - 1, function);
                QuickSort(array, pivotIndex + 1, right, function);
            }
        }
        public static int QuickSort_Partition<T>(T[] array, int left, int right, Func<T, T, int> function) where T : IComparable<T>
        {
            T pivot = array[right];

            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (function(array[j], pivot) <= 0)
                {
                    i++;
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }

            (array[i + 1], array[right]) = (array[right], array[i + 1]);

            return i + 1;
        }
        #endregion

        #region Sort_HeapSort
        public static void Sort_HeapSort<T>(T[] array, Func<T, T, int> function) where T : struct, IComparable<T>
        {
            #region 공부정리
            /*
            ■ Heap정렬법
            □ 작동원리
            ○ 기본 구조
             - Heap정렬은 index 0 이 자식 index 1, index 2를 두고, index 1 는 index 3, 4를, index 2 는 index 5,6을 자식으로 둔다. 즉 0으로 시작한, 2개의 자식을 두는 구조가 무한히 반복된다.
             - 이렇게 자식을 둔다 하면, index n의 자식의 index값은, 2*n + 1, 2*n+2 가 된다. (이것 관련 수리증명은 아직 못했지만, index를 쭉 나열해보면, 확인은 가능..)
            ○ Heapify
             - 위의 구조에서, Hepaify(index: n)을 한다는 것은, index n과, n의 자식 2*n+1, 2*n+2중 가장 큰 값을 n의 위치와 스왑한다.
             - 스왑이 이뤄질시(기존부모n이 자식 중 하나보다 작았을시), 스왑된 자식의 기존 위치에서(2*n+1 또는 2*n+2의 위치), 다시 재귀적으로 Heapify를 시행한다
            ○ 작동 전개과정
             1) index 0 부터 n / 2 - 1 [ (n/2-1) * 2 + 2 = n이므로] 까지의 index를 대상으로, n/2-1 부터, 0까지 내려오며, 순차적으로 재귀적 Heapify를 시행한다 [이 과정을 Building Max Heap 이라 하는듯]
             2) Building Max Heap 처리후, index 0과, index 최댓값을 스왑한다. 이후, index 최댓값을 Heapify 대상에서 제외하고, 재귀적 Heapify(index 0) 를 시행한다 -> 이 과정을 Heapify대상이 없어지는, 1 아래 코드의 length 가 1이 될 때까지 반복 시행한다
            */
            #endregion
            int length = array.Length;

            for (int i = length / 2 - 1; i >= 0; i--)
            {
                HeapSort_Heapify(array, length, i, function);
            }

            for (int i = length - 1; i > 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                HeapSort_Heapify(array, i, 0, function);
            }
        }
        private static void HeapSort_Heapify<T>(T[] array, int length, int parentIndex, Func<T, T, int> function) where T : struct, IComparable<T>
        {
            int largest = parentIndex;
            int leftChildIndex = parentIndex * 2 + 1;
            int rightChidIndex = parentIndex * 2 + 2;

            if (leftChildIndex < length && function(array[largest], array[leftChildIndex]) < 0)
            {
                largest = leftChildIndex;
            }
            if (rightChidIndex < length && function(array[largest], array[rightChidIndex]) < 0)
            {
                largest = rightChidIndex;
            }

            if (largest != parentIndex)
            {
                (array[parentIndex], array[largest]) = (array[largest], array[parentIndex]);
                HeapSort_Heapify(array, length, largest, function);
            }
        }
        #endregion

        static int MyBinarySearch<T>(T[] array, T searchingValue) where T : struct, IComparable<T>
        {
            int left = 0;
            int right = array.Length - 1;

            while (right - left > 1)
            {
                int averageIndex = left + (right - left) / 2; // OverFlowPrevention;
                int compareInt = searchingValue.CompareTo(array[averageIndex]);
                if (compareInt == 0)
                {
                    return averageIndex;
                }
                else if (compareInt > 0)
                {
                    left = averageIndex + 1;
                }
                else
                {
                    right = averageIndex - 1;
                }
            }
            return -1;
        }
    }
}