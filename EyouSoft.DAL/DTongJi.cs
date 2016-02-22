//统计相关DAL 汪奇志 2015-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL
{
    public class DTongJi : DALBase
    {
        #region static constants
        //static constants
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DTongJi()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        #endregion

        #region public members
        #endregion
    }
}
