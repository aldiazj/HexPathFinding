using System.Collections.Generic;
using Hex.Runtime.AI;
using Hex.Runtime.Level;
using UnityEngine;

namespace Hex.Runtime.GameControl.States
{
    public class FindPath : GameState
    {
        private GameManager gameManager;
        private IPathFinder pathFinder;
        private IList<ICell> path;

        public override void Initialize(GameManager gameManagerReference)
        {
            gameManager = gameManagerReference;
            pathFinder = new PathFinder();
            path = pathFinder.FindPathOnMap(gameManager.StartCell, gameManager.EndCell, gameManager.Map);
        }

        public void ShowResults()
        {
            foreach (ICell pathCell in path)
            {
                pathCell.SetColor(Color.yellow);
            }
            gameManager.StartCell.SetColor(Color.blue);
            gameManager.EndCell.SetColor(Color.green);
        }

        public int GetPathLength() => path.Count;
    }
}