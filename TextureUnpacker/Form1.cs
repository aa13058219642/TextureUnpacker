using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextureUnpacker
{
    public partial class Form1 : Form
    {

        Atlas atlasLoad;
		PlistLoad plistLoad;

        public Form1()
        {
            InitializeComponent();
        }

        private void BT_OpenConfig_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            String path = openFileDialog1.FileName;
            if (path != "")
            {
                TB_ConfigPath.Text = path;
				string pngfile = TB_ConfigPath.Text.Substring(0, TB_ConfigPath.Text.LastIndexOf('.')) + ".png";
				if(File.Exists(pngfile))
				{
					TB_TexturePath.Text = pngfile;

					if (TB_ConfigPath.Text.Substring(TB_ConfigPath.Text.LastIndexOf('.')) == ".plist")
					{
						RB_Plist.Checked = true;
					}
					else if(TB_ConfigPath.Text.Substring(TB_ConfigPath.Text.LastIndexOf('.')) == ".atlas")
					{
						RB_Atlas.Checked = true;
					}
				}
				BT_Open_Click(sender, e);
            }
        }

        private void BT_OpenTexture_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();

            String path = openFileDialog2.FileName;
            if (path != "")
            {
                TB_TexturePath.Text = path;
            }
        }

        private void BT_Open_Click(object sender, EventArgs e)
        {
            //try
            {
				String path1 = TB_ConfigPath.Text;
				String path2 = TB_TexturePath.Text;

				if (path1 == "" || path2 == "")
					return;

                if(RB_Plist.Checked==true) //Plist
				{
					plistLoad = new PlistLoad(path1);

					Image img = Image.FromFile(path2);
					Bitmap bmp = new Bitmap(img);
					if (CB_ShowFrame.Checked == true)
					{
						Graphics g = Graphics.FromImage(bmp);
						Pen pen = new Pen(Color.Red, 1);

						foreach (PlistFrame frame in plistLoad.plistFile.frames)
						{
							if (frame.rotated == true)
								g.DrawRectangle(pen, new Rectangle(frame.frame.Left, frame.frame.Top, frame.frame.Height, frame.frame.Width));
							else
								g.DrawRectangle(pen, frame.frame);
						}
					}
					texture_box.Image = bmp;
                }
                else if (RB_Atlas.Checked == true) //Atlas
				{
					atlasLoad = new Atlas(path1);
                    Image img = Image.FromFile(path2);
                    Bitmap bmp = new Bitmap(img);
                    if (CB_ShowFrame.Checked == true)
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        Pen pen = new Pen(Color.Red, 1);

                        foreach (AtlasRegion region in atlasLoad.List_atlasFile[0].region)
                        {
                            if (region.rotate == true)
                                g.DrawRectangle(pen, new Rectangle(region.xy, new Size(region.size.Height, region.size.Width)));
                            else
                                g.DrawRectangle(pen, new Rectangle(region.xy, region.size));
                        }
                    }
                    texture_box.Image = bmp;
                }
            }
            //catch
            //{
            //    MessageBox.Show("路径或格式错误");
            //}
        }

        private void BT_Export_Click(object sender, EventArgs e)
        {
			String path1 = TB_ConfigPath.Text;
			String path2 = TB_TexturePath.Text;

			if (path1 == "" || path2 == "")
				return;

			String path = path1;
			path = path.Substring(0, path.LastIndexOf(@"\")).Trim();
			path = path + "\\export";

			//如果不存在就创建file文件夹
			if (Directory.Exists(path) == false)
				Directory.CreateDirectory(path);
            
            if (RB_Plist.Checked == true)
				//export Plist
				plistLoad.export(path2, path, CB_ClipSpeite.Checked);
            else if (RB_Atlas.Checked == true)
				//export Atlas
				atlasLoad.export(path2, path, CB_ClipSpeite.Checked);

        }

		private void TB_ConfigPath_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
			else 
				e.Effect = DragDropEffects.None;
		}

		private void TB_ConfigPath_DragDrop(object sender, DragEventArgs e)
		{
			string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			TB_ConfigPath.Text = path;
			string pngfile = TB_ConfigPath.Text.Substring(0, TB_ConfigPath.Text.LastIndexOf('.')) + ".png";
			if (File.Exists(pngfile))
			{
				TB_TexturePath.Text = pngfile;

				if (TB_ConfigPath.Text.Substring(TB_ConfigPath.Text.LastIndexOf('.')) == ".plist")
					RB_Plist.Checked = true;
				else if (TB_ConfigPath.Text.Substring(TB_ConfigPath.Text.LastIndexOf('.')) == ".atlas")
					RB_Atlas.Checked = true;
			}
			BT_Open_Click(sender, e);
		}

		private void TB_TexturePath_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link;
			else 
				e.Effect = DragDropEffects.None;
		}

		private void TB_TexturePath_DragDrop(object sender, DragEventArgs e)
		{
			string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			TB_TexturePath.Text = path;
		}

		private void CB_ShowFrame_CheckedChanged(object sender, EventArgs e)
		{
			BT_Open_Click(sender, e);
		}

    }
}
