using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class TreeOP
    {
        //DFS 算法 遞迴巡訪樹，三種序
		private static void BST_DFS(TreeNode root, List<int> bTree)
		{
			if (root == null)
			{
				return;
			}
			else
			{
				
				//bTree.Add(root.val);          //PreOrder
				BST_DFS(root.left, bTree);
                bTree.Add(root.val);          //InOrder
				BST_DFS(root.right, bTree);
                //bTree.Add(root.val);          //PosOrder 
			}
		}

		//101. Symmetric Tree
		private bool isMirror(TreeNode left, TreeNode right)
		{
			if (left == null && right == null)
			{
				return true;
			}

			if (left == null || right == null)
			{
				return false;
			}

			return (left.val == right.val) &&
				   isMirror(left.left, right.right) &&
				   isMirror(left.right, right.left);
		}

		public bool IsSymmetric(TreeNode root)
		{
			return isMirror(root, root);
		}



		//104. Maximum Depth of Binary Tree
		private int maxDeep = 0;

		private void DFS(TreeNode node, int deep)
		{
			if (node == null)
			{
				return;
			}

			deep++;
			maxDeep = Math.Max(maxDeep, deep);
			DFS(node.left, deep);
			DFS(node.right, deep);
		}

		public int MaxDepth(TreeNode root)
		{
			DFS(root, 0);
			return maxDeep;
		}

		//108. Convert Sorted Array to Binary Search Tree
		public class Span
		{
			public Span(int beg, int end, TreeNode papa, bool isleft)
			{
				Papa = papa;
				Beg = beg;
				End = end;
				IsLeft = isleft;
			}

			public TreeNode Papa;
			public int Beg;
			public int End;
			public bool IsLeft;
		}

		public TreeNode SortedArrayToBST(int[] nums)
		{
			Queue<Span> queue = new Queue<Span>();
			TreeNode root = null;

			queue.Enqueue(new Span(0, nums.Length - 1, root, true));

			while (queue.Count() > 0)
			{
				Span s = queue.Dequeue();

				if (s.Beg <= s.End)
				{
					int mid = (s.Beg + s.End) / 2;
					TreeNode n = new TreeNode(nums[mid]);

					if (s.Papa == null)
					{
						root = n;
					}
					else
					{
						if (s.IsLeft)
						{
							s.Papa.left = n;
						}
						else
						{
							s.Papa.right = n;
						}
					}

					queue.Enqueue(new Span(s.Beg, mid - 1, n, true));
					queue.Enqueue(new Span(mid + 1, s.End, n, false));
				}
				else
				{
					//do nothing...
				}
			}

			return root;
		}

		//94. Binary Tree Inorder Traversal
		//這是學校題了，只是雄雄看到 inorder traversal 時呆了
		//除了 DFS BFS 外，DFS 還 preorder inorder postorder 三種 lay 法
		public IList<int> InorderTraversal(TreeNode root)
		{
			List<int> result = new List<int>();
			recInorder(root, result);
			return result;
		}

		private void recInorder(TreeNode root, List<int> result)
		{
			if (root == null)
			{
				return;
			}
			else
			{
				recInorder(root.left, result);
				result.Add(root.val);
				recInorder(root.right, result);
			}
		}

		//98. Validate Binary Search Tree
		//這題花了一個多小時，真的是笨了，上面都學到三種lay法了，不會拿來lay一次就知了
		//別人有寫一個把最大最小值傳入遞回的，很厲害，在每次進子樹時動態的決定要新的邊界，但總是保留一邊
		public bool IsValidBST(TreeNode root)
		{
			List<int> bTree = new List<int>();
			recIsValidBST(root, bTree);
			for (int i = 0; i < bTree.Count() - 1; i++)
			{
				if (bTree[i] >= bTree[i + 1])
				{
					return false;
				}
			}

			return true;
		}

		private void recIsValidBST(TreeNode root, List<int> bTree)
		{
			if (root == null)
			{
				return;
			}
			else
			{
				recIsValidBST(root.left, bTree);
				bTree.Add(root.val);
				recIsValidBST(root.right, bTree);
			}
		}

		//102. Binary Tree Level Order Traversal
		//很好，這題沒卡到，標準 BFS 先前有學過了，用 queue 的概念作
		public IList<IList<int>> LevelOrder(TreeNode root)
		{
			List<IList<int>> result = new List<IList<int>>();
			List<TreeNode> que = new List<TreeNode>();
			que.Add(root);

			while (que.Count() > 0)
			{
				List<TreeNode> ls = new List<TreeNode>();
				List<int> tempResult = new List<int>();
				foreach (var q in que)
				{
					if (q != null)
					{
						tempResult.Add(q.val);
						ls.Add(q.left);
						ls.Add(q.right);
					}
				}

				que = ls;
				if (tempResult.Count() > 0)
				{
					result.Add(tempResult);
				}
			}

			return result;
		}

		//103. Binary Tree Zigzag Level Order Traversal
		//這題只是上題加點變化，個人覺得無聊…不知道有啥用途
		public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
		{
			List<IList<int>> result = new List<IList<int>>();
			List<TreeNode> que = new List<TreeNode>();
			que.Add(root);

			int loop = 1;
			while (que.Count() > 0)
			{
				List<TreeNode> ls = new List<TreeNode>();
				List<int> tempResult = new List<int>();
				foreach (var q in que)
				{
					if (q != null)
					{
						tempResult.Add(q.val);
						ls.Add(q.left);
						ls.Add(q.right);
					}
				}

				que = ls;
				if (tempResult.Count() > 0)
				{
					if (loop % 2 == 0)
					{
						tempResult.Reverse();
					}
					result.Add(tempResult);
				}
				loop++;
			}

			return result;
		}
		//105. Construct Binary Tree from Preorder and Inorder Traversal
        //三種列序，只要能握有任意兩個，就可以重建二元樹
		public TreeNode BuildTree(int[] preorder, int[] inorder)
		{
			/* 先記一下測試，不太好找出來 XD
            [1,2,4,5,3,6,7]
            [4,2,5,1,6,3,7]
            */

			//沒辦法算的資料
			if (preorder.Length == 0 || inorder.Length == 0)
			{
				return null;
			}

			//real root value is at the first of preorder
			TreeNode root = new TreeNode(preorder[0]);

			int mid = -1;
			for (int i = 0; i < inorder.Length; i++)
			{
				if (inorder[i] == root.val)
				{
					mid = i;
				}
			}

			//這裡 mid 一定要有值才合理

			//利用 mid 切開 陣列

			int[] leftInord = new int[mid];
			Array.Copy(inorder, 0, leftInord, 0, leftInord.Length);

			int[] rightInord = new int[inorder.Length - 1 - mid];
			Array.Copy(inorder, mid + 1, rightInord, 0, rightInord.Length);

			int[] leftPreord = new int[leftInord.Length];
			Array.Copy(preorder, 1, leftPreord, 0, leftPreord.Length);

			int[] rightPreord = new int[rightInord.Length];
			Array.Copy(preorder, leftPreord.Length + 1, rightPreord, 0, rightPreord.Length);

			root.left = BuildTree(leftPreord, leftInord);
			root.right = BuildTree(rightPreord, rightInord);

			return root;
		}

		//208. Implement Trie (Prefix Tree) 
		//特里樹，專門處理 prefix 同的快速比較
		//https://leetcode.com/problems/implement-trie-prefix-tree/solution/
		//這題我是寫完一次過，可是我完全不知道怎麼測啊!!!! 那個輸入我怎麼改，不是 runtime error 就是 null false
		//從來沒看過 true ....Orz
		public class Trie
		{
			private TrieNode root = new TrieNode();

			public Trie()
			{

			}

			public void Insert(string word)
			{
				if (word.Length > 0)
				{
					TrieNode curr = root;
					foreach (char c in word)
					{
						curr = curr.Put(c);
					}
					curr.IsEnd = true;
				}
			}

			private bool startsWith(string prefix, out TrieNode last)
			{
				TrieNode curr = root;

				foreach (var c in prefix)
				{
					curr = curr.Get(c);
					if (curr == null)
					{
						last = null;
						return false;
					}
				}

				last = curr;
				return true;
			}

			public bool Search(string word)
			{
				if (word.Length > 0)
				{
					TrieNode temp;
					bool res = startsWith(word, out temp);
					return res && temp.IsEnd;
				}
				else
				{
					return true;
				}
			}

			public bool StartsWith(string prefix)
			{
				if (prefix.Length > 0)
				{
					TrieNode temp;
					bool res = startsWith(prefix, out temp);
					return res;
				}
				else
				{
					return true;
				}
			}
		}

		public class TrieNode
		{
			private const int bound = 26;

			private TrieNode[] nextNode = new TrieNode[bound];

			public bool IsEnd { get; set; }

			public TrieNode Put(char c)
			{
				if (nextNode[c - 'a'] == null)
				{
					nextNode[c - 'a'] = new TrieNode();
				}

				return nextNode[c - 'a'];
			}

			public TrieNode Get(char c)
			{
				if (nextNode[c - 'a'] != null)
				{
					return nextNode[c - 'a'];
				}
				else
				{
					return null;
				}
			}

		}

		//230. Kth Smallest Element in a BST
		//這題很容題，就看會不會寫 DFS 加上 BST 在 inorder 下是由小到大，只要知道這個組合簡單
		public int KthSmallest(TreeNode root, int k)
		{
			currTh = 0;
			currValue = 0;
			recKth(root, k);
			return currValue;
		}

		int currTh = 0;
		int currValue = 0;
		private void recKth(TreeNode root, int k)
		{
			if (root == null)
			{
				return;
			}
			else
			{
				recKth(root.left, k);

				if (currTh < k)
				{
					currValue = root.val;
					currTh++;
				}
				else
				{
					return;
				}

				recKth(root.right, k);
			}
		}

		//236. Lowest Common Ancestor of a Binary Tree
		//這題線上沒得測，用 DFS 遞迴寫應該不難，送上去後發現測大型的 case 又 Runtime error
		//所以猜測又是遞迴到死人… 想改用 BFS 但應該沒辦法解
		//結果看了別人的答案，還是用 DFS 遞迴解，只是更精簡
		//這裡要注意它為何就是把找到外傳就可以，因為，在 DFS 中如果你能找到，表示你已經到了最底退回來
		//所以你就是最低的那個答案，其它呼叫都只是回傳這個結果，我原本的寫法是回找到了幾個，答案也是對的
		//但就需要有個 lca 去記得第一次找到的對像，但我不清楚，為何這樣的遞迴在它測超長單邊樹時會死
		public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
		{
			if (root == null || root == p || root == q)
			{
				return root;
			}
			else
			{
				TreeNode left = LowestCommonAncestor(root.left, p, q);
				TreeNode right = LowestCommonAncestor(root.right, p, q);

				if (left != null && right != null)
				{
					return root;
				}
				else if (left != null)
				{
					return left;
				}
				else if (right != null)
				{
					return right;
				}
				else
				{
					return null;
				}
			}
		}

		//private int recLCA(TreeNode root, TreeNode p, TreeNode q, ref TreeNode lca)
		//{
		//    if (root == null || lca != null)
		//    {
		//        return 0;
		//    }
		//    else
		//    {
		//        int me = root == p || root == q ? 1 : 0;
		//        int left = recLCA(root.left, p, q, ref lca);
		//        int right = recLCA(root.right, p, q, ref lca);

		//        if (me + left + right == 2 && me != 2 && left != 2 && right != 2)
		//        {
		//            lca = root;
		//        }

		//        return me + left + right;
		//    }
		//}


	}
}
