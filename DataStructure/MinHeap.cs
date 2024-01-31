using System;
using System.Collections.Generic;

namespace DataStructure
{
    public class MinHeap<TElement, TPriority>
    {
        private List<(TElement Element, TPriority Priority)> heap;
        private Comparer<TPriority> comparer;
        private int rootIndex = 1;
        //1-indexed
        public bool IsEmpty() => heap.Count == 1;
        public MinHeap()
        {
            this.heap = new List<(TElement, TPriority)>();
            this.heap.Add(default); //0番目の要素をあけておいて、1-indexedにする
            this.comparer = Comparer<TPriority>.Default;
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            heap.Add((element, priority));
            HeapifyUp(heap.Count - 1);
        }

        public TElement Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Heap is Empty");
            }

            var result = heap[rootIndex].Element;
            heap[rootIndex] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(rootIndex);

            return result;

        }

        // ヒープに要素を追加した際の木の構築
        private void HeapifyUp(int index)
        {
            while (index > 1)
            {
                int parentIndex = index / 2;
                //親よりも子の方が大きい
                if (comparer.Compare(heap[index].Priority, heap[parentIndex].Priority) >= 0)
                {
                    break;
                }

                SwapHeap(index, parentIndex);
                index = parentIndex;
            }
        }

        //ヒープから要素を削除した際の木の構築
        private void HeapifyDown(int index)
        {
            int lastIndex = heap.Count - 1;
            while (true)
            {
                int leftChildIndex = 2 * index;
                int rightChildIndex = 2 * index + 1;
                int smallestIndex = index;

                if (leftChildIndex <= lastIndex && comparer.Compare(heap[leftChildIndex].Priority, heap[smallestIndex].Priority) < 0)
                {
                    smallestIndex = leftChildIndex;
                }

                if (rightChildIndex <= lastIndex && comparer.Compare(heap[rightChildIndex].Priority, heap[smallestIndex].Priority) < 0)
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex == index) break;

                SwapHeap(index, smallestIndex);
                index = smallestIndex;
            }
        }

        //ヒープの要素を交換, 引数はindex
        private void SwapHeap(int index1, int index2)
        {
            (heap[index1], heap[index2]) = (heap[index2], heap[index1]);
        }
    }
}