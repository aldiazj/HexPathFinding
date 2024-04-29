using System.Collections.Generic;
using Hex.Runtime.Level;

namespace Hex.Runtime.AI
{
    public interface IPathFinder
    {
        IList<ICell> FindPathOnMap(ICell cellStart, ICell cellEnd, IMap map);
    }
}