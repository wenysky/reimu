using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NConvert.Utils
{
    public class Forums
    {
        #region 递归所有子节点
        public static string ChildNode = "0";
        /// <summary>
        /// 递归所有子节点并返回字符串
        /// </summary>
        /// <param name="correntfid">当前</param>
        /// <returns>子版块的集合,格式:1,2,3,4,</returns>
        public static string FindChildNode(string correntfid)
        {
            lock (ChildNode)
            {
                DataTable dt = GetFidInForumsByParentid(int.Parse(correntfid));

                ChildNode = ChildNode + "," + correntfid;

                if (dt.Rows.Count > 0)
                {
                    //有子节点
                    foreach (DataRow dr in dt.Rows)
                    {
                        FindChildNode(dr["fid"].ToString());
                    }
                    dt.Dispose();
                }
                else
                {
                    dt.Dispose();
                }
                return ChildNode;
            }
        }
        private static DataTable GetFidInForumsByParentid(int parentid)
        {
            MainForm.targetDBH.ParametersClear();
            MainForm.targetDBH.ParameterAdd("@parentid", parentid, DbType.Int32, 4);
            string sql = string.Format("SELECT fid FROM {0}forums WHERE parentid=@parentid ORDER BY displayorder", MainForm.cic.TargetDbTablePrefix);
            return MainForm.targetDBH.ExecuteDataSet(sql).Tables[0];
        }
        #endregion 

        private static DataTable GetAllForumList()
        {
            string sql = string.Format("SELECT * FROM {0}forums ORDER BY displayorder", MainForm.cic.TargetDbTablePrefix);
            return MainForm.targetDBH.ExecuteDataSet(sql).Tables[0];
        }

        private static DataTable GetMainForum()
        {
            string sql = string.Format("SELECT * FROM {0}forums WHERE layer=0 ORDER BY displayorder", MainForm.cic.TargetDbTablePrefix);
            return MainForm.targetDBH.ExecuteDataSet(sql).Tables[0];
        }
        public static void ResetForums()
        {
            SetForumslayer();
            SetForumsSubForumCountAndDispalyorder();
            SetForumsPathList();
            SetForumsStatus();
        }

        /// <summary>
        /// 设置版块列表中层数(layer)和父列表(parentidlist)字段
        /// </summary>
        private static void SetForumslayer()
        {
            DataTable dt = GetAllForumList();

            foreach (DataRow dr in dt.Rows)
            {
                int layer = 0;
                string parentidlist = "";
                string parentid = dr["parentid"].ToString().Trim();

                //如果是(分类)顶层则直接更新数据库
                if (parentid == "0")
                {
                    SetForumslayer(layer, "0", int.Parse(dr["fid"].ToString()));
                    continue;
                }

                do
                { //更新子版块的层数(layer)和父列表(parentidlist)字段
                    string temp = parentid;

                    parentid = GetForumsParentidByFid(int.Parse(parentid)).ToString();
                    layer++;
                    if (parentid != "0")
                    {
                        parentidlist = temp + "," + parentidlist;
                    }
                    else
                    {
                        parentidlist = temp + "," + parentidlist;
                        SetForumslayer(layer, parentidlist.Substring(0, parentidlist.Length - 1), int.Parse(dr["fid"].ToString()));
                        break;
                    }
                } while (true);
            }
        }
        #region SetForumslayer
        private static void SetForumslayer(int layer, string parentidlist, int fid)
        {
            string sqlLayer = string.Format("UPDATE {0}forums SET layer=@layer WHERE fid=@fid",MainForm.cic.TargetDbTablePrefix);
            string sqlParentidlist = string.Format("UPDATE {0}forums SET parentidlist=@parentidlist WHERE fid=@fid",MainForm.cic.TargetDbTablePrefix);
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            dbh.ParameterAdd("@fid", fid, DbType.Int32, 4);
            dbh.ParameterAdd("@layer", layer, DbType.Int16, 2);
            dbh.ExecuteNonQuery(sqlLayer);

            dbh.ParametersClear();
            dbh.ParameterAdd("@fid", fid, DbType.Int32, 4);
            dbh.ParameterAdd("@parentidlist", parentidlist, DbType.String, 300);
            dbh.ExecuteNonQuery(sqlParentidlist);
            dbh.Close();
        }

        private static int GetForumsParentidByFid(int fid)
        {
            string sql = string.Format("SELECT TOP 1 parentid FROM {0}forums WHERE fid=@fid",MainForm.cic.TargetDbTablePrefix);
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            dbh.ParameterAdd("@fid", fid, DbType.Int32, 4);
            return Convert.ToInt32(dbh.ExecuteScalar(sql));
        }
        #endregion

        /// <summary>
        /// 设置论坛字版数和显示顺序
        /// </summary>
        private static void SetForumsSubForumCountAndDispalyorder()
        {
            DataTable dt = GetAllForumList();

            foreach (DataRow dr in dt.Rows)
            {
                UpdateSubForumCount(int.Parse(dt.Select("parentid=" + dr["fid"].ToString()).Length.ToString()), int.Parse(dr["fid"].ToString()));
            }

            if (dt.Rows.Count == 1) return;

            int displayorder = 1;
            string fidlist;
            foreach (DataRow dr in dt.Select("parentid=0"))
            {
                if (dr["parentid"].ToString() == "0")
                {
                    ChildNode = "0";
                    fidlist = ("," + FindChildNode(dr["fid"].ToString())).Replace(",0,", "");

                    foreach (string fidstr in fidlist.Split(','))
                    {
                        UpdateDisplayorderInForumByFid(displayorder, int.Parse(fidstr));
                        displayorder++;
                    }

                }
            }
        }
        #region SetForumsSubForumCountAndDispalyorder
        private static void UpdateSubForumCount(int subforumcount, int fid)
        {
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            dbh.ParameterAdd("@fid", fid, DbType.Int32, 4);
            dbh.ParameterAdd("@subforumcount", subforumcount, DbType.Int32, 4);
            string sql = string.Format("UPDATE {0}forums SET subforumcount=@subforumcount WHERE fid=@fid",MainForm.cic.TargetDbTablePrefix);
            dbh.ExecuteNonQuery(sql);
            dbh.Close();
        }

        private static void UpdateDisplayorderInForumByFid(int displayorder, int fid)
        {
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            dbh.ParameterAdd("@fid", fid, DbType.Int32, 4);
            dbh.ParameterAdd("@displayorder", displayorder, DbType.Int32, 4);
            string sql = string.Format("UPDATE {0}forums SET displayorder=@displayorder WHERE fid=@fid",MainForm.cic.TargetDbTablePrefix);
            dbh.ExecuteNonQuery(sql);
            dbh.Close();
        }

        #endregion

        /// <summary>
        /// 设置版块列表中论坛路径(pathlist)字段
        /// </summary>
        private static void SetForumsPathList()
        {
            //string extname = GeneralConfigs.Deserialize(Utils.GetMapPath(BaseConfigs.GetForumPath + "config/general.config")).Extname;
            string extname = ".aspx";

            SetForumsPathList(true, extname);
        }
        #region SetForumsPathList
        /// <summary>
        /// 按指定的文件扩展名称设置版块列表中论坛路径(pathlist)字段
        /// </summary>
        /// <param name="extname">扩展名称,如:aspx , html 等</param>
        private static void SetForumsPathList(bool isaspxrewrite, string extname)
        {
            DataTable dt = GetAllForumList();

            foreach (DataRow dr in dt.Rows)
            {
                string pathlist = "";

                if (dr["parentidlist"].ToString().Trim() == "0")
                {
                    if (isaspxrewrite)
                    {
                        pathlist = "<a href=\"showforum-" + dr["fid"].ToString() + extname + "\">" + dr["name"].ToString().Trim() + "</a>";
                    }
                    else
                    {
                        pathlist = "<a href=\"showforum.aspx?forumid=" + dr["fid"].ToString() + "\">" + dr["name"].ToString().Trim() + "</a>";
                    }
                }
                else
                {
                    foreach (string parentid in dr["parentidlist"].ToString().Trim().Split(','))
                    {
                        if (parentid.Trim() != "")
                        {
                            DataRow[] drs = dt.Select("[fid]=" + parentid);
                            if (drs.Length > 0)
                            {
                                if (isaspxrewrite)
                                {
                                    pathlist += "<a href=\"showforum-" + drs[0]["fid"].ToString() + extname + "\">" + drs[0]["name"].ToString().Trim() + "</a>";
                                }
                                else
                                {
                                    pathlist += "<a href=\"showforum.aspx?forumid=" + drs[0]["fid"].ToString() + "\">" + drs[0]["name"].ToString().Trim() + "</a>";
                                }
                            }
                        }
                    }
                    if (isaspxrewrite)
                    {
                        pathlist += "<a href=\"showforum-" + dr["fid"].ToString() + extname + "\">" + dr["name"].ToString().Trim() + "</a>";
                    }
                    else
                    {
                        pathlist += "<a href=\"showforum.aspx?forumid=" + dr["fid"].ToString() + "\">" + dr["name"].ToString().Trim() + "</a>";
                    }
                }

                SetForumsPathList(pathlist, int.Parse(dr["fid"].ToString()));
            }
        }

        private static void SetForumsPathList(string pathlist, int fid)
        {
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            dbh.ParameterAdd("@fid", fid, DbType.Int32, 4);
            dbh.ParameterAdd("@pathlist", pathlist, DbType.String, 3000);
            string sql = string.Format("UPDATE {0}forums SET pathlist=@pathlist WHERE fid=@fid", MainForm.cic.TargetDbTablePrefix);
            dbh.ExecuteNonQuery(sql);
            dbh.Close();
        }
        #endregion

        /// <summary>
        /// 设置论坛版块的状态
        /// </summary>
        private static void SetForumsStatus()
        {
            DataTable dt = GetMainForum();


            foreach (DataRow dr in dt.Rows)
            {
                ChildNode = "0";
                string fidlist = ("," + FindChildNode(dr["fid"].ToString())).Replace(",0,", "");

                if (dr["status"].ToString() == "0")
                {
                    UpdateStatusByFidlist(fidlist);
                }
                else if (dr["status"].ToString() == "1")
                {
                    UpdateStatusByFidlistOther(fidlist);
                }
                else
                {
                    SetStatusInForum(4, int.Parse(dr["fid"].ToString()));

                    int i = 5;
                    foreach (DataRow currentdr in GetForumByParentid(int.Parse(dr["fid"].ToString())).Rows)
                    {
                        SetStatusInForum(i, int.Parse(currentdr["fid"].ToString()));
                        i++;
                    }
                }
            }
        }
        #region SetForumsStatus
        private static void UpdateStatusByFidlist(string fidlist)
        {
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            string sql = string.Format("UPDATE {0}forums SET [status]=0 WHERE fid IN({1})", MainForm.cic.TargetDbTablePrefix,fidlist);
            dbh.ExecuteNonQuery(sql);
            dbh.Close();
        }

        private static void UpdateStatusByFidlistOther(string fidlist)
        {
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            //MainForm.targetDBH.ParameterAdd("@fidlist", fidlist, DbType.String, 3000);
            string sql = string.Format("UPDATE {0}forums SET status=1 WHERE status>1 AND fid IN({1})", MainForm.cic.TargetDbTablePrefix,fidlist);
            dbh.ExecuteNonQuery(sql);
            dbh.Close();
        }

        private static void SetStatusInForum(int status, int fid)
        {
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParametersClear();
            dbh.ParameterAdd("@fid", fid, DbType.Int32, 4);
            dbh.ParameterAdd("@status", status, DbType.Int32, 4);
            string sql = string.Format("UPDATE {0}forums SET status=@status WHERE AND fid=@fid", MainForm.cic.TargetDbTablePrefix);
            dbh.ExecuteNonQuery(sql);
            dbh.Close();
        }

        private static DataTable GetForumByParentid(int parentid)
        {
            Yuwen.Tools.TinyData.DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            dbh.ParameterAdd("@parentid", parentid, DbType.Int32, 4);
            string sql = "SELECT * FROM {0}forums WHERE parentid=@parentid ORDER BY DisplayOrder";
            DataSet ds = dbh.ExecuteDataSet(sql);
            dbh.Close();
            return ds.Tables[0];
        }
        #endregion
    }
}
