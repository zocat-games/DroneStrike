using System;
using DG.Tweening;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;
using ObjectPool = Opsive.Shared.Game.ObjectPool;
using Opsive.Shared.Game;

namespace Zocat
{
    public class WorksiteManager : MonoSingleton<WorksiteManager>
    {
        public WorksiteLocation WorksiteLocation;
    }
}