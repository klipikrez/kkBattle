using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class TIDsync : NetworkBehaviour
{
    public teamIdentefier team;

    [SyncVar(hook = nameof(upadteVarID))]
    public int teamID = 0;
    [SyncVar(hook = nameof(upadteVarColor))]
    public Color teamColor = Color.red;

    void upadteVarID(int ildVar, int assas)
    {
        team.teamID = teamID;
    }

    void upadteVarColor(Color ildVar, Color assas)
    {
        team.teamColor = teamColor;
    }

}
