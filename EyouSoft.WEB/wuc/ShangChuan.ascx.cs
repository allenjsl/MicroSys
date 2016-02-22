using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.wuc
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public partial class ShangChuan : System.Web.UI.UserControl
    {
        #region attributes
        /// <summary>
        /// File Input ClientId,ClientName
        /// </summary>
        public string FileClientId { get { return ClientID + "_File"; } }
        /// <summary>
        /// 新上传文件Filepath Input ClientName
        /// </summary>
        public string FilepathClientName { get { return ClientID + "_Filepath"; } }
        /// <summary>
        /// 原上传文件Filepath Input ClientName
        /// </summary>
        public string YuanFilepathClientName { get { return ClientID + "_Yuan_Filepath"; } }
        string _FileTypeExts = "*.jpg;*.jpeg;*.gif;*.png;*.bmp";
        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        public string FileTypeExts { get { return _FileTypeExts; } set { _FileTypeExts = value; } }
        string _FileTypeDesc = "请选择图片";
        /// <summary>
        /// 在浏览窗口底部的文件类型下拉菜单中显示的文本
        /// </summary>
        public string FileTypeDesc { get { return _FileTypeDesc; } set { _FileTypeDesc = value; } }
        /// <summary>
        /// QueueClientId
        /// </summary>
        public string QueueClientId { get { return ClientID + "_Queue"; } }
        /// <summary>
        /// XianShiClientId
        /// </summary>
        public string XianShiClientId { get { return ClientID + "_XianShi"; } }
        string _Multi = "0";
        /// <summary>
        /// 0：单个文件 1：多个文件
        /// </summary>
        public string Multi { get { return _Multi; } set { _Multi = value; } }
        string _XianShiClassName = "uploadify_xianshi";
        /// <summary>
        /// 显示上传信息样式名称
        /// </summary>
        public string XianShiClassName { get { return _XianShiClassName; } set { _XianShiClassName = value; } }

        /// <summary>
        /// 原文件信息集合
        /// </summary>
        public IList<EyouSoft.WEB.ashx.uploadfile.MFileInfo> YuanFiles
        {
            get { return GetYuanFiles(); }
            set { SetYuanFiles(value); }
        }

        /// <summary>
        /// 上传的文件信息集合
        /// </summary>
        public IList<EyouSoft.WEB.ashx.uploadfile.MFileInfo> Files { get { return GetFiles(); } }

        protected string YuanFilesJson = "[]";
        /// <summary>
        /// 上传说明
        /// </summary>
        public string ShuoMing { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region private members
        /// <summary>
        /// get yuan files
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.WEB.ashx.uploadfile.MFileInfo> GetYuanFiles()
        {
            IList<EyouSoft.WEB.ashx.uploadfile.MFileInfo> items = new List<EyouSoft.WEB.ashx.uploadfile.MFileInfo>();

            string[] txtFilepath = Utils.GetFormValues(YuanFilepathClientName);

            if (txtFilepath != null && txtFilepath.Length > 0)
            {
                foreach (var filepath in txtFilepath)
                {
                    var item = new EyouSoft.WEB.ashx.uploadfile.MFileInfo();
                    item.Filepath = filepath;
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// set yuan files
        /// </summary>
        /// <param name="items"></param>
        void SetYuanFiles(IList<EyouSoft.WEB.ashx.uploadfile.MFileInfo> items)
        {
            if (items != null && items.Count > 0) YuanFilesJson = Newtonsoft.Json.JsonConvert.SerializeObject(items);
        }

        /// <summary>
        /// get files
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.WEB.ashx.uploadfile.MFileInfo> GetFiles()
        {
            IList<EyouSoft.WEB.ashx.uploadfile.MFileInfo> items = new List<EyouSoft.WEB.ashx.uploadfile.MFileInfo>();

            string[] txtFilepath = Utils.GetFormValues(FilepathClientName);

            if (txtFilepath != null && txtFilepath.Length > 0)
            {
                foreach (var filepath in txtFilepath)
                {
                    var item = new EyouSoft.WEB.ashx.uploadfile.MFileInfo();
                    item.Filepath = filepath;
                    items.Add(item);
                }
            }


            return items;
        }
        #endregion
    }
}