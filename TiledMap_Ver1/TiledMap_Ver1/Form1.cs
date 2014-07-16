using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiledMap_Ver1.Model;
using TiledMap_Ver1.Object;

namespace TiledMap_Ver1
{
    public partial class Form1 : Form
    {
        #region Fields
        //TiledMap_DAL tiledMap;
        Image Background;

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Form Control
        /// <summary>
        /// Procced image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbTileName.Text == null)        
            {
                MessageBox.Show("Init name of tile set", "Error", MessageBoxButtons.OK);
            }
            else
            {
                List<Tile_DAL> listTile = new List<Tile_DAL>();
                int sizetile_height = (int)Int32.Parse(this.nudTileHeight.Value.ToString());
                int sizetile_width = (int)Int32.Parse(nudTileWidth.Value.ToString());
                pgBar.Maximum = (Background.Width / sizetile_width) * (Background.Height / sizetile_height);
                
                int testW = Background.Width / sizetile_width;
                int testH = Background.Height / sizetile_height;
                pgBar.Value = 0;
                try
                {
                    for (int i = 0; i < Background.Height / sizetile_height - 1; i++)
                        for (int j = 0; j < Background.Width / sizetile_width - 1; j++)
                        {
                            pgBar.Value++;
                            Image imgTile = Util.Crop(new Rectangle(j * sizetile_width, i * sizetile_height, sizetile_width, sizetile_height));
                            Tile_DAL _tile = new Tile_DAL(listTile.Count, tbTileName.Text + "_" + listTile.Count, imgTile);

                            if (listTile.Count == 0)
                            {
                                listTile.Add(_tile);
                            }
                            else
                            {
                                if (CompareImageWithList(listTile, _tile.imgTile))
                                    listTile.Add(_tile);
                            }

                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                SaveImage(listTile);
                MessageBox.Show("Process complete! Congratulation!", "Success", MessageBoxButtons.OK);
            }
            
        }

        //cancel button
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Get image path
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select file to be upload";
            fDialog.Filter = "PNG Image(*.png)|*.png*";
            //  fDialog.Filter = "PDF Files|*.pdf";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {       
                tbLocated.Text = fDialog.FileName.ToString();
                Background = Image.FromFile(tbLocated.Text);
                tbSize.Text = "" + Background.Size.Width + " x " + Background.Size.Height;
                Util.SourceImage = Image.FromFile(tbLocated.Text);
                Util.bmp = new Bitmap(Util.SourceImage);
            }

        }
        #endregion

        #region Public methob
        /// <summary>
        /// Compare one Image with list of Tile throught ImageCompare
        /// </summary>
        /// <param name="listTile">list Tile</param>
        /// <param name="image">Image compare</param>
        /// <returns>
        /// true  : have no Image like image input in list
        /// false : have image like image input in list 
        /// </returns>
        public bool CompareImageWithList(List<Tile_DAL> listTile, Image image)
        {
            int count = 0;
            foreach (Tile_DAL tile in listTile)
            {
                if (Util.ImageCompare(image, tile.imgTile) == true)
                {
                    count++;
                }
            }
            if (count > 0)
                return false;
            else
                return true;
        }

        public void SaveImage(List<Tile_DAL> listTile)
        {
            foreach (Tile_DAL tile in listTile)
            {
                tile.imgTile.Save("TiledMap//" + tile.StrTileName + ".png");
                //Console.WriteLine("save tile " + tile.IID);
            }

        }
        #endregion

    }
}
