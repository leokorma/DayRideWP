using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using DayRide.Resources;
using Microsoft.Phone.Shell;

namespace DayRide.Utils
{
    /**
   * Helper Class to manage Tile events
   */
    public class TileUtils
    {
        public static void Init(ShellTile tile)
        {
            EditTitle(tile, LocalizationResources.ApplicationName);
        }

        public static void EditTitle(ShellTile tile, string title)
        {
            if (tile != null)
            {
                StandardTileData NewTileData = new StandardTileData
                {
                    Title = title
                };

                tile.Update(NewTileData);
            }
        }
    }
}
