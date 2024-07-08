using System;
using Assets.Scripts.Estructura_de_Datos;
using Assets.Scripts.Estructura_de_Datos.Interfaces;

namespace TDAs.Trees
{
    public class AVLTree : IAVLTree
    {
        private AVLNode root;

    public void Insert(EventNode data)
    {
        root = Insert(root, data);
    }

    private AVLNode Insert(AVLNode node, EventNode data)
    {
        if (node == null)
            return new AVLNode(data);

        int comparison = data.CompareTo(node.Data);
        if (comparison < 0)
            node.Left = Insert(node.Left, data);
        else if (comparison > 0)
            node.Right = Insert(node.Right, data);
        else
            return node;  // Los datos duplicados no se insertan

        return Balance(node);
    }

    public void Remove(EventNode data)
    {
        root = Remove(root, data);
    }

    private AVLNode Remove(AVLNode node, EventNode data)
    {
        if (node == null) return node;

        int cmp = data.CompareTo(node.Data);
        if (cmp < 0)
            node.Left = Remove(node.Left, data);
        else if (cmp > 0)
            node.Right = Remove(node.Right, data);
        else
        {
            if (node.Left == null)
                return node.Right;
            else if (node.Right == null)
                return node.Left;

            node.Data = GetMin(node.Right).Data;
            node.Right = Remove(node.Right, node.Data);
        }

        return Balance(node);
    }

    public AVLNode GetMin()
    {
        return GetMin(root);
    }

    private AVLNode GetMin(AVLNode node)
    {
        AVLNode current = node;
        while (current != null && current.Left != null)
            current = current.Left;
        return current;
    }

    private AVLNode Balance(AVLNode node)
    {
        if (node == null) return node;

        int balance = GetBalance(node);
        if (balance > 1)
        {
            if (GetBalance(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        if (balance < -1)
        {
            if (GetBalance(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }

    private AVLNode RotateRight(AVLNode y)
    {
        AVLNode x = y.Left;
        AVLNode T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private AVLNode RotateLeft(AVLNode x)
    {
        AVLNode y = x.Right;
        AVLNode T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    private int GetHeight(AVLNode node)
    {
        return node == null ? 0 : node.Height;
    }

    private void UpdateHeight(AVLNode node)
    {
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    private int GetBalance(AVLNode node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }
    }

}