using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TextureUnpacker
{
    public struct Rect
    {
        public int left, right, top, bottom;

        public Rect(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }
    }

    public struct AtlasPage
    {
        public String name;
        public Size size;
        public String format;  //Alpha, Intensity, LuminanceAlpha, RGB565, RGBA4444, RGB888, RGBA8888
        public String filter;  //Nearest, Linear, MipMap, MipMapNearestNearest, MipMapLinearNearest, MipMapNearestLinear, MipMapLinearLinear.
        public String repeat;  //x, y, xy, none.
    }

    public struct AtlasRegion
    {
        public String name;    //The first line is the region name. This is used to find a region in the atlas. Multiple regions may have the same name if they have a different index.
        public bool rotate;    //If true, the region was stored in the page image rotated by 90 degrees counter clockwise.
        public Point xy;       //The pixel location of this image within the page image.
        public Size size;      //The packed size, which is the pixel size of this image within the page image.
        public Rect split;     //The left, right, top, and bottom splits for a ninepatch. These are the number of pixels from the unpacked image edge. Splits define a 3x3 grid for a resizing an image without stretching all parts of the image. split may be omitted.
        public Rect pad;       //The left, right, top, and bottom padding for a ninepatch. These are the number of pixels from the unpacked image edge. Padding allows content placed on top of a ninepatch to be inset differently from the splits. pad may be omitted and is always omitted if there is no split.
        public Size orig;      //The original size, which is the pixel size of this image before it was packed. If whitespace stripping was performed, this may be larger than the region in the page image.
        public Point offset;   //The amount of whitespace pixels that were stripped from the left and bottom edges of the image before it was packed.
        public int index;      //An index allows many images to be packed using the same name, as long as each has a different index. Typically the index is the frame number for regions that will be shown sequentional for frame by frame animation.
    }

    public class AtlasFile
    {
        public AtlasPage page = new AtlasPage();
        public List<AtlasRegion> region = new List<AtlasRegion>();
    }

    class Atlas
    {
        public List<AtlasFile> List_atlasFile = new List<AtlasFile>();

		public Atlas() { }
		public Atlas(String filepath) { load(filepath); }


        public void load(String path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            
            String str, p_name;
            int lineindex = 0;
            int page = 0;
            int regionNum = 0;

            AtlasFile atlasFile = null;
            AtlasRegion atlasRegion = new AtlasRegion();

            while ((str = file.ReadLine()) != null)
            {
                // empty line mean new Region
                if (str == "")
                {
					continue;
                }
                // first line is Name
                else if (lineindex == 0)
                {
                    if (atlasFile != null)
                    {
                        atlasFile.region.Add(atlasRegion);
                        List_atlasFile.Add(atlasFile);
                    }
                    page++;
                    regionNum = 0;
                    atlasFile = new AtlasFile();
                    atlasFile.page.name = str;
                }
                // next 4 lines is AtlasFils's Head
                else if (lineindex < 5)
                {
                    p_name = getPropertiesName(str);
                    if (p_name == "size")
                    {
                        List<int> var = getIntValue(str);
                        atlasFile.page.size = new Size(var[0], var[1]);
                    }
                    else if (p_name == "format")
                    {
                        atlasFile.page.format = getStringValue(str)[0];
                    }
                    else if (p_name == "filter")
                    {
                        atlasFile.page.filter = getStringValue(str)[0];
                    }
                    else if (p_name == "repeat")
                    {
                        atlasFile.page.repeat = getStringValue(str)[0];
                    }
                }
                else
                {
                    p_name = getPropertiesName(str);
                    if (p_name == "rotate")
                    {
                        if (getStringValue(str)[0]=="false")
                        {
                            atlasRegion.rotate = false;
                        }
                        else
                        {
                            atlasRegion.rotate = true;
                        }
                    }
                    else if (p_name == "xy")
                    {
                        List<int> var = getIntValue(str);
                        atlasRegion.xy = new Point(var[0], var[1]);
                    }
                    else if (p_name == "size")
                    {
                        List<int> var = getIntValue(str);
                        atlasRegion.size = new Size(var[0], var[1]);
                    }
                    else if (p_name == "split")
                    {
                        List<int> var = getIntValue(str);
                        atlasRegion.split = new Rect(var[0], var[1], var[2], var[3]);
                    }
                    else if (p_name == "pad")
                    {
                        List<int> var = getIntValue(str);
                        atlasRegion.pad = new Rect(var[0], var[1], var[2], var[3]);
                    }
                    else if (p_name == "orig")
                    {
                        List<int> var = getIntValue(str);
                        atlasRegion.orig = new Size(var[0], var[1]);
                    }
                    else if (p_name == "offset")
                    {
                        List<int> var = getIntValue(str);
                        atlasRegion.offset = new Point(var[0], var[1]);
                    }
                    else if (p_name == "index")
                    {
                        List<int> var = getIntValue(str);
                        atlasRegion.index = var[0];
                    }
                    else
                    {
                        if (regionNum!=0)
                        {
                            atlasFile.region.Add(atlasRegion);
                        }
                        regionNum++;
                        atlasRegion = new AtlasRegion();
                        atlasRegion.name = p_name;
                    }
                }
                lineindex++;
            }

            if (atlasFile != null)
            {
                atlasFile.region.Add(atlasRegion);
                List_atlasFile.Add(atlasFile);
            }

            file.Close();
        }

        public bool export(String texture_path, String export_path, bool clip_sprite = false)
        {
            Bitmap bmp;
            Image img = Image.FromFile(texture_path);

            if (!clip_sprite)
            {
                foreach (AtlasRegion region in List_atlasFile[0].region)
                {
                    bmp = new Bitmap(region.orig.Width, region.orig.Height);
                    Graphics g = Graphics.FromImage(bmp);

                    // Pen pen = new Pen(Color.Red, 1);
                    // g.DrawRectangle(pen, new Rectangle(region.offset, region.size));

                    if (!region.rotate)
                    {
                        g.DrawImage(img,
                            new Rectangle(region.offset, region.size),
                            new Rectangle(region.xy, region.size),
                            GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.TranslateTransform(region.offset.X + region.size.Width, region.offset.Y);
                        g.RotateTransform(90.0F);
                        g.DrawImage(img,
                            new Rectangle(0, 0, region.size.Height, region.size.Width),
                            new Rectangle(region.xy, new Size(region.size.Height, region.size.Width)),
                            GraphicsUnit.Pixel);
                    }
                    bmp.Save(export_path + "\\" + region.name + ".png");
                }
            }
            else
            {
                foreach (AtlasRegion region in List_atlasFile[0].region)
                {
                    bmp = new Bitmap(region.size.Width, region.size.Height);
                    Graphics g = Graphics.FromImage(bmp);

                    if (!region.rotate)
                    {
                         g.DrawImage(img,
                             new Rectangle(0, 0, region.size.Width, region.size.Height),
                             new Rectangle(region.xy, region.size),
                             GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.TranslateTransform(region.size.Width, 0);
                        g.RotateTransform(90.0F);
                        g.DrawImage(img,
                            new Rectangle(0, 0, region.size.Height, region.size.Width),
                            new Rectangle(region.xy, new Size(region.size.Height, region.size.Width)),
                            GraphicsUnit.Pixel);
                    }
                    bmp.Save(export_path + "\\" + region.name + ".png");
                }
            }
            return true;
        }

        private String getPropertiesName(String str)
        {
            int index = str.IndexOf(':');
            return index!=-1? str.Substring(0, index).Trim() : str.Trim();
        }

        private List<int> getIntValue(String str)
        {
            List<int> values = new List<int>();
            String[] vs = getStringValue(str);

            foreach( String s in vs  )
            {
                int v = Int32.Parse(s);
                values.Add(v);
            }
            return values;
        }

        private String[] getStringValue(String str)
        {
            String s = str.Substring(str.IndexOf(':') + 1).Trim();

            String[] vs = s.Split(',');

            return vs;
        }


    }
}
