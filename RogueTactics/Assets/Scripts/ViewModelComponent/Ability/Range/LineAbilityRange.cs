using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class LineAbilityRange : AbilityRange
{
    public override List<TileDefinitionData> GetTilesInRange()
    {
        Vector2 startPos = unit.TileDefinition.position;
        Vector2[] endPos = new Vector2[4];
        List<TileDefinitionData> retValue = new List<TileDefinitionData>();

        endPos[0] = new Vector2(startPos.x, Board.max.y);
        endPos[1] = new Vector2(Board.max.x, startPos.y);
        endPos[2] = new Vector2(startPos.x, Board.min.y);
        endPos[3] = new Vector2(Board.min.x, startPos.y);
        
        for (int i = 0; i < endPos.Length; ++i)
        {
            startPos = unit.TileDefinition.position;
            
            while (startPos != endPos[i])
            {
                if (startPos.x < endPos[i].x) startPos.x++;
                else if (startPos.x > endPos[i].x) startPos.x--;
                
                if (startPos.y < endPos[i].y) startPos.y++;
                else if (startPos.y > endPos[i].y) startPos.y--;
                
                TileDefinitionData t = Board.GetTile(startPos);
                if (t != null)
                    retValue.Add(t);
            }
        }

        return retValue;
    }

    public override List<TileDefinitionData> GetTilesInRange(List<TileDefinitionData> movementRadiusTiles)
    {
        throw new System.NotImplementedException();
    }
}
