using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMineBehavoir
{
    public virtual void SetUpMine(MineRoomManager mineRoomManager, Mine mineOwner){ }

    public virtual void SendDataConnection(Mine mineOwner) {}

    public virtual void MineSubscribe(Mine mineOwner) {}
    public virtual void MineUnSubscribe(Mine mineOwner) {}

    public virtual void GlobalMinesubscribe(Mine mineOwner) {}
    public virtual void GlobalMineUnSubscribe(Mine mineOwner) {}
}
