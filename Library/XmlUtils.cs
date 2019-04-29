using System.Collections.Generic;
using System.Xml;

namespace CodeM.Common.Tools.Xml
{
    public class XmlNodeInfo
    {
        private XmlTextReader mReader;

        private bool mIsEmptyNodeEnd = false;

        public XmlNodeInfo(XmlTextReader reader)
        {
            mReader = reader;
        }

        public string Path
        {
            get;
            set;
        }

        public string FullPath
        {
            get;
            set;
        }

        public string LocalName
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public string NamespaceURI
        {
            get;
            set;
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
                if (mIsEmptyNodeEnd)
                {
                    return true;
                }
                else
                {
                    return mReader.NodeType == XmlNodeType.EndElement;
                }
            }
            set
            {
                mIsEmptyNodeEnd = value;
            }
        }

        public bool IsEmptyNode
        {
            get
            {
                return mReader.IsEmptyElement;
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
            get;
            set;
        }

    }

    public delegate bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);

    public class XmlUtils
    {
        public static void Iterate(string file, XmlNodeInfoGetter callback = null)
		{
            if (callback != null) {
                Dictionary<string, int> nodeIndexes = new Dictionary<string, int>();
                Dictionary<string, bool> nodeHasChild = new Dictionary<string, bool>();
                Dictionary<string, bool> nodeHasText = new Dictionary<string, bool>();
                Dictionary<string, bool> nodeHasCData = new Dictionary<string, bool>();

                XmlNodeInfo nodeInfo = null;
                using (XmlTextReader xr = new XmlTextReader(file))
                {
                    List<string> pathItems = new List<string>();
                    List<string> fullPathItems = new List<string>();
                    while (xr.Read())
                    {
                        XmlNodeType nodeType = xr.NodeType;
                        if (nodeType != XmlNodeType.XmlDeclaration &&
                            nodeType != XmlNodeType.Whitespace &&
                            nodeType != XmlNodeType.Comment)
                        {
                            if (nodeType == XmlNodeType.Element)
                            {
                                string localName = xr.LocalName;
                                pathItems.Add(localName);

                                string currentPath = string.Concat("/", string.Join("/", pathItems));

                                string parentFullPath = fullPathItems.Count > 0 ? string.Concat("/", string.Join("/", fullPathItems)) : "/";
                                nodeHasChild.TryAdd(parentFullPath, true);

                                string indexPath = "/".Equals(parentFullPath) ? string.Concat("/", localName) : string.Concat(parentFullPath, "/", localName);
                                int currentIndex = nodeIndexes.GetValueOrDefault(indexPath, 0);

                                fullPathItems.Add(string.Concat(localName, "[", currentIndex, "]"));
                                string currentFullPath = string.Concat("/", string.Join("/", fullPathItems));

                                nodeInfo = new XmlNodeInfo(xr);
                                nodeInfo.Path = currentPath;
                                nodeInfo.FullPath = currentFullPath;
                                nodeInfo.LocalName = localName;
                                nodeInfo.NamespaceURI = xr.NamespaceURI;
                                nodeInfo.FullName = xr.Name;
                                nodeInfo.Line = xr.LineNumber;

                                if (!callback(nodeInfo))
                                {
                                    break;
                                }

                                if (xr.IsEmptyElement)
                                {
                                    nodeInfo.Path += "/@text";
                                    nodeInfo.FullPath += "/@text";
                                    if (!callback(nodeInfo))
                                    {
                                        break;
                                    }
                                    nodeHasText.TryAdd(currentFullPath, true);

                                    nodeInfo.Path = currentPath;
                                    nodeInfo.FullPath = currentFullPath;
                                    nodeInfo.IsEndNode = true;
                                    if (!callback(nodeInfo))
                                    {
                                        break;
                                    }

                                    if (pathItems.Count > 0)
                                    {
                                        pathItems.RemoveAt(pathItems.Count - 1);
                                    }
                                    if (fullPathItems.Count > 0)
                                    {
                                        fullPathItems.RemoveAt(fullPathItems.Count - 1);
                                    }

                                    if (!nodeIndexes.ContainsKey(indexPath))
                                    {
                                        nodeIndexes.Add(indexPath, 1);
                                    }
                                    else
                                    {
                                        nodeIndexes[indexPath] += 1;
                                    }
                                }
                            }
                            else if (nodeType == XmlNodeType.Text)
                            {
                                nodeHasText.TryAdd(nodeInfo.FullPath, true);

                                nodeInfo.Path += "/@text";
                                nodeInfo.FullPath += "/@text";
                                if (!callback(nodeInfo))
                                {
                                    break;
                                }
                            }
                            else if (nodeType == XmlNodeType.CDATA)
                            {
                                nodeHasCData.TryAdd(nodeInfo.FullPath, true);

                                nodeInfo.Path += "/@cdata";
                                nodeInfo.FullPath += "/@cdata";
                                if (!callback(nodeInfo))
                                {
                                    break;
                                }
                            }
                            else if (nodeType == XmlNodeType.EndElement)
                            {
                                if (nodeInfo != null)
                                {
                                    string currentPath = string.Concat("/", string.Join("/", pathItems));
                                    string currentFullPath = string.Concat("/", string.Join("/", fullPathItems));

                                    if (pathItems.Count > 0)
                                    {
                                        pathItems.RemoveAt(pathItems.Count - 1);
                                    }
                                    if (fullPathItems.Count > 0)
                                    {
                                        fullPathItems.RemoveAt(fullPathItems.Count - 1);
                                    }

                                    string parentFullPath = fullPathItems.Count > 0 ? string.Concat("/", string.Join("/", fullPathItems)) : "/";
                                    string indexPath = "/".Equals(parentFullPath) ? string.Concat("/", xr.LocalName) : string.Concat(parentFullPath, "/", xr.LocalName);

                                    if (!nodeIndexes.ContainsKey(indexPath))
                                    {
                                        nodeIndexes.Add(indexPath, 1);
                                    }
                                    else
                                    {
                                        nodeIndexes[indexPath] += 1;
                                    }

                                    bool hasChild = false;
                                    nodeHasChild.TryGetValue(currentFullPath, out hasChild);
                                    bool hasText = false;
                                    nodeHasText.TryGetValue(currentFullPath, out hasText);
                                    bool hasCData = false;
                                    nodeHasCData.TryGetValue(currentFullPath, out hasCData);
                                    if (!hasChild && !hasText && !hasCData)
                                    {
                                        nodeInfo.Path = string.Concat(currentPath, "/@text");
                                        nodeInfo.FullPath = string.Concat(currentFullPath, "/@text");
                                        if (!callback(nodeInfo))
                                        {
                                            break;
                                        }
                                    }

                                    nodeInfo.Path = currentPath;
                                    nodeInfo.FullPath = currentFullPath;
                                    nodeInfo.Line = xr.LineNumber;
                                    if (!callback(nodeInfo))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
		}
    }
}