using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    #region  DISCLAIMER
    /* THE CLASS IS PROVIDED "AS IS". ANY ECONOMIC (OR OTHERWISE) 
    /* LOSS/DAMAGE INCURRED DIRECTLY OR INDIRECTLY BECAUSE OF THIS 
    /* CLASS IS TO BE BEARED BY THE USER. THE AUTHOR IS IN NO WAY 
    /* RESPONSIBLE FOR ANY SUCH LOSS/DAMAGE.*/
    #endregion
    public class STROperation
    {
        /// <summary>
        /// This function prepares an StreeNode Similar to the treeView
        /// </summary>
        public static STreeNode fnPrepareToWrite(TreeView treeView)
        {
            try
            {
                TreeNode treeNode = new TreeNode();
                foreach (TreeNode tr in treeView.Nodes)
                {
                    treeNode.Nodes.Add((TreeNode)tr.Clone());
                }
                STreeNode FinalStr = fnPrepareTreeNode(treeNode);
                return FinalStr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This function prepares an StreeNode Similar to the TreeNode
        /// </summary>
        public static STreeNode fnPrepareToWrite(TreeNode treeNode)
        {
            try
            {
                TreeNode treeNode_t = new TreeNode();
                foreach (TreeNode tr in treeNode.Nodes)
                {
                    treeNode_t.Nodes.Add((TreeNode)tr.Clone());
                }
                STreeNode FinalStr = fnPrepareTreeNode(treeNode_t);
                return FinalStr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Allied functions for writing purpose
        /// <summary>
        /// One of the two recursives.
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        private static List<STreeNode> fnPrepareChildNode(TreeNode tr)
        {
            List<STreeNode> retSTreeNode = new List<STreeNode>();
            STreeNode stc;
            foreach (TreeNode trc in tr.Nodes)
            {
                stc = fnPrepareTreeNode(trc);
                retSTreeNode.Add(stc);
            }
            return retSTreeNode;
        }
        /// <summary>
        /// One of the two recursives
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        private static STreeNode fnPrepareTreeNode(TreeNode tr)
        {
            STreeNode strRet = new STreeNode();
            strRet.Name = tr.Name;
            strRet.ToolTipText = tr.ToolTipText;
            strRet.ImageIndex = tr.ImageIndex;
            if (tr.Tag != null)
                strRet.Tag = tr.Tag.ToString();
            else
                strRet.Tag = null;
            strRet.Text = tr.Text.ToString();
            strRet.SelectedImageIndex = tr.SelectedImageIndex;
            strRet.Nodes = fnPrepareChildNode(tr);
            return strRet;
        }
        #endregion
        /// <summary>
        /// This functions returns the treeView for which it has been written
        /// </summary>
        /// <returns></returns>
        public static TreeNode fnPrepareToRead(STreeNode sTreeNode)
        {
            try
            {
                TreeNode FinalTreeNode = RfnPrepareTreeNode(sTreeNode);
                return FinalTreeNode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Allied functions for Writing Purpose
        private static List<TreeNode> RfnPrepareChildNodes(STreeNode str)
        {
            List<TreeNode> retTreeNode = new List<TreeNode>();
            TreeNode tnc;
            foreach(STreeNode strc in str.Nodes)
            {
                tnc = RfnPrepareTreeNode(strc);
                retTreeNode.Add(tnc);
            }
            return retTreeNode;
        }
        private static TreeNode RfnPrepareTreeNode(STreeNode str)
        {
            TreeNode trnRet = new TreeNode();
            trnRet.Name = str.Name;
            trnRet.Tag = str.Tag;
            trnRet.ToolTipText = str.ToolTipText;
            trnRet.ImageIndex = str.ImageIndex;
            trnRet.Text = str.Text;
            trnRet.SelectedImageIndex = str.SelectedImageIndex;
            #region Building NodeCollection
            List<TreeNode> retTempTreeNodeList = RfnPrepareChildNodes(str);
            foreach (TreeNode tempTr in retTempTreeNodeList)
            {
                trnRet.Nodes.Add(tempTr);
            }
            #endregion
            return trnRet;
        }
        #endregion
    }
}
