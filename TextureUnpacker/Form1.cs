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

        AtlasLoad atlasLoad;
		PlistLoad plistLoad;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            String path = openFileDialog1.FileName;
            if (path != "")
            {
                textBox1.Text = path;
				string pngfile = textBox1.Text.Substring(0, textBox1.Text.LastIndexOf('.')) + ".png";
				if(File.Exists(pngfile))
				{
					textBox2.Text = pngfile;

					if (textBox1.Text.Substring(textBox1.Text.LastIndexOf('.')) == ".plist")
					{
						r1.Checked = true;
					}
					else if(textBox1.Text.Substring(textBox1.Text.LastIndexOf('.')) == ".atlas")
					{
						r2.Checked = true;
					}
				}
				button3_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();

            String path = openFileDialog2.FileName;
            if (path != "")
            {
                textBox2.Text = path;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //try
            {
				String path1 = textBox1.Text;
				String path2 = textBox2.Text;

				if (path1 == "" || path2 == "")
				{
					return;
				}

                //Plist
                if(r1.Checked==true)
                {
					plistLoad = new PlistLoad(path1);

					Image img = Image.FromFile(path2);
					Bitmap bmp = new Bitmap(img);
					if (checkBox1.Checked == true)
					{
						Graphics g = Graphics.FromImage(bmp);
						Pen pen = new Pen(Color.Red, 1);

						foreach (PlistFrame frame in plistLoad.plistFile.frames)
						{
							if (frame.rotated == true)
							{
								g.DrawRectangle(pen, new Rectangle(
									frame.frame.Left, 
									frame.frame.Top,
									frame.frame.Height,
									frame.frame.Width));
							}
							else
							{
								g.DrawRectangle(pen, frame.frame);
							}
						}
					}
					pictureBox1.Image = bmp;
                }
                //Atlas
                else if (r2.Checked == true)
                {
					atlasLoad = new AtlasLoad(path1);

                    Image img = Image.FromFile(path2);

                    Bitmap bmp = new Bitmap(img);
                    if (checkBox1.Checked == true)
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        Pen pen = new Pen(Color.Red, 1);

                        foreach (AtlasRegion region in atlasLoad.List_atlasFile[0].region)
                        {
                            if (region.rotate == true)
                            {
                                g.DrawRectangle(pen, new Rectangle(region.xy, new Size(region.size.Height, region.size.Width)));
                            }
                            else
                            {
                                g.DrawRectangle(pen, new Rectangle(region.xy, region.size));
                            }
                        }
                    }
                    pictureBox1.Image = bmp;
                }
            }
            //catch
            //{
            //    MessageBox.Show("路径或格式错误");
            //}
        }


        private void button4_Click(object sender, EventArgs e)
        {
			String path1 = textBox1.Text;
			String path2 = textBox2.Text;

			if (path1 == "" || path2 == "")
			{
				return;
			}

			String path = path1;
			path = path.Substring(0, path.LastIndexOf(@"\")).Trim();
			path = path + "\\export";

			//如果不存在就创建file文件夹
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}

			//导出
            //Plist
            if (r1.Checked == true)
            {
				foreach(PlistFrame frame in  plistLoad.plistFile.frames)
				{
					Bitmap bmp;
					Image img = Image.FromFile(path2);
					if (frame.rotated == true)
					{
						bmp = new Bitmap(frame.sourceSize.Height, frame.sourceSize.Width);
						Graphics g = Graphics.FromImage(bmp);

						g.DrawImage(img,
							new Rectangle(
								(frame.sourceSize.Height - frame.frame.Height) / 2 + frame.offset.Y,
								(frame.sourceSize.Width - frame.frame.Width) / 2 + frame.offset.X,
								frame.frame.Height,
								frame.frame.Width),
							new Rectangle(
								frame.frame.Left,
								frame.frame.Top,
								frame.frame.Height,
								frame.frame.Width
								),
							GraphicsUnit.Pixel);

						bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
					}
					else
					{
						bmp = new Bitmap(frame.sourceSize.Width, frame.sourceSize.Height);
						Graphics g = Graphics.FromImage(bmp);

						Rectangle r = new Rectangle(
								(frame.sourceSize.Width - frame.frame.Width) / 2 + frame.offset.X,
								(frame.sourceSize.Height - frame.frame.Height) / 2 + frame.offset.Y,
								frame.frame.Width,
								frame.frame.Height);

						g.DrawImage(img,
							new Rectangle(
								(frame.sourceSize.Width - frame.frame.Width )/ 2 + frame.offset.X,
								(frame.sourceSize.Height - frame.frame.Height) / 2 - frame.offset.Y,
								frame.frame.Width,
								frame.frame.Height),
							frame.frame,
							GraphicsUnit.Pixel);
					}
					bmp.Save(path + "\\" + frame.name);
				}
            }
            //Atlas
            else if (r2.Checked == true)
            {
                foreach (AtlasRegion region in atlasLoad.List_atlasFile[0].region)
                {
                    Bitmap bmp;
					Image img = Image.FromFile(path2);
                    bmp = new Bitmap(region.orig.Width, region.orig.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    if (region.rotate == true)
                    {
                        g.TranslateTransform(region.orig.Width, 0.0F);
                        g.RotateTransform(90.0F);
                        g.DrawImage(img,
                            new Rectangle(new Point(region.offset.Y, region.offset.X), new Size(region.size.Height, region.size.Width)),
                            new Rectangle(region.xy, new Size(region.size.Height, region.size.Width)),
                            GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(img,
                            new Rectangle(region.offset, region.orig),
                            new Rectangle(region.xy, region.size),
                            GraphicsUnit.Pixel);
                    }
                    pictureBox2.Image = bmp;
                    bmp.Save(path + "\\" + region.name + ".png");
                }
            }

        }

		private void textBox1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
			else e.Effect = DragDropEffects.None;
		}

		private void textBox1_DragDrop(object sender, DragEventArgs e)
		{
			string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			textBox1.Text = path;
			string pngfile = textBox1.Text.Substring(0, textBox1.Text.LastIndexOf('.')) + ".png";
			if (File.Exists(pngfile))
			{
				textBox2.Text = pngfile;

				if (textBox1.Text.Substring(textBox1.Text.LastIndexOf('.')) == ".plist")
				{
					r1.Checked = true;
				}
				else if (textBox1.Text.Substring(textBox1.Text.LastIndexOf('.')) == ".atlas")
				{
					r2.Checked = true;
				}
			}
			button3_Click(sender, e);
		}

		private void textBox2_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link;
			else e.Effect = DragDropEffects.None;
		}

		private void textBox2_DragDrop(object sender, DragEventArgs e)
		{
			string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			textBox2.Text = path;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			button3_Click(sender, e);
		}

    }
}
