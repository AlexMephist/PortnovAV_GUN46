using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp1_List
{
    internal class Node<T>(T? data)
    {
        private readonly T? data = data;

        public T? Data { get; set; }
        public Node<T>? Previous { get; set; }
        public Node<T>? Next { get; set; }
    }
}
