using UnityEngine;
using System.Collections;

public class GaterController : GaterElement
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            app.model.MakeMove(Direction.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            app.model.MakeMove(Direction.Down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            app.model.MakeMove(Direction.Left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            app.model.MakeMove(Direction.Right);
    }
}
