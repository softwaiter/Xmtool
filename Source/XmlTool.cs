using CodeM.Common.Tools.DynamicObject;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CodeM.Common.Tools.Xml
{
    public class XmlNodeInfo
    {
        private XmlTextReader mReader;

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

        public bool IsRoot
        {
            get
            {
                return mReader.Depth == 0;
            }
        }

        public bool IsNode
        {
            get
            {
                return (mReader.NodeType == XmlNodeType.Element || 
                    mReader.NodeType == XmlNodeType.EndElement) &&
                    !mIsEmptyTextNode;
            }
        }

        private bool mIsEmptyNodeEnd = false;
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

        private bool mIsEmptyTextNode = false;
        public bool IsTextNode
        {
            get
            {
                return (mReader.NodeType == XmlNodeType.Text) || mIsEmptyTextNode;
            }
            internal set
            {
                mIsEmptyTextNode = value;
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

        /// <summary>
        /// 节点级别
        /// </summary>
        public int Level
        {
            get
            {
                return mReader.Depth + 1;
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

        public string GetAttributeName(int index)
        {
            if (index >= 0 && index < mReader.AttributeCount)
            {
                mReader.MoveToAttribute(index);
                string attrName = mReader.Name;
                mReader.MoveToElement();
                return attrName;
            }
            return string.Empty;
        }

        public int Line
        {
            get;
            set;
        }

    }

    public delegate bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);

    public class XmlTool
    {
        private static XmlTool sXTool = new XmlTool();

        private XmlTool()
        { 
        }

        internal static XmlTool New()
        {
            return sXTool;
        }

        private void IterateXmlNode(XmlTextReader xr, XmlNodeInfoGetter callback)
        {
            Dictionary<string, int> nodeIndexes = new Dictionary<string, int>();
            Dictionary<string, bool> nodeHasChild = new Dictionary<string, bool>();
            Dictionary<string, bool> nodeHasText = new Dictionary<string, bool>();
            Dictionary<string, bool> nodeHasCData = new Dictionary<string, bool>();

            XmlNodeInfo nodeInfo = null;

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
                            nodeInfo.IsTextNode = true;
                            if (!callback(nodeInfo))
                            {
                                break;
                            }
                            nodeHasText.TryAdd(currentFullPath, true);

                            nodeInfo.Path = currentPath;
                            nodeInfo.FullPath = currentFullPath;
                            nodeInfo.IsTextNode = false;
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

        public void Iterate(string file, XmlNodeInfoGetter callback = null)
        {
            if (callback != null)
            {
                using (XmlTextReader xr = new XmlTextReader(file))
                {
                    IterateXmlNode(xr, callback);
                }
            }
        }

        public void IterateFromString(string content, XmlNodeInfoGetter callback = null)
        {
            if (callback != null)
            {
                using (StringReader sr = new StringReader(content))
                {
                    using (XmlTextReader xr = new XmlTextReader(sr))
                    {
                        IterateXmlNode(xr, callback);
                    }
                }
            }
        }

        private void DeserializeXmlNode(Stack<DynamicObjectExt> s,
            XmlNodeInfo node, bool includeRoot)
        {
            if (!node.IsEndNode)
            {
                if (!node.IsRoot || includeRoot)
                {
                    DynamicObjectExt p = s.Peek();
                    if (node.IsNode)
                    {
                        DynamicObjectExt newObj = new DynamicObjectExt();
                        p.TrySetValue(node.LocalName, newObj);
                        s.Push(newObj);

                        for (int i = 0; i < node.AttributeCount; i++)
                        {
                            string attrName = node.GetAttributeName(i);
                            newObj.TrySetValue(attrName, node.GetAttribute(i));
                        }
                    }
                    else if (node.IsTextNode)
                    {
                        p.TrySetValue("Value", node.Text);
                    }
                    else if (node.IsCDATANode)
                    {
                        p.TrySetValue("Value", node.CData);
                    }
                }
            }
            else if (node.IsEndNode && node.IsNode)
            {
                s.Pop();
            }
        }

        public dynamic Deserialize(string file, bool includeRoot = false)
        {
            DynamicObjectExt r = new DynamicObjectExt();
            Stack<DynamicObjectExt> s = new Stack<DynamicObjectExt>();
            s.Push(r);

            Iterate(file, (XmlNodeInfo node) =>
            {
                DeserializeXmlNode(s, node, includeRoot);
                return true;
            });
            return r;
        }

        public dynamic DeserializeFromString(string xml, bool includeRoot = false)
        {
            DynamicObjectExt r = new DynamicObjectExt();
            Stack<DynamicObjectExt> s = new Stack<DynamicObjectExt>();
            s.Push(r);

            IterateFromString(xml, (XmlNodeInfo node) =>
            {
                DeserializeXmlNode(s, node, includeRoot);
                return true;
            });
            return r;
        }
    }
}