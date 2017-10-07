using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class Graph
    {
		//210. Course Schedule II
		//這題是寫到目前為止，讓我最傷心的一題
		public int[] FindOrder(int numCourses, int[,] prerequisites)
		{
			//先建成DAG
			//有關 graph 的資料結構，學弟有大作可以參考
			//http://www.csie.ntnu.edu.tw/~u91029/Graph.html#1
			//這裡就是建那個 adjacency lists
			//degree 又有分 in-degree and out-degree，這裡是用 in-degree
			//目的是用來判定，這個節點的所有上遊節點，是否都被處理過了

			List<int>[] graph = new List<int>[numCourses];
			int[] indegree = new int[numCourses];
			for (int i = 0; i < prerequisites.GetLength(0); i++)
			{
				int needer = prerequisites[i, 0];
				int beneed = prerequisites[i, 1];

				if (graph[beneed] == null)
				{
					graph[beneed] = new List<int>();
				}
				graph[beneed].Add(needer);

				//需求別人，數量加1
				indegree[needer]++;
			}

			//這裡就類似 BFS ，每次都只處理 需求量為 0 的元素，因為它沒有被需求，可以輸出
			//如果資料有循環，則循環區的所有 indegree 都是會大於0，因為每個元素都被需要
			//你會找不到一個開口去把它們的 indegree 減一，造成新的點不會進 zeroNex
			//只要沒辦法正常的跑完 numCourses 數量，就是不對
			//這種寫法，可以避開無窮迴圈的問題
			//我有實作另一篇討論的 BFS 作法，結果就卡在無窮迴圈不知如何處理，它的 visited 永遠空不了，所以它的輸出會重覆出現，所以它要加 if 過濾
			//https://discuss.leetcode.com/topic/13873/two-ac-solution-in-java-using-bfs-and-dfs-with-explanation

			List<int> result = new List<int>();
			Queue<int> zeroNext = new Queue<int>();
			for (int i = 0; i < indegree.Count(); i++)
			{
				//從所有的 0 開始 
				if (indegree[i] == 0)
				{
					zeroNext.Enqueue(i);
				}
			}

			//正常的輸出，答案一定剛好等於 numCourses 的數量，多一少一都有問題
			for (int i = 0; i < numCourses; i++)
			{

				if (zeroNext.Count() == 0)
				{
					return new int[] { };
				}

				//每次取出就輸出
				int curr = zeroNext.Dequeue();
				result.Add(curr);

				if (graph[curr] != null)
				{
					foreach (var child in graph[curr])
					{
						indegree[child]--;

						//只要 0，表示它不再被別人需要，輸出它
						if (indegree[child] == 0)
						{
							zeroNext.Enqueue(child);
						}
					}
				}
			}

			return result.ToArray();
		}
    }
}
