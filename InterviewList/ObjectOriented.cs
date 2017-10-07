using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class ObjectOriented
    {
		//341. Flatten Nested List Iterator
		//這題一開始會看到暈暈的，因為它就是一層包一層的無限可能，然後名字也很鬼
		//它給了你 NestedInteger 的定義，然後叫你實作它的 Iterator，但名字很像就看的暈
		//我一開始就笨笨的在記讀到第幾個，然後就發現沒辦法，無限層是怎麼記，我只能實作最外圍的 Iterater
		//又不能叫那個 Integer 有方法可以讓我遞迴叫 next...
		//後來才想到，我為何不一開始就全面遞迴一次，把所有可以直接轉的值換一次不就好了，再來就只是一直拿
		//然後它的輸出又是 DFS ，所以只要搞個集合記下來依序輸出就好，這題來說 Queue 剛好
		public interface NestedInteger
		{
			bool IsInteger();
			int GetInteger();
			IList<NestedInteger> GetList();
		}

		public class NestedIterator
		{
			public NestedIterator(IList<NestedInteger> nestedList)
			{
				recProcList(nestedList);
			}

			private Queue<NestedInteger> allInteger = new Queue<NestedInteger>();

			//使用 DFS 分解所有 NestedInteger,留下可以直接變成 int 的
			private void recProcList(IList<NestedInteger> nestedList)
			{
				foreach (var nl in nestedList)
				{
					if (nl == null)
					{
						//do nothing....
					}
					else if (nl.IsInteger())
					{
						allInteger.Enqueue(nl);
					}
					else
					{
						recProcList(nl.GetList());
					}
				}
			}

			public bool HasNext()
			{
				return allInteger.Count() > 0;
			}

			public int Next()
			{
				return allInteger.Dequeue().GetInteger();
			}
		}

		//380. Insert Delete GetRandom O(1)
		//這也不是我的功勞，是 hashtable 的強大
		//我只是接口，另外，ElementAt 是linq提供的，我不確定它是 O(1)
		//如果集合對像有實作 IList，就會靠 index 作到 O(1)，但我看 HashSet 是沒有
		//所以多數的解答都會靠多一個 List 來解決
		public class RandomizedSet
		{
			private HashSet<int> data;
			Random rand;

			public RandomizedSet()
			{
				data = new HashSet<int>();
				rand = new Random(Environment.TickCount);
			}

			public bool Insert(int val)
			{
				if (data.Contains(val))
				{
					return false;
				}
				else
				{
					data.Add(val);
					return true;
				}
			}

			public bool Remove(int val)
			{
				if (data.Contains(val))
				{
					data.Remove(val);
					return true;
				}
				return false;
			}

			public int GetRandom()
			{
				int ind = rand.Next() % data.Count();
				return data.ElementAt(ind);
			}
		}

		//384. Shuffle an Array
		public class Solution
		{
			private int[] original;
			private int[] currNums;
			//這個 random 寫在這裡很重要我現在才理解
			//如果我們用同一個 tick 去建 random ，那它拿出來的數列是一樣的
			//所以如果每次都重建，則要保證 tick 不同，但是，我沒辦法保證我不會被狂連呼!!
			//所以這個 random 只能建一次，這樣至少保證這個物件自己是 random 有效
			//但如果有人狂建這種物件，那… 很可能有一堆人是用同一個 tick，那就死定了…
			Random rand = new Random(Environment.TickCount);

			public Solution(int[] nums)
			{
				original = nums;
				currNums = new int[nums.Length];
				Array.Copy(nums, currNums, nums.Length);
			}

			public int[] Reset()
			{
				Array.Copy(original, currNums, original.Length);
				return currNums;
			}

			public int[] Shuffle()
			{
				for (int i = 0; i < currNums.Length; i++)
				{
					//random a position to swap eachother
					int ind = rand.Next() % currNums.Length;
					int temp = currNums[i];
					currNums[i] = currNums[ind];
					currNums[ind] = temp;
				}
				return currNums;
			}
		}
    }
}
