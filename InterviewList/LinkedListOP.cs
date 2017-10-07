using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class LinkedListOP
    {
		//21. Merge Two Sorted Lists
		public ListNode MergeTwoLists(ListNode l1, ListNode l2)
		{

			ListNode head = null;
			ListNode tail = null;
			ListNode toL1 = l1;
			ListNode toL2 = l2;

			while (toL1 != null || toL2 != null)
			{
				ListNode curr;

				if (toL1 != null && toL2 != null)
				{
					if (toL1.val < toL2.val)
					{
						curr = toL1;
						toL1 = toL1.next;
					}
					else
					{
						curr = toL2;
						toL2 = toL2.next;
					}

				}
				else if (toL1 != null)
				{
					curr = toL1;
					toL1 = toL1.next;
				}
				else
				{
					curr = toL2;
					toL2 = toL2.next;
				}

				curr.next = null;

				if (head == null)
				{
					head = curr;
					tail = curr;
				}
				else
				{
					tail.next = curr;
					tail = curr;
				}
			}

			return head;
		}

		//141. Linked List Cycle
		public bool HasCycle(ListNode head)
		{
			/*
             * 我也是傳統解，解答有一個很聰明的解，用兩隻指標，一個一次跑兩步，一個一次跑一步
             * 如果能追上，表示有迴圈，不然，就一定可以跑到 null，很強大，不用花空間 
            */

			if (head == null)
			{
				return false;
			}

			HashSet<ListNode> hs = new HashSet<ListNode>();

			while (head.next != null)
			{
				if (hs.Contains(head))
				{
					return true;
				}
				else
				{
					hs.Add(head);
					head = head.next;
				}
			}

			return false;
		}

		//160. Intersection of Two Linked Lists
		public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
		{
			/*
             * 我還是採標準版，我認為 O(1) space 的解法有點變態了
             * 而且我還是看沒有為何一定會在交叉點相遇，不會是在交叉區的中間嗎?? 不太好想像，就不想了... 
            */
			HashSet<ListNode> hs = new HashSet<ListNode>();
			while (headA != null)
			{
				hs.Add(headA);
				headA = headA.next;
			}

			while (headB != null)
			{
				if (hs.Contains(headB))
				{
					return headB;
				}
				else
				{
					hs.Add(headB);
					headB = headB.next;
				}
			}

			return null;
		}

		//206. Reverse Linked List
		public ListNode ReverseList(ListNode head)
		{
			if (head == null)
			{
				return head;
			}

			ListNode tempHead = head;
			ListNode tempNext = head.next;
			head.next = null;

			while (tempNext != null)
			{
				ListNode temp = tempNext.next;
				tempNext.next = tempHead;
				tempHead = tempNext;
				tempNext = temp;
			}

			return tempHead;
		}

		//234. Palindrome Linked List
		public bool IsPalindrome(ListNode head)
		{
			//找到中間點前後
			int count = 0;
			ListNode temp = head;
			while (temp != null)
			{
				count++;
				temp = temp.next;
			}

			if (count < 2)
			{
				return true;
			}

			int tar = count % 2 == 0 ? count / 2 + 1 : count / 2 + 2;

			temp = head;
			while (tar > 1)
			{
				temp = temp.next;
				tar--;
			}

			//反轉後半
			temp = ReverseList(temp);

			//比對前半與後半所有 node
			while (temp != null)
			{
				if (temp.val != head.val)
				{
					return false;
				}
				temp = temp.next;
				head = head.next;
			}

			return true;
		}

		//237. Delete Node in a Linked List
		//這題，反而讓我沒想到，不常寫 Linked List 的人應該都不昜想到
		//如果該 node 是一個複雜物件，含蓋多層指標，那我認為這樣作也是危險的
		//物件的 clone 不是一件簡單的事
		public void DeleteNode(ListNode node)
		{
			node.val = node.next.val;
			node.next = node.next.next;
		}

		//2. Add Two Numbers
		public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
		{
			if (l1 == null && l2 == null)
			{
				return null;
			}

			ListNode head = new ListNode(-1);
			ListNode curr = head;

			bool isCarry = false;
			while (l1 != null || l2 != null)
			{
				int v1 = 0;
				int v2 = 0;
				int sum = isCarry ? 1 : 0;

				if (l1 != null)
				{
					v1 = l1.val;
					l1 = l1.next;
				}

				if (l2 != null)
				{
					v2 = l2.val;
					l2 = l2.next;
				}

				sum += (v1 + v2);
				isCarry = sum >= 10;
				sum = sum % 10;

				ListNode node = new ListNode(sum);
				curr.next = node;
				curr = node;
			}

			if (isCarry)
			{
				curr.next = new ListNode(1);
			}

			return head.next;
		}

		//19. Remove Nth Node From End of List
		public ListNode RemoveNthFromEnd(ListNode head, int n)
		{
			ListNode curr = head;
			ListNode pre = head;
			ListNode last = head;
			int count = 1;

			//用兩隻指標指向快和慢，慢就是要刪除的那個
			while (curr.next != null)
			{
				pre = curr;
				curr = curr.next;

				count++;

				if (count > n)
				{
					last = last.next;
				}
			}

			//刪的時候，有分刪頭，刪尾，刪中間，這是要有不同寫法的，刪尾要有前一才有辦法刪
			//刪中可以用分身複制作弊
			if (last == head)
			{
				//remove head; 
				return head.next;
			}
			else if (last.next == null)
			{
				//remove tail
				pre.next = null;
			}
			else
			{
				//remove middle
				last.val = last.next.val;
				last.next = last.next.next;
			}

			return head;
		}

		//138. Copy List with Random Pointer
		//這題也沒辦法除錯，只能硬幹上傳，結果兩次就過了
		//只有忘記加上 random 指標可能是 null 就不要去查字典了的這行檢查 
		public RandomListNode CopyRandomList(RandomListNode head)
		{
			RandomListNode resultHead = null;
			Dictionary<RandomListNode, RandomListNode> map = new Dictionary<RandomListNode, RandomListNode>();
			RandomListNode src = head;
			RandomListNode curr = null;
			while (src != null)
			{
				RandomListNode temp = new RandomListNode(src.label);
				if (curr != null)
				{
					curr.next = temp;
				}
				curr = temp;

				if (resultHead == null)
				{
					resultHead = curr;
				}

				map.Add(src, curr);

				src = src.next;
			}

			src = head;
			curr = resultHead;
			while (src != null)
			{
				if (src.random != null)
				{
					curr.random = map[src.random];
				}

				src = src.next;
				curr = curr.next;
			}

			return resultHead;
		}

		//148. Sort List
		//這題雖然一開始就有想到應該是用 merge sort，但沒想到麻煩真多
		//而且 merge sort 的 nLogn 其中的 log 來自每次都把手上的集合對半分
		//這件事沒作就沒有義意，我原本只有把頭取下，這樣分一點用都沒有，會遞迴到爆
		//這題又麻煩在它是 linked list，所以切分與next指標的設置，檢查都有狀況
		public ListNode SortList(ListNode head)
		{
			if (head == null || head.next == null)
			{
				return head;
			}
			ListNode tmph;
			cutIntoTwo(head, out head, out tmph);
			return recMergeSort(head, tmph);
		}

		private void cutIntoTwo(ListNode head, out ListNode left, out ListNode right)
		{
			if (head != null && head.next != null)
			{
				left = head;
				right = head.next;
				while (right != null && right.next != null)
				{
					right = right.next.next;
					left = left.next;
				}

				right = left.next;
				left.next = null;
				left = head;
			}
			else
			{
				left = null;
				right = null;
			}
		}

		private ListNode recMergeSort(ListNode left, ListNode right)
		{
			//資料不夠小，就對半分，兩半再 merge
			//所以，一定是資料切到夠小了才 merge
			//然後開始收斂，一路 merge起來，直到又成為一條

			if (left.next != null)
			{
				ListNode tmpl;
				cutIntoTwo(left, out left, out tmpl);
				left = recMergeSort(left, tmpl);
			}

			if (right.next != null)
			{
				ListNode tmpr;
				cutIntoTwo(right, out right, out tmpr);
				right = recMergeSort(right, tmpr);
			}


			ListNode head = null;
			ListNode temp = null;
			ListNode curr = null;
			while (left != null || right != null)
			{
				if (left != null && right != null)
				{
					if (left.val < right.val)
					{
						temp = left;
						left = left.next;
					}
					else
					{
						temp = right;
						right = right.next;
					}
				}
				else if (left != null)
				{
					temp = left;
					left = left.next;
				}
				else
				{
					temp = right;
					right = right.next;
				}

				temp.next = null;
				if (curr != null)
				{
					curr.next = temp;
				}
				curr = temp;

				if (head == null)
				{
					head = curr;
				}
			}
			return head;
		}

		//328. Odd Even Linked List
		//不難，畫一下圖就有了，只是一次就跳兩個指標，所以條件比多一層
		public ListNode OddEvenList(ListNode head)
		{
			if (head == null)
			{
				return null;
			}

			ListNode odd = head;
			ListNode even = null;
			ListNode evenHead = null;
			ListNode last = odd;

			while (odd != null)
			{
				last = odd;

				if (odd.next != null)
				{
					if (even == null)
					{
						even = odd.next;
						evenHead = even;
					}
					else
					{
						even.next = odd.next;
						even = even.next;
					}

					odd.next = even.next;
					even.next = null;
					odd = odd.next;
				}
				else
				{
					odd = odd.next;
				}
			}

			last.next = evenHead;

			return head;
		}
    }
}
