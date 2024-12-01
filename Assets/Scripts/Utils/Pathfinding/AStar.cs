using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Scripts.Utils.PathFinding
{
    public class AStar
    {
        public static LevelData GameData;

        public static List<Vector2> FindPath(Vector2 start1, Vector2 end1)
        {
            GameData = MapInit._this.LevelData;
            Vector2 start = new Vector2(Round(start1.x), Round(start1.y));
            Vector2 end = new Vector2(Round(end1.x), Round(end1.y));
            var isStraightLineClear = IsStraightLineClear(start, end);
            //  Debug.Log($"start {start} end {end} is straight {isStraightLineClear}");
            if (isStraightLineClear)
            {
                return new List<Vector2>();
            }
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            Node startNode = new Node(start);
            openList.Add(startNode);
            while (openList.Count > 0)
            {
                var minF = GetMinF(openList);
                if (minF == null) { break; }
                Vector2 current = minF.pos;
                openList.Remove(minF);
                bool c = isSame(current, end);
                //var clean = IsStraightLineClear(current, end);
                closedList.Add(minF);
                if (c)
                {
                    List<Vector2> path = new List<Vector2>();
                    Node currentNode = minF;
                    while (currentNode != null)
                    {
                        path.Add(currentNode.pos);
                        currentNode = currentNode.father;
                    }
                    path.Reverse();
                    path.RemoveAt(0);
                    if (path.Count > 1)
                    {
                        path.RemoveAt(path.Count - 1);
                    }
                    return path;
                }
                foreach (Vector2 neighbor in getNeighbors(current))
                {
                    float g = isDiagonalMove(neighbor, current) ? 2.4f : 1.2f;
                    g = isNoWallMove(current, neighbor) ? 1.2f : 2.4f;
                    float h = ExpectedValue(neighbor, end);
                    Node nnode = new Node(neighbor)
                    {
                        father = minF,
                        f = g + h
                    };
                    if (isInList(nnode, closedList)) { continue; }
                    if (!isInList(nnode, openList))
                    {
                        openList.Add(nnode);
                    }
                }
            }
            return new List<Vector2>();
        }
        public static int Round(float value)
        {
            if (value - (int)value > 0.5)
            {
                return (int)value + 1;
            }
            return (int)value;
        }
        public static float ExpectedValue(Vector2 start, Vector2 end)
        {
            return Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y);
        }
        public static bool isWalkable(Vector2 position)
        {
            //   Debug.Log($"Max X {X} Max Y {Y} X {position.x} Y {position.y}");
            if (position.y > GameData.mapdata.map.Count - 1 || position.x > GameData.mapdata.map[0].Count - 1 || position.x < 0 || position.y < 0)
            {
                return false;
            }
            int tileInt = GameData.mapdata.map[(int)position.y][(int)position.x];
            var tile = GameData.mapdata.tiles[tileInt];
            //Debug.Log($"Pos {position} type {typeLower}");
            if (tile.heightType != "HIGHLAND" && !tile.tileKey.ToLower().Contains("fence_bound") && !tile.tileKey.ToLower().Contains("hole"))
            {
                return true;
            }
            return false;
        }
        public static List<Vector2> getNeighbors(Vector2 position)
        {
            List<Vector2> neighbors = new List<Vector2>();
            Vector2 l = new Vector2(position.x - 1f, position.y);
            Vector2 r = new Vector2(position.x + 1f, position.y);
            Vector2 u = new Vector2(position.x, position.y + 1f);
            Vector2 d = new Vector2(position.x, position.y - 1f);
            Vector2 lu = new Vector2(position.x - 1f, position.y + 1f);
            Vector2 ld = new Vector2(position.x - 1f, position.y - 1f);
            Vector2 ru = new Vector2(position.x + 1f, position.y + 1f);
            Vector2 rd = new Vector2(position.x + 1f, position.y - 1f);
            if (isWalkable(l)) neighbors.Add(l);
            if (isWalkable(r)) neighbors.Add(r);
            if (isWalkable(u)) neighbors.Add(u);
            if (isWalkable(d)) neighbors.Add(d);
            if (isWalkable(lu) && isWalkable(l) && isWalkable(u)) neighbors.Add(lu);
            if (isWalkable(ld) && isWalkable(l) && isWalkable(d)) neighbors.Add(ld);
            if (isWalkable(ru) && isWalkable(r) && isWalkable(u)) neighbors.Add(ru);
            if (isWalkable(rd) && isWalkable(r) && isWalkable(d)) neighbors.Add(rd);
            return neighbors;
        }
        private static bool isNoWallMove(Vector2 start, Vector2 end)
        {
            if (end.x == start.x || end.y == start.y)
            {
                return true;
            }
            if (isWalkable(new Vector2(start.x, end.y)) && isWalkable(new Vector2(end.x, start.y)))
            {
                return true;
            }
            return false;
        }
        private static bool isDiagonalMove(Vector2 position, Vector2 point)
        {
            return position.x != point.x && position.y != point.y;
        }
        private static bool isSame(Vector2 a, Vector2 b)
        {
            if (a.x == b.x && a.y == b.y)
            {
                return true;
            }
            return false;
        }
        public static bool isInList(Node node, List<Node> list)
        {
            foreach (var lnode in list)
            {
                if (lnode.pos.x == node.pos.x && lnode.pos.y == node.pos.y)
                {
                    return true;
                }
            }
            return false;
        }
        public static Node GetMinF(List<Node> nodes)
        {
            float minF = nodes.Min(node => node.f);
            foreach (var F in nodes)
            {
                if (F.f == minF)
                {
                    return F;
                }
            }
            return null;
        }
        public static bool IsStraightLineClear(Vector2 start, Vector2 end)
        {
            int dx = (int)Math.Abs(Math.Round(end.x - start.x));
            int dy = (int)Math.Abs(Math.Round(end.y - start.y));
            if (dx <= 1 && dy <= 1)
            {
                return isWalkable(new Vector2(start.x, end.y)) && isWalkable(new Vector2(end.x, start.y));
            }
            int sx = start.x < end.x ? 1 : -1;
            int sy = start.y < end.y ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;
            int e2;
            int x0 = (int)Math.Round(start.x);
            int y0 = (int)Math.Round(start.y);
            int x1 = x0;
            int y1 = y0;
            while (true)
            {
                if (x1 != x0 && y1 != y0)
                {
                    if (!isWalkable(new Vector2(x0, y1))) { return false; }
                    if (!isWalkable(new Vector2(x1, y0))) { return false; }
                }
                if (!isWalkable(new Vector2(x0, y0)))
                {
                    return false;
                }
                x1 = x0;
                y1 = y0;
                if (new Vector2(x0, y0) == end) { return true; }
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }
    }
    public class Node
    {
        public Node(Vector2 pos)
        {
            this.pos = pos;
        }
        public Vector2 pos { get; set; }
        public float f { get; set; } = float.MaxValue;
        public Node father { get; set; }
    }
}