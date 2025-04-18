using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TictTackGame
{
    namespace Act
    {
        public static class GameActions
        {
            public static Action<GameType> levelSelect;
            public static Action RestartAction;

            public static Action StopAction;
        }
    }

    namespace Data
    {
        public static class GameValues
        {
            public static GameType currentGameType;
        }
    }
}

