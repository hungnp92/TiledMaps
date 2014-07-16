using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap_Ver1.Model
{
    public class Tile_DAL
    {
        private int iID;

        public int IID
        {
            get { return iID; }
            set { iID = value; }
        }

        public Image imgTile { get; set; }
        
        private string strTileName;

        public string StrTileName
        {
            get { return strTileName; }
            set { strTileName = value; }
        }

        public Tile_DAL(int _iID, string _strTileName, Image _imgTile)
        {
            this.iID = _iID;
            this.strTileName = _strTileName;
            this.imgTile = _imgTile;
        }
    }
}
