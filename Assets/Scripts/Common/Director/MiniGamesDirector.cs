using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsMiniGames.Common
{
    public class MiniGamesDirector
    {
        private PauseController _pauseController = new PauseController();

        public void Setup()
        {
            _pauseController.Setup();
        }

        public void Dispose()
        {
            _pauseController.Dispose();
        }
    }
}