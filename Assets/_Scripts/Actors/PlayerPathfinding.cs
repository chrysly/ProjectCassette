using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;
using DG.Tweening;
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

    private List<Coil> RetracePath(WebNode start, WebNode target) {
        List<WebNode> path = new List<WebNode>();
        WebNode current = target;

        while (current.coil != start.coil) {
            path.Add(current);
            current = current.prev;
        }

        path.Reverse();
        return ToCoil(path);
    }

    //Nodes must be compared by grabbing their coils
    public List<Coil> FindPath(Coil start, Coil target) {
        WebNode startNode = start.node;
        WebNode targetNode = target.node;

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
            //WebNode curr = GetLowestFCostNode(openList);
            WebNode curr = openList[0];
            for (int i = 1; i < openList.Count; i++) {
                if (openList[i].fCost < curr.fCost || Mathf.Approximately(openList[i].fCost, curr.fCost) && openList[i].hCost < curr.hCost) {
                    curr = openList[i];
                }
            }
            
            openList.Remove(curr);
            closedList.Add(curr);
            
            if (curr.coil == targetNode.coil) {
                return RetracePath(startNode, targetNode);
            }

            foreach (WebNode node in GetNeightbourList(curr)) {
                if (closedList.Contains(node)) continue;

                float tempGCost = curr.gCost + CalculateDistance(curr, node);
                if (tempGCost < node.gCost || !openList.Contains(node)) {
                    node.gCost = tempGCost;
                    node.hCost = CalculateDistance(node, targetNode);
                    node.prev = curr;
                    
                    if (!openList.Contains(node)) {
                        openList.Add(node);
                    }
                }
            }
        }
        
        //No more nodes
        Debug.Log("No suitable paths");
        return null;
    }

    private List<WebNode> GetNeightbourList(WebNode curr) {
        List<WebNode> neightborList = new List<WebNode>();
        foreach (Wire wire in curr.coil.wires) {
            Coil coil1 = wire.coils.coil1;
            Coil coil2 = wire.coils.coil2;
            if (coil1 == curr.coil) {
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
