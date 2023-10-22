using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;
using UnityEngine;

public class PlayerPathfinding {
    private List<Coil> _coils;
    private List<WebNode> _nodes;
    
    public PlayerPathfinding(List<Coil> coils) {
        _coils = coils;
        _nodes = new List<WebNode>();
        foreach (Coil coil in coils) {
            _nodes.Add(coil.node);
        }
    }

    private List<Coil> ToCoil(List<WebNode> nodes) {
        List<Coil> coils = new List<Coil>();
        foreach (WebNode node in nodes) {
            coils.Add(node.coil);
        }
        return coils;
    }
    
    private List<WebNode> ToWebNode(List<Coil> coils) {
        List<WebNode> nodes = new List<WebNode>();
        foreach (Coil coil in coils) {
            nodes.Add(new WebNode(coil));
        }
        return nodes;
    }

    List<Coil> RetracePath(WebNode start, WebNode target) {
        List<WebNode> path = new List<WebNode>();
        path.Add(target);
        WebNode current = target;

        while (current != start) {
            path.Add(current.prev);
            current = current.prev;
        }

        path.Reverse();
        return ToCoil(path);
    }

    public List<Coil> FindPath(Coil start, Coil target) {
        WebNode startNode = new WebNode(start);
        WebNode targetNode = new WebNode(target);

        List<WebNode> openList = new List<WebNode>();
        HashSet<WebNode> closedList = new HashSet<WebNode>();
        openList.Add(startNode);

        //Cost Calculation
        foreach (WebNode node in _nodes) {
            node.gCost = int.MaxValue;
            node.prev = null;
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, targetNode);

        while (openList.Count > 0) {
            WebNode curr = GetLowestFCostNode(openList);
            if (curr == targetNode) {
                //At final node ayo
                return RetracePath(startNode, targetNode);
            }

            openList.Remove(curr);
            closedList.Add(curr);

            foreach (WebNode node in GetNeightbourList(curr)) {
                if (closedList.Contains(node)) continue;

                float tempGCost = curr.gCost + CalculateDistance(curr, node);
                if (tempGCost < node.gCost) {
                    node.prev = node;
                    node.gCost = tempGCost;
                    node.hCost = CalculateDistance(node, targetNode);

                    if (!openList.Contains(node)) {
                        openList.Add(node);
                    }
                }
            }
        }
        
        //No more nodes
        return null;
    }

    private List<WebNode> GetNeightbourList(WebNode curr) {
        List<WebNode> neightborList = new List<WebNode>();
        foreach (Wire wire in curr.coil.wires) {
            Coil coil1 = wire.coils.coil1;
            Coil coil2 = wire.coils.coil2;
            if (coil1.node != curr) {
                neightborList.Add(coil2.node);
            }
            else {
                neightborList.Add(coil1.node);
            }
        }

        return neightborList;
    }

    private WebNode GetLowestFCostNode(List<WebNode> nodeList) {
        WebNode lowestFCostNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++) {
            if (nodeList[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private float CalculateDistance(WebNode a, WebNode b) {
        return Vector3.Distance(a.location, b.location);
    }
}
