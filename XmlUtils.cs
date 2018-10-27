using System;
using System.Collections.Generic;
using System.Xml;

namespace CodeM.Common.Tools.Xml
{
    public class XmlNodeInfo
    {
        private XmlTextReader mReader;

        private string mPath;
        private string mLocalName;
        private string mFullName;
        private string mNamespaceURI;
        private int mLine;

        public XmlNodeInfo(XmlTextReader reader)
        {
            mReader = reader;
        }

        public string Path
        {
            set
            {
                mPath = value;
            }
            get
            {
                return mPath;
            }
        }

        public string LocalName
        {
            set
            {
                mLocalName = value;
            }
            get
            {
                return mLocalName;
            }
        }

        public string FullName
        {
            set
            {
                mFullName = value;
            }
            get
            {
                return mFullName;
            }
        }

        public string NamespaceURI
        {
            set
            {
                mNamespaceURI = value;
            }
            get
            {
                return mNamespaceURI;
            }
        }

        public bool IsNode
        {
            get
            {
                return mReader.NodeType == XmlNodeType.Element || 
                    mReader.NodeType == XmlNodeType.EndElement;
            }
        }

        public bool IsEndNode
        {
            get
            {
                return mReader.NodeType == XmlNodeType.EndElement;
            }
        }

        public bool IsTextNode
        {
            get
            {
                return mReader.NodeType == XmlNodeType.Text;
            }
        }

        public bool IsCDATANode
        {
            get
            {
                return mReader.NodeType == XmlNodeType.CDATA;
            }
        }

        public string Text
        {
            get
            {
                return mReader.Value;
            }
        }

        public string CData
        {
            get
            {
                return mReader.Value;
            }
        }

        public int AttributeCount
        {
            get
            {
                return mReader.AttributeCount;
            }
        }

        public bool HasAttributes()
        {
            return mReader.HasAttributes;
        }

        public string GetAttribute(int index)
        {
            if (index >= 0 && index < mReader.AttributeCount)
            {
                return mReader.GetAttribute(index);
            }
            return string.Empty;
        }

        public string GetAttribute(string name)
        {
            return mReader.GetAttribute(name);
        }

        public string GetAttribute(string localName, string namespaceURI)
        {
            return mReader.GetAttribute(localName, namespaceURI);
        }

        public int Line
        {
            set
            {
                mLine = value;
            }
            get
            {
                return mLine;
            }
        }

    }

    public delegate bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);

    public class XmlUtils
    {
        public static void Iterate(string file, XmlNodeInfoGetter callback = null)
		{
            if (callback != null) {
                XmlNodeInfo nodeInfo = null;

                using (XmlTextReader xr = new XmlTextReader(file))
                {
                    List<string> pathItems = new List<string>();
                    while (xr.Read())
                    {
                        XmlNodeType nodeType = xr.NodeType;
                        if (nodeType != XmlNodeType.XmlDeclaration &&
                            nodeType != XmlNodeType.Whitespace)
                        {
                            if (nodeType == XmlNodeType.Element)
                            {
                                string localName = xr.LocalName;
                                pathItems.Add(localName);

                                nodeInfo = new XmlNodeInfo(xr);
                                nodeInfo.Path = string.Concat("/", string.Join("/", pathItems));
                                nodeInfo.LocalName = localName;
                                nodeInfo.NamespaceURI = xr.NamespaceURI;
                                nodeInfo.FullName = xr.Name;
                                nodeInfo.Line = xr.LineNumber;

                                if (!callback(nodeInfo))
                                {
                                    break;
                                }
                            }
                            else if (nodeType == XmlNodeType.Text)
                            {
                                nodeInfo.Path += "/@text";
                                if (!callback(nodeInfo))
                                {
                                    break;
                                }
                            }
                            else if (nodeType == XmlNodeType.CDATA)
                            {
                                nodeInfo.Path += "/@cdata";
                                if (!callback(nodeInfo))
                                {
                                    break;
                                }
                            }
                            else if (nodeType == XmlNodeType.EndElement)
                            {
                                if (nodeInfo != null)
                                {
                                    nodeInfo.Path = string.Concat("/", string.Join("/", pathItems));
                                    nodeInfo.Line = xr.LineNumber;
                                    if (!callback(nodeInfo))
                                    {
                                        break;
                                    }
                                }

                                if (pathItems.Count > 0)
                                {
                                    pathItems.RemoveAt(pathItems.Count - 1);
                                }
                            }
                        }
                    }
                }
            }
		}
    }
}