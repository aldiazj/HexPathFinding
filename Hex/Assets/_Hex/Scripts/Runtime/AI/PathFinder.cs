using System;
using System.Collections.Generic;
using Hex.Runtime.Level;

namespace Hex.Runtime.AI
{
    public class PathFinder: IPathFinder
    {
        public IList<ICell> FindPathOnMap(ICell cellStart, ICell cellEnd, IMap map)
        {
            List<ICell> toSearch = new List<ICell> { cellStart, };
            List<ICell> processed = new List<ICell>();

            while (toSearch.Count > 0)
            {
                ICell current = toSearch[0];
                foreach (ICell cell in toSearch)
                {
                    if (cell.TotalCost < current.TotalCost
                        || cell.TotalCost == current.TotalCost
                        && cell.DistanceToTargetNode < current.DistanceToTargetNode)
                    {
                        current = cell;
                    }
                }
                processed.Add(current);
                toSearch.Remove(current);

                if (current == cellEnd)
                {
                    ICell currentPathTile = cellEnd;
                    IList<ICell> path = new List<ICell>();
                    int maxCellsCounter = map.Grid.Count;
                    while (currentPathTile != cellStart)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        maxCellsCounter--;

                        if (maxCellsCounter < 0)
                        {
                            throw new Exception("It was not possible to find the path");
                        }
                    }

                    return path;
                }

                IEnumerable<ICell> currentNeighbors = map.GetExistingCellsAt(current.Neighbors);

                foreach (ICell currentNeighbor in currentNeighbors)
                {
                    if (!currentNeighbor.IsWalkable || processed.Contains(currentNeighbor))
                    {
                        continue;
                    }

                    float costToNeighbor = current.DistanceToStartNode + current.GetDistanceTo(currentNeighbor);
                    bool isInSearch = toSearch.Contains(currentNeighbor);

                    if (isInSearch && costToNeighbor >= currentNeighbor.DistanceToStartNode)
                    {
                        continue;
                    }

                    currentNeighbor.DistanceToStartNode = costToNeighbor;
                    currentNeighbor.Connection = current;

                    if (isInSearch)
                    {
                        continue;
                    }

                    currentNeighbor.DistanceToTargetNode = currentNeighbor.GetDistanceTo(cellEnd);
                    toSearch.Add(currentNeighbor);
                }
            }

            return new List<ICell>();
        }
    }
}