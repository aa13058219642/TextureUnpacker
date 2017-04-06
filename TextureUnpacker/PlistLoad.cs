using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Drawing;

namespace TextureUnpacker
{
	public class PlistFile
	{
		public PlistMetadata metadata = new PlistMetadata();
		public List<PlistFrame> frames = new List<PlistFrame>();
	}

	public struct PlistMetadata
	{
		public int format;
		public String realTextureFileName;
		public Size size;
		public String smartupdate;
		public String textureFileName;
	}

	public struct PlistFrame
	{
		public String name;
		public Rectangle frame;
		public Point offset;
		public bool rotated;
		public Rectangle sourceColorRect;
		public Size sourceSize;
	}


	class PlistLoad
	{
		public PlistFile plistFile = new PlistFile();

		public PlistLoad() { }
		public PlistLoad(String filepath) { load(filepath); }

		public void load(String path)
		{
			System.IO.StreamReader file = new System.IO.StreamReader(path);

			// Create an XmlReaderSettings object.  
			XmlReaderSettings settings = new XmlReaderSettings();

			// Set XmlResolver to null, and ProhibitDtd to false. 
			settings.XmlResolver = null;
			settings.DtdProcessing = DtdProcessing.Ignore;

			// Now, create an XmlReader.  This is a forward-only text-reader based
			// reader of Xml.  Passing in the settings will ensure that validation
			// is not performed. 
			XmlReader reader = XmlTextReader.Create(file, settings);

			// Create your document, and load the reader. 
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			XmlNode root = doc.DocumentElement.FirstChild;

			Dictionary<string, XmlNode> dict = tree_to_dict(root);

			plistFile = dict_to_plist(dict);

			file.Close();
		}

		private Dictionary<string, XmlNode> tree_to_dict(XmlNode root)
		{
			Dictionary<string, XmlNode> d = new Dictionary<string, XmlNode>();
			for (int nodeIndex = 0; nodeIndex < root.ChildNodes.Count; nodeIndex += 2)
			{
				if (root.ChildNodes[nodeIndex].Name == "key" )
				{ 
					if(root.ChildNodes[nodeIndex + 1].Name == "dict")
						d.Add(root.ChildNodes[nodeIndex].InnerText, root.ChildNodes[nodeIndex + 1]);
					else if (root.ChildNodes[nodeIndex + 1].Name == "string")
						d.Add(root.ChildNodes[nodeIndex].InnerText, root.ChildNodes[nodeIndex + 1]);
					else if (root.ChildNodes[nodeIndex + 1].Name == "true")
						d.Add(root.ChildNodes[nodeIndex].InnerText, root.ChildNodes[nodeIndex + 1]);
					else if (root.ChildNodes[nodeIndex + 1].Name == "false")
						d.Add(root.ChildNodes[nodeIndex].InnerText, root.ChildNodes[nodeIndex + 1]);
					else if (root.ChildNodes[nodeIndex + 1].Name == "integer")
						d.Add(root.ChildNodes[nodeIndex].InnerText, root.ChildNodes[nodeIndex + 1]);
				}
            }
			return d;
		}

		private List<int> to_list(string str)
		{
			String[] slist = str.Replace('{',' ').Replace('}',' ').Split(',');
			List<int> list = new List<int>();
			foreach(String s in slist)
			{
				list.Add(Int32.Parse(s.Trim()));
			}
			return list;
		}

		private PlistFile dict_to_plist(Dictionary<string, XmlNode> dict)
		{
			PlistFile plistFile = new PlistFile();

			//metadata
			plistFile.metadata.format = 0;
			if(dict.ContainsKey("metadata"))
			{
				Dictionary<string, XmlNode> metadata = tree_to_dict(dict["metadata"]);
				plistFile.metadata.format = Int32.Parse(metadata["format"].InnerText);
				plistFile.metadata.realTextureFileName = metadata["realTextureFileName"].InnerText;
				plistFile.metadata.smartupdate = metadata["smartupdate"].InnerText;
				plistFile.metadata.textureFileName = metadata["textureFileName"].InnerText;

				List<int> li = to_list(metadata["size"].InnerText);
				plistFile.metadata.size = new Size(li[0], li[1]);
			}

			//frames
			Dictionary<string, XmlNode> frames = tree_to_dict(dict["frames"]);
			int format = plistFile.metadata.format;
			foreach (KeyValuePair<string, XmlNode> node in frames)
			{
				Dictionary<string, XmlNode> d = tree_to_dict(node.Value);
				PlistFrame frame = new PlistFrame();
				frame.name = node.Key;
				if(format==0)
				{
					frame.frame = new Rectangle(
						Int32.Parse(d["x"].InnerText), 
						Int32.Parse(d["y"].InnerText),
						Int32.Parse(d["width"].InnerText),
						Int32.Parse(d["height"].InnerText)
					);

					frame.offset = new Point(
						Int32.Parse(d["offsetX"].InnerText),
						Int32.Parse(d["offsetY"].InnerText)
					);

					frame.sourceSize = new Size(
						Math.Abs(Int32.Parse(d["originalWidth"].InnerText)),
						Math.Abs(Int32.Parse(d["originalHeight"].InnerText))
					);

					frame.rotated = false;
				}
				else if (format == 1 || format == 2)
				{
					frame.rotated = format == 2 ? (d["rotated"].Name == "true") : false;

					List<int> li = to_list(d["frame"].InnerText);
					frame.frame = new Rectangle(li[0], li[1], li[2], li[3]);

					li = to_list(d["offset"].InnerText);
					frame.offset = new Point(li[0], li[1]);

					li = to_list(d["sourceSize"].InnerText);
					frame.sourceSize = new Size(li[0], li[1]);
				}
				else if (format == 3)
				{
					frame.rotated = bool.Parse(d["textureRotated"].InnerText);

					List<int> li = to_list(d["spriteSourceSize"].InnerText);
					frame.sourceSize = new Size(li[0], li[1]);

					li = to_list(d["spriteOffset"].InnerText);
					frame.offset = new Point(li[0], li[1]);

					li = to_list(d["spriteSize"].InnerText);
					Size s = new Size(li[0], li[1]);

					li = to_list(d["frame"].InnerText);
					Rectangle b = new Rectangle(li[0], li[1], li[2], li[3]);

					frame.frame = new Rectangle(b.X, b.Y, s.Width, s.Height);

				}
				plistFile.frames.Add(frame);
			}
			return plistFile;
		}
	}
}












