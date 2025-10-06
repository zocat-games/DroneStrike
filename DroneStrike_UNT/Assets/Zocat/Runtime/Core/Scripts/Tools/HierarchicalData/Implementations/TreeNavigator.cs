using System;
using System.Collections.Generic;
using System.Linq;
using HierarchicalData.Abstractions;
using Zocat;
using EventHandler = Opsive.Shared.Events.EventHandler;
using ObjectPool = Opsive.Shared.Game.ObjectPool;
using Opsive.Shared.Game;
using UnityEngine;

namespace HierarchicalData.Implementations
{
    public class TreeNavigator : InstanceBehaviour
    {
        /*--------------------------------------------------------------------------------------*/
        private List<StructuralNode> AllNodes = new();

        /*--------------------------------------------------------------------------------------*/
        public void CreateNode(string id)
        {
            var node = new StructuralNode(id, id, id);
            AddIfNotExist(node);
        }

        public void AddChild(string parent, string child)
        {
            var _parent = GetById(parent);
            var _child = GetById(child);
            _parent.AddChild(_child);
        }

        public void AddIfNotExist(StructuralNode node)
        {
            if (CanAdd(node)) AllNodes.Add(node);
        }

        private bool CanAdd(StructuralNode node)
        {
            foreach (var item in AllNodes)
            {
                if (item.Id == node.Id) return false;
            }

            return true;
        }

        public StructuralNode GetById(string id)
        {
            foreach (var item in AllNodes)
            {
                if (item.Id == id) return item;
            }

            return null;
        }

        public bool AtTheSameBranch(string ancestor, string child)
        {
            if (ancestor == child) return true;
            var path = GetNodeFullPath(child);


            foreach (var item in path)
            {
                if (item != child & item == ancestor) return true;
            }

            return false;
        }

        /*--------------------------------------------------------------------------------------*/
        public string[] GetNodeFullPath(string _node)
        {
            var node = GetById(_node);
            var result = new List<string> { node.PathPart };

            var parent = node.Parent;

            while (parent != null)
            {
                result.Insert(0, parent.PathPart);
                parent = parent.Parent;
            }

            return result.ToArray();
        }

        public INode SearchById(string id)
        {
            var root = GetById("Root");
            if (root?.GetFlatChildren().ContainsKey(id) ?? false)
            {
                return root.GetFlatChildren()[id];
            }

            return null;
        }

        public IList<INode> Search(IStructuralNode root, Func<INode, bool> predicate)
        {
            var flat = root?.GetFlatChildren();

            if (flat != null)
            {
                return flat
                    .Where(entry => predicate(entry.Value))
                    .Select(entry => entry.Value)
                    .ToList();
            }

            return new List<INode>();
        }

        public int FindNodeDepth(string node)
        {
            var _node = GetById(node);
            var root = GetById("Root");
            var foundNode = SearchById(_node.Id);

            if (foundNode != null)
            {
                var depth = 0;
                var parent = _node.Parent;

                while (parent != null)
                {
                    depth++;

                    if (parent.Id == root.Id)
                    {
                        break;
                    }

                    parent = parent.Parent;
                }

                return depth;
            }

            return 0;
        }
    }
}